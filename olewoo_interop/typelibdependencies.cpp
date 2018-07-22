#include "Stdafx.h"
#include "typelibdependencies.h"

std::unordered_set<ITypeLibPtr> GetDependencies(ITypeLib* pTypeLib)
{
	// get dependencies
	std::unordered_set<ITypeLibPtr> output;
	std::unordered_set<ITypeInfoPtr> history;
	GetDependenciesHelper(pTypeLib, &history, &output);
	return output;
}

void GetDependenciesHelper(ITypeLib* pTypeLib, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// iterate over type infos
	auto typeInfoCount = pTypeLib->GetTypeInfoCount();
	for (UINT typeInfoIndex = 0; typeInfoIndex < typeInfoCount; ++typeInfoIndex)
	{
		// get type info
		ITypeInfoPtr pTypeInfo;
		_com_util::CheckError(pTypeLib->GetTypeInfo(typeInfoIndex, &pTypeInfo));

		// get dependencies for type info
		GetDependenciesHelper(pTypeLib, pTypeInfo, pHistory, pOutput);
	}
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// short-circuit if we've already processed this type info
	if (!pHistory->insert(pTypeInfo).second)
		return;

	// get type attributes
	TYPEATTR* typeAttributes;
	_com_util::CheckError(pTypeInfo->GetTypeAttr(&typeAttributes));
	try
	{
		// special handling for aliases
		if (typeAttributes->typekind == TKIND_ALIAS)
		{
			// get dependencies of the alias
			GetDependenciesHelper(pTypeLib, pTypeInfo, typeAttributes->tdescAlias, pHistory, pOutput);
		}
		else
		{
			// iterate over implemented types
			auto implementedTypeCount = typeAttributes->cImplTypes;
			for (WORD implementedTypeIndex = 0; implementedTypeIndex < implementedTypeCount; ++implementedTypeIndex)
			{
				// get type reference
				HREFTYPE hRefType;
				_com_util::CheckError(pTypeInfo->GetRefTypeOfImplType(implementedTypeIndex, &hRefType));

				// get dependencies of the implementation
				GetDependenciesHelper(pTypeLib, pTypeInfo, hRefType, pHistory, pOutput);
			}

			// iterate over functions
			auto functionCount = typeAttributes->cFuncs;
			for (WORD functionIndex = 0; functionIndex < functionCount; ++functionIndex)
			{
				// get function description
				FUNCDESC* functionDescription;
				_com_util::CheckError(pTypeInfo->GetFuncDesc(functionIndex, &functionDescription));
				try
				{
					// get dependencies of the function declaration
					GetDependenciesHelper(pTypeLib, pTypeInfo, *functionDescription, pHistory, pOutput);
				}
				catch (...)
				{
					// release function description
					pTypeInfo->ReleaseFuncDesc(functionDescription);
					throw;
				}

				// release function description
				pTypeInfo->ReleaseFuncDesc(functionDescription);
			}

			// iterate over variables
			auto variableCount = typeAttributes->cVars;
			for (WORD variableIndex = 0; variableIndex < variableCount; ++variableIndex)
			{
				// get variable description
				VARDESC* variableDescription;
				_com_util::CheckError(pTypeInfo->GetVarDesc(variableIndex, &variableDescription));
				try
				{
					// get dependencies of the variable declaration
					GetDependenciesHelper(pTypeLib, pTypeInfo, *variableDescription, pHistory, pOutput);
				}
				catch (...)
				{
					// release variable description
					pTypeInfo->ReleaseVarDesc(variableDescription);
					throw;
				}

				// release variable description
				pTypeInfo->ReleaseVarDesc(variableDescription);
			}
		}
	}
	catch (...)
	{
		// release type attributes
		pTypeInfo->ReleaseTypeAttr(typeAttributes);
		throw;
	}

	// release type attributes
	pTypeInfo->ReleaseTypeAttr(typeAttributes);
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, HREFTYPE hRefType, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// get referenced type info
	ITypeInfoPtr referencedTypeInfo;
	_com_util::CheckError(pTypeInfo->GetRefTypeInfo(hRefType, &referencedTypeInfo));

	// get referenced type lib
	ITypeLibPtr referencedTypeLibrary;
	UINT referencedTypeInfoIndex;
	_com_util::CheckError(referencedTypeInfo->GetContainingTypeLib(&referencedTypeLibrary, &referencedTypeInfoIndex));

	// store dependency
	if (referencedTypeLibrary != pTypeLib)
		pOutput->insert(referencedTypeLibrary);
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, TYPEDESC& referencedTypeDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	switch (referencedTypeDescription.vt)
	{
	case VT_PTR:
	{
		// get dependencies of the pointer declaration
		GetDependenciesHelper(pTypeLib, pTypeInfo, *referencedTypeDescription.lptdesc, pHistory, pOutput);
		break;
	}
	case VT_CARRAY:
	{
		// get dependencies of the array declaration
		GetDependenciesHelper(pTypeLib, pTypeInfo, *referencedTypeDescription.lpadesc, pHistory, pOutput);
		break;
	}
	case VT_USERDEFINED:
	{
		// get dependencies of the UDT reference
		GetDependenciesHelper(pTypeLib, pTypeInfo, referencedTypeDescription.hreftype, pHistory, pOutput);
		break;
	}
	}
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, FUNCDESC& functionDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// get dependencies of the function return value
	GetDependenciesHelper(pTypeLib, pTypeInfo, functionDescription.elemdescFunc, pHistory, pOutput);

	// iterate over parameters
	auto parameterCount = functionDescription.cParams;
	for (SHORT parameterIndex = 0; parameterIndex < parameterCount; ++parameterIndex)
	{
		// get parameter description
		auto& parameterDescription = functionDescription.lprgelemdescParam[parameterIndex];

		// get dependencies of the parameter declaration
		GetDependenciesHelper(pTypeLib, pTypeInfo, parameterDescription, pHistory, pOutput);
	}
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, VARDESC& variableDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// get dependencies of the variable declaration
	GetDependenciesHelper(pTypeLib, pTypeInfo, variableDescription.elemdescVar, pHistory, pOutput);
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, ARRAYDESC& arrayDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// get dependencies of the array declaration
	GetDependenciesHelper(pTypeLib, pTypeInfo, arrayDescription.tdescElem, pHistory, pOutput);
}

void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, ELEMDESC& elementDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput)
{
	// get dependencies of the array element declaration
	GetDependenciesHelper(pTypeLib, pTypeInfo, elementDescription.tdesc, pHistory, pOutput);
}