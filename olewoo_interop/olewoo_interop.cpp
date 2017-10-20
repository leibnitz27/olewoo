/**************************************
 *
 * Part of OLEWOO - http://www.benf.org
 *
 * CopyLeft, but please credit.
 *
 */
#include "stdafx.h"

#include "olewoo_interop.h"

using namespace olewoo_interop;

System::Guid olewoo_interop::MkSystemGuid(GUID & graw)
{
	return System::Guid::Guid(graw.Data1, graw.Data2, graw.Data3, graw.Data4[0], graw.Data4[1], graw.Data4[2], graw.Data4[3], graw.Data4[4], graw.Data4[5], graw.Data4[6], graw.Data4[7]);
}

void TypeAttr::Release()
{
	if (m_attrptr.ToPointer() != NULL)
	{
		m_i->ReleaseTypeAttr(m_attrptr);
	}
	m_attr = NULL;
	m_i = nullptr;
}

void TypeAttr::Assign(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i)
{
	m_i = i;
	IntPtr  ipt;
	i->GetTypeAttr((IntPtr %)ipt);
	m_attr = (TYPEATTR*)ipt.ToPointer();
	m_attrptr = ipt;
}

/*
void TypeAttr::ReplaceTypeInfo(System::Runtime::InteropServices::ComTypes::ITypeInfo ^ i)
{
	Release();
	Assign(i);
}
*/

