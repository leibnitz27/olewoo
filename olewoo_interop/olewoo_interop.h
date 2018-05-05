// olewoo_interop.h

#include "stdafx.h"
#include "typelibdependencies.h"
#include <unordered_set>
#include "atlbase.h"
#include <string>
#pragma once

using namespace System;

namespace olewoo_interop {

	public ref class TypeLibMetadata
	{
	public:
		System::Collections::Generic::List<System::Runtime::InteropServices::ComTypes::ITypeLib^>^ GetDependentLibraries(System::Runtime::InteropServices::ComTypes::ITypeLib^ ptypeLib)
		{
			auto list = gcnew System::Collections::Generic::List<System::Runtime::InteropServices::ComTypes::ITypeLib^>();
			System::IntPtr ip;
			try {
				ip = System::Runtime::InteropServices::Marshal::GetIUnknownForObject(ptypeLib);
				ITypeLib* pitl = static_cast<ITypeLib*>(ip.ToPointer());
				auto set = GetDependencies(pitl);
				for (const auto& elem : set) {
					auto ptr = elem.GetInterfacePtr();
					auto intPtr = System::IntPtr(ptr);
					auto obj = System::Runtime::InteropServices::Marshal::GetObjectForIUnknown(intPtr);
					auto lib = static_cast<System::Runtime::InteropServices::ComTypes::ITypeLib^>(obj);
					list->Add(lib);
				}
			}
			catch (...)
			{
				// oh well
			}

			if (ip != System::IntPtr::Zero)
			{
				System::Runtime::InteropServices::Marshal::Release(ip);
			}
			return list;
		}
	};

	public ref class IDLFormatter_iop abstract
	{
	public:
		virtual void AddString(System::String ^ s) = 0;
		virtual void AddLink(System::String ^ s, System::String ^ s2) = 0;
	};

	System::Guid MkSystemGuid(GUID & graw);
	void stringifyTypeDesc(TYPEDESC* typeDesc, ITypeInfo* pTypeInfo, IDLFormatter_iop ^ ift);

	public ref class CustomData
	{
		System::Guid _g;
		System::Object ^ _o;
	public:
		CustomData(GUID g, VARIANT & v)
		{
			_g = MkSystemGuid(g);
			_o = System::Runtime::InteropServices::Marshal::GetObjectForNativeVariant(System::IntPtr(&v));
		}
		property System::Guid guid
		{
			System::Guid get()
			{
				return _g;
			}
		}
		property System::Object ^ varValue
		{
			System::Object ^ get()
			{
				return _o;
			}
		}
	};

	public ref class CustomDatas
	{
		CUSTDATA * _custdata;
	public:
		CustomDatas(System::Runtime::InteropServices::ComTypes::ITypeLib2 ^ tl2)
		{
			_custdata = new CUSTDATA();
			IntPtr mpcustdata((void*)_custdata);
			tl2->GetAllCustData((IntPtr %)mpcustdata);
		}
		~CustomDatas()
		{
			this->!CustomDatas();
		}
		!CustomDatas()
		{
			delete _custdata;
		}
		property cli::array<CustomData ^, 1> ^ Items
		{
			cli::array<CustomData ^, 1> ^ get()
			{
				cli::array<CustomData ^, 1> ^ res = gcnew cli::array<CustomData ^, 1>(_custdata->cCustData);
				for (size_t x=0;x<_custdata->cCustData;++x)
				{
					CUSTDATAITEM & cdi = _custdata->prgCustData[x];
					CustomData ^ cd = gcnew CustomData(cdi.guid, cdi.varValue);
					res[x] = cd;
				}
				return res;
			}
		}
	};

