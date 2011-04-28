
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.1
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v2.0.50727
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 2:0:0:0
}
.assembly extern Rule2.Interfaces
{
  .ver 1:0:0:0
}
.assembly extern Rule2.Console
{
  .ver 1:0:0:0
}
.assembly Rule2.Library
{
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 31 2E 30 2E 30 2E 30 00 00 )             // ...1.0.0.0..
  .custom instance void [mscorlib]System.Runtime.InteropServices.GuidAttribute::.ctor(string) = ( 01 00 24 32 37 33 33 36 66 65 33 2D 30 31 63 33   // ..$27336fe3-01c3
                                                                                                  2D 34 65 37 63 2D 62 34 35 64 2D 39 37 33 32 35   // -4e7c-b45d-97325
                                                                                                  66 32 65 35 39 33 66 00 00 )                      // f2e593f..
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Reflection.AssemblyTrademarkAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 0D 52 75 6C 65 32 2E 4C 69 62 72 61 72 79   // ...Rule2.Library
                                                                                              00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = ( 01 00 1B 43 6F 70 79 72 69 67 68 74 20 C2 A9 20   // ...Copyright .. 
                                                                                                  4D 69 63 72 6F 73 6F 66 74 20 32 30 31 31 00 00 ) // Microsoft 2011..
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 0D 52 75 6C 65 32 2E 4C 69 62 72 61 72 79   // ...Rule2.Library
                                                                                                00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 09 4D 69 63 72 6F 73 6F 66 74 00 00 )       // ...Microsoft..
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module Rule2.Library.dll
// MVID: {9145B1E4-8C8E-4B5F-9666-BB806922B65B}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x00390000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit Rule2.Library.ConsoleGreeter
       extends [mscorlib]System.Object
       implements [Rule2.Interfaces]Rule2.Interfaces.IGreeter
{
  .method public hidebysig newslot virtual final 
          instance void  Greet(string target) cil managed
  {
    // Code size       14 (0xe)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      "Hello, {0}!"
    IL_0006:  ldarg.1
    IL_0007:  call       void [Rule2.Console]Rule2.Console.HelperClass::WriteLine(string,
                                                                                  string,
                                                                                  object)
    IL_000c:  nop
    IL_000d:  ret
  } // end of method ConsoleGreeter::Greet

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method ConsoleGreeter::.ctor

} // end of class Rule2.Library.ConsoleGreeter


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file SampleRunnerWithStackUnderflow.res
