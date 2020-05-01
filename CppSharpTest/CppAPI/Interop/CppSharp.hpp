#pragma once

/**
 * These defines are empty but signal to the CppSharp Generator to generate out and ref parameters.
 * For byte vectors use ByteVectorHolder.
 */
#define CS_IN
#define CS_OUT
#define CS_IN_OUT

#define CS_IGNORE

#if defined(_MSC_VER)
#define _DLLEXPORT_		__declspec(dllexport)
#else
#define _DLLEXPORT_ /**/
#endif