	public ref class TypeLibAttr : public System::IDisposable
	{
		System::Runtime::InteropServices::ComTypes::ITypeLib ^ _tl;
		TLIBATTR * _plibAttr;
	public:
		enum class LibFlags
		{	
			LIBFLAG_FRESTRICTED	= 0x1,
			LIBFLAG_FCONTROL	= 0x2,
			LIBFLAG_FHIDDEN	= 0x4,
			LIBFLAG_FHASDISKIMAGE	= 0x8
		};

		TypeLibAttr(System::Runtime::InteropServices::ComTypes::ITypeLib ^ tl)
		{
			System::IntPtr pt;
			_tl = tl;
			tl->GetLibAttr((System::IntPtr %)pt);
			_plibAttr = (TLIBATTR *)pt.ToPointer();
		}
		~TypeLibAttr(void)
		{
			this->!TypeLibAttr();
		}
		!TypeLibAttr()
		{
			_tl->ReleaseTLibAttr(System::IntPtr(_plibAttr));
			_plibAttr = NULL;
		}
		property System::Guid guid
		{
			System::Guid get()
			{
				return MkSystemGuid(_plibAttr->guid);
			}
		}
		property int lcid
		{
			int get()
			{
				return _plibAttr->lcid;
			}
		}
		property int syskind
		{
			int get()
			{
				return _plibAttr->syskind;
			}
		}
		property int wMajorVerNum
		{
			int get()
			{
				return _plibAttr->wMajorVerNum;
			}
		}
		property int wMinorVerNum
		{
			int get()
			{
				return _plibAttr->wMinorVerNum;
			}
		}
		property LibFlags wLibFlags
		{
			LibFlags get()
			{
				return (LibFlags)_plibAttr->wLibFlags;
			}
		}
	};

	public ref class TypeDesc
	{
		TYPEDESC * m_ptd;
	public:
		TypeDesc(TYPEDESC & td)
		{
			m_ptd = &td;
		}
		void ComTypeNameAsString(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ ti, IDLFormatter_iop ^ ift)
		{
			CComVariant tmp;
			System::Runtime::InteropServices::Marshal::GetNativeVariantForObject(ti, System::IntPtr(&tmp));
			CComQIPtr<ITypeInfo> titmp = tmp.punkVal;
			stringifyTypeDesc(m_ptd, titmp, ift);
		}
		property int hreftype
		{
			int get()
			{
				return m_ptd->hreftype;
			}
		}
	};

	public ref class ParamDesc
	{
		PARAMDESC * m_pd;
	public:
		enum class ParamFlags
		{
			PARAMFLG_NONE = 0,
			PARAMFLG_FIN = 0x1,
			PARAMFLG_FOUT	= 0x2 ,
			PARAMFLG_FLCID	= 0x4 ,
			PARAMFLG_FRETVAL	= 0x8 ,
			PARAMFLG_FOPT	= 0x10 ,
			PARAMFLG_FHASDEFAULT	= 0x20 ,
			PARAMFLG_FHASCUSTDATA	= 0x40
		};
		ParamDesc(PARAMDESC & pd)
		{
			m_pd = &pd;
		}
		property ParamFlags wParamFlags
		{
			ParamFlags get()
			{
				return (ParamFlags)m_pd->wParamFlags;
			}
		}
		property System::Object ^ varDefaultValue
		{
			System::Object ^ get()
			{
				return System::Runtime::InteropServices::Marshal::GetObjectForNativeVariant(System::IntPtr(&(m_pd->pparamdescex->varDefaultValue)));
			}
		}

	};

	public ref class ElemDesc
	{
		// What's this tied to the lifetime of?
		ELEMDESC * m_ed;
	public:
		ElemDesc(ELEMDESC & e)
		{
			m_ed = &e;
		}
		property TypeDesc ^ tdesc
		{
			TypeDesc ^ get()
			{
				return gcnew TypeDesc(m_ed->tdesc);
			}
		}
		property ParamDesc ^ paramdesc
		{
			ParamDesc ^ get()
			{
				return gcnew ParamDesc(m_ed->paramdesc);
			}
		}
	};


	public ref class VarDesc
	{
		System::Runtime::InteropServices::ComTypes::ITypeInfo ^ m_i;
		IntPtr m_fdptr;
		VARDESC * m_vardesc;
	public:
		VarDesc(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i, int idx)
		{
			IntPtr fdptr;
			i->GetVarDesc(idx, (IntPtr%)fdptr);
			m_vardesc = (VARDESC*)fdptr.ToPointer();
			m_i = i;
		}
		~VarDesc()
		{
			m_i->ReleaseVarDesc(m_fdptr);
			m_vardesc = NULL;
		}
		property long memid
		{
			long get()
			{
				return m_vardesc->memid;
			}
		}
		//property long varValue
		//{
		//	long get()
		//	{
		//		return m_vardesc->lpvarValue->lVal;
		//	}
		//}
		property System::Object ^ varValue
		{
			System::Object ^ get()
			{
				return System::Runtime::InteropServices::Marshal::GetObjectForNativeVariant(System::IntPtr(m_vardesc->lpvarValue));
			}
		}
		property VARKIND varkind
		{
			VARKIND get()
			{
				return m_vardesc->varkind;
			}
		}
		property ElemDesc ^ elemDescVar
		{
			ElemDesc ^ get()
			{
				return gcnew ElemDesc(m_vardesc->elemdescVar);
			}
		}
	};


