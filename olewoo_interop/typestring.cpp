/**************************************
 *
 * Part of OLEWOO - http://www.benf.org
 *
 * CopyLeft, but please credit.
 *
 */
#include "stdafx.h"
#include "olewoo_interop.h"
#include <sstream>
#include <string>
#include <msclr/marshal.h>

using namespace olewoo_interop;



namespace olewoo_interop
{

void stringifyCustomType(HREFTYPE refType, ITypeInfo* pti, IDLFormatter_iop ^ ift, CustomTypeLibData ^ custLibData) 
{
    CComPtr<ITypeInfo> pTypeInfo(pti);
    CComPtr<ITypeInfo> pCustTypeInfo;
	HRESULT hr(pTypeInfo->GetRefTypeInfo(refType, &pCustTypeInfo));
    if(hr)
	{
		ift->AddString("UnknownCustomType");
		return;
	}
    CComBSTR bstrType;
    hr = pCustTypeInfo->GetDocumentation(-1, &bstrType, 0, 0, 0);
    if(hr) 
	{
		ift->AddString("UnknownCustomType");
		return;
	}
	ITypeLib* pCustTypeLib;
	UINT uIndex;
	hr = pCustTypeInfo->GetContainingTypeLib(&pCustTypeLib, &uIndex);
	if (SUCCEEDED(hr))
	{
		CComBSTR bstrLib;
		hr = pCustTypeLib->GetDocumentation(-1, &bstrLib, 0, 0, 0);
		if (SUCCEEDED(hr))
		{
			LPTLIBATTR attr;
			hr = pCustTypeLib->GetLibAttr(&attr);
			if (SUCCEEDED(hr))
			{
				CustomTypeLibData^ data = gcnew CustomTypeLibData();
				
				data->LibName = msclr::interop::marshal_as<System::String^>(bstrLib.m_str);
				data->LibGuid = MkSystemGuid(attr->guid);
				data->MajorVersion = attr->wMajorVerNum;
				data->MinorVersion = attr->wMinorVerNum;

				pCustTypeLib->ReleaseTLibAttr(attr);
			}
		}
	}
    char ansiType[MAX_PATH];
    WideCharToMultiByte(CP_ACP, 0, bstrType, bstrType.Length() + 1, 
        ansiType, MAX_PATH, 0, 0);
	ift->AddLink(gcnew System::String(ansiType), "i");
	return;
}

void stringifyTypeDesc(TYPEDESC* typeDesc, ITypeInfo* pTypeInfo, IDLFormatter_iop ^ ift, CustomTypeLibData ^ custLibData) {
    if(typeDesc->vt == VT_PTR) 
	{
        stringifyTypeDesc(typeDesc->lptdesc, pTypeInfo, ift, custLibData);
		ift->AddString("*");
		return;
    }
    if(typeDesc->vt == VT_SAFEARRAY) {
        ift->AddString("SAFEARRAY(");
        stringifyTypeDesc(typeDesc->lptdesc, pTypeInfo, ift, custLibData);
		ift->AddString(")");
		return;
    }
    if(typeDesc->vt == VT_CARRAY) {
        stringifyTypeDesc(&typeDesc->lpadesc->tdescElem, pTypeInfo, ift, custLibData);
        for(int dim(0); typeDesc->lpadesc->cDims; ++dim) 
		{
			std::stringstream oss;
            oss<< '['<< typeDesc->lpadesc->rgbounds[dim].lLbound<< "..."
                << (typeDesc->lpadesc->rgbounds[dim].cElements + 
                typeDesc->lpadesc->rgbounds[dim].lLbound - 1)<< ']';
			ift->AddString(gcnew System::String(oss.str().c_str()));
		}
		return;
    }
    if(typeDesc->vt == VT_USERDEFINED) {
        stringifyCustomType(typeDesc->hreftype, pTypeInfo, ift, custLibData);
        return;
    }
    
    switch(typeDesc->vt) {
        // VARIANT/VARIANTARG compatible types
    case VT_I2: ift->AddString( "short"); return;
    case VT_I4: ift->AddString( "long"); return;
    case VT_R4: ift->AddString( "float"); return;
    case VT_R8: ift->AddString( "double"); return;
    case VT_CY: ift->AddString( "CY"); return;
    case VT_DATE: ift->AddString( "DATE"); return;
    case VT_BSTR: ift->AddString( "BSTR"); return;
    case VT_DISPATCH: ift->AddString( "IDispatch*"); return;
    case VT_ERROR: ift->AddString( "SCODE"); return;
    case VT_BOOL: ift->AddString( "VARIANT_BOOL"); return;
    case VT_VARIANT: ift->AddString( "VARIANT"); return;
    case VT_UNKNOWN: ift->AddString( "IUnknown*"); return;
    case VT_UI1: ift->AddString( "BYTE"); return;
    case VT_DECIMAL: ift->AddString( "DECIMAL"); return;
    case VT_I1: ift->AddString( "char"); return;
    case VT_UI2: ift->AddString( "unsigned short"); return;
    case VT_UI4: ift->AddString( "unsigned long"); return;
    case VT_I8: ift->AddString( "int64"); return;
    case VT_UI8: ift->AddString( "uint64"); return;
    case VT_INT: ift->AddString( "int"); return;
    case VT_UINT: ift->AddString( "unsigned int"); return;
    case VT_HRESULT: ift->AddString( "HRESULT"); return;
    case VT_VOID: ift->AddString( "void"); return;
    case VT_LPSTR: ift->AddString( "LPSTR"); return;
    case VT_LPWSTR: ift->AddString( "LPWSTR"); return;
    }
	{
		std::stringstream oss;
		oss << "[??Unknown type : " << typeDesc->vt << "]";
		ift->AddString(gcnew System::String(oss.str().c_str()));
	}
}
}