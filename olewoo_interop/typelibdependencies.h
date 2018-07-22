#pragma once
#include "Stdafx.h"
#include <windows.h>
#include <comdef.h>
#include <unordered_set>

// The class aims to extract the dependent type
// libraries for a given type library, using the
// code provided at Stack Overflow:
// https://stackoverflow.com/a/45090861/643342

// gets dependencies of a type library
std::unordered_set<ITypeLibPtr> GetDependencies(ITypeLib* pTypeLib);

// gathers dependencies of a type library
void GetDependenciesHelper(ITypeLib* pTypeLib, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of a type
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of a reference
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, HREFTYPE hRefType, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of a reference
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, TYPEDESC& referencedTypeDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of a function declaration
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, FUNCDESC& functionDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of a variable declaration
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, VARDESC& variableDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of an array declaration
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, ARRAYDESC& arrayDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

// gathers dependencies of an array element declaration
void GetDependenciesHelper(ITypeLib* pTypeLib, ITypeInfo* pTypeInfo, ELEMDESC& elementDescription, std::unordered_set<ITypeInfoPtr>* pHistory, std::unordered_set<ITypeLibPtr>* pOutput);

namespace std
{
	// provides a function for hashing ITypeLibPtr instances by their raw address
	template<> struct hash<ITypeLibPtr>
	{
		size_t operator()(ITypeLibPtr const& pTypeLib) const { return pTypeLib; }
	};
	// provides a function for hashing ITypeInfo instances by their raw address
	template<> struct hash<ITypeInfoPtr>
	{
		size_t operator()(ITypeInfoPtr const& pTypeInfo) const { return pTypeInfo; }
	};
}