	public ref class FuncDesc
	{
		System::Runtime::InteropServices::ComTypes::ITypeInfo ^ m_i;
		IntPtr m_fdptr;
		FUNCDESC * m_funcdesc;
	public:
		 /* [v1_enum] */ 
		enum class InvokeKind
		{	INVOKE_FUNC	= 1,
			INVOKE_PROPERTYGET	= 2,
			INVOKE_PROPERTYPUT	= 4,
			INVOKE_PROPERTYPUTREF	= 8
		};

		enum class FuncFlags
			{	FUNCFLAG_FRESTRICTED	= 0x1,
			FUNCFLAG_FSOURCE	= 0x2,
			FUNCFLAG_FBINDABLE	= 0x4,
			FUNCFLAG_FREQUESTEDIT	= 0x8,
			FUNCFLAG_FDISPLAYBIND	= 0x10,
			FUNCFLAG_FDEFAULTBIND	= 0x20,
			FUNCFLAG_FHIDDEN	= 0x40,
			FUNCFLAG_FUSESGETLASTERROR	= 0x80,
			FUNCFLAG_FDEFAULTCOLLELEM	= 0x100,
			FUNCFLAG_FUIDEFAULT	= 0x200,
			FUNCFLAG_FNONBROWSABLE	= 0x400,
			FUNCFLAG_FREPLACEABLE	= 0x800,
			FUNCFLAG_FIMMEDIATEBIND	= 0x1000
			};
		enum class CallConv
			{	CC_FASTCALL	= 0,
			CC_CDECL	= 1,
			CC_MSCPASCAL	= ( CC_CDECL + 1 ) ,
			CC_PASCAL	= CC_MSCPASCAL,
			CC_MACPASCAL	= ( CC_PASCAL + 1 ) ,
			CC_STDCALL	= ( CC_MACPASCAL + 1 ) ,
			CC_FPFASTCALL	= ( CC_STDCALL + 1 ) ,
			CC_SYSCALL	= ( CC_FPFASTCALL + 1 ) ,
			CC_MPWCDECL	= ( CC_SYSCALL + 1 ) ,
			CC_MPWPASCAL	= ( CC_MPWCDECL + 1 ) ,
			CC_MAX	= ( CC_MPWPASCAL + 1 ) 
		};

		FuncDesc(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i, int idx)
		{
			IntPtr fdptr;
			i->GetFuncDesc(idx, (IntPtr%)fdptr);
			m_funcdesc = (FUNCDESC*)fdptr.ToPointer();
			m_i = i;
		}
		~FuncDesc()
		{
			m_i->ReleaseFuncDesc(m_fdptr);
			m_funcdesc = NULL;
		}
		property long cParams
		{
			long get()
			{
				return m_funcdesc->cParams;
			}
		}
		property long memid
		{
			long get()
			{
				return m_funcdesc->memid;
			}
		}
		property InvokeKind invkind
		{
			InvokeKind get()
			{
				return (InvokeKind)m_funcdesc->invkind;
			}
		}
		property CallConv callconv
		{
			CallConv get()
			{
				return (CallConv)m_funcdesc->callconv;
			}
		}
		property FuncFlags wFuncFlags
		{
			FuncFlags get()
			{
				return (FuncFlags)m_funcdesc->wFuncFlags;
			}
		}
		property ElemDesc ^ elemdescFunc
		{
			ElemDesc ^ get()
			{
				return gcnew ElemDesc(m_funcdesc->elemdescFunc);
			}
		}
		property cli::array<ElemDesc ^, 1> ^ elemdescParams
		{
			cli::array<ElemDesc ^, 1> ^ get()
			{
				cli::array<ElemDesc ^, 1> ^ res = gcnew cli::array<ElemDesc ^, 1>(m_funcdesc->cParams);
				for (int x=0;x<m_funcdesc->cParams;++x)
				{
					res[x] = gcnew ElemDesc(m_funcdesc->lprgelemdescParam[x]);
				}
				return res;
			}
		}
	};


	public ref class TypeAttr
	{
		System::Runtime::InteropServices::ComTypes::ITypeInfo ^ m_i;
		IntPtr m_attrptr;
		TYPEATTR * m_attr;
	public:
		enum class TypeKind // tagTYPEKIND
		{
			TKIND_ENUM	= 0,
			TKIND_RECORD	= TKIND_ENUM + 1,
			TKIND_MODULE	= TKIND_RECORD + 1,
			TKIND_INTERFACE	= TKIND_MODULE + 1,
			TKIND_DISPATCH	= TKIND_INTERFACE + 1,
			TKIND_COCLASS	= TKIND_DISPATCH + 1,
			TKIND_ALIAS	= TKIND_COCLASS + 1,
			TKIND_UNION	= TKIND_ALIAS + 1,
			TKIND_MAX	= TKIND_UNION + 1	
		};

		enum class TypeFlags
			{	TYPEFLAG_FAPPOBJECT	= 0x1,
			TYPEFLAG_FCANCREATE	= 0x2,
			TYPEFLAG_FLICENSED	= 0x4,
			TYPEFLAG_FPREDECLID	= 0x8,
			TYPEFLAG_FHIDDEN	= 0x10,
			TYPEFLAG_FCONTROL	= 0x20,
			TYPEFLAG_FDUAL	= 0x40,
			TYPEFLAG_FNONEXTENSIBLE	= 0x80,
			TYPEFLAG_FOLEAUTOMATION	= 0x100,
			TYPEFLAG_FRESTRICTED	= 0x200,
			TYPEFLAG_FAGGREGATABLE	= 0x400,
			TYPEFLAG_FREPLACEABLE	= 0x800,
			TYPEFLAG_FDISPATCHABLE	= 0x1000,
			TYPEFLAG_FREVERSEBIND	= 0x2000,
			TYPEFLAG_FPROXY	= 0x4000
			};

		TypeAttr(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i)
		{
			Assign(i);
		}
//		void ReplaceTypeInfo(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i);
		void Release();

		~TypeAttr()
		{
			Release();			
		}

		property TypeKind typekind
		{
			TypeKind get()
			{
				return (TypeKind)(m_attr->typekind);
			}
		}
		property TypeFlags wTypeFlags
		{
			TypeFlags get()
			{
				return (TypeFlags)m_attr->wTypeFlags;
			}
		}
		property int cFuncs
		{
			int get()
			{
				return m_attr->cFuncs;
			}
		}
		property int cVars
		{
			int get()
			{
				return m_attr->cVars;
			}
		}
		property int cImplTypes
		{
			int get()
			{
				return m_attr->cImplTypes;
			}
		}
		property TypeDesc ^ tdescAlias
		{
			TypeDesc ^ get()
			{
				return gcnew TypeDesc(m_attr->tdescAlias);
			}
		}
		property Guid guid
		{
			Guid get()
			{
				return MkSystemGuid(m_attr->guid);
			}
		}
		property int wMajorVerNum
		{
			int get()
			{
				return m_attr->wMajorVerNum;
			}
		}
		property int wMinorVerNum
		{
			int get()
			{
				return m_attr->wMinorVerNum;
			}
		}
	private:
		void Assign(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i);
	};

	public ref class ITypeInfoXtra
	{
	public:
		System::String ^ GetDllEntry(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ ti, FuncDesc::InvokeKind invkind, int memid)
		{
			USES_CONVERSION;
			CComVariant tmp;
			System::Runtime::InteropServices::Marshal::GetNativeVariantForObject(ti, System::IntPtr(&tmp));
			CComQIPtr<ITypeInfo> titmp = tmp.punkVal;
			CComBSTR dllentry;
			if (SUCCEEDED(titmp->GetDllEntry(memid,(INVOKEKIND) invkind, &dllentry, 0, 0)) && (dllentry != 0))
			{
				return gcnew System::String(OLE2A(dllentry));
			}
			return "";
		}
	};
}
