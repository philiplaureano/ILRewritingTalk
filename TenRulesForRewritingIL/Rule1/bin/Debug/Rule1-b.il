
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.1
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v2.0.50727
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 2:0:0:0
}
.assembly Rule1
{
  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 05 52 75 6C 65 31 00 00 )                   // ...Rule1..
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 09 4D 69 63 72 6F 73 6F 66 74 00 00 )       // ...Microsoft..
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 05 52 75 6C 65 31 00 00 )                   // ...Rule1..
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = ( 01 00 1B 43 6F 70 79 72 69 67 68 74 20 C2 A9 20   // ...Copyright .. 
                                                                                                  4D 69 63 72 6F 73 6F 66 74 20 32 30 31 31 00 00 ) // Microsoft 2011..
  .custom instance void [mscorlib]System.Reflection.AssemblyTrademarkAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.GuidAttribute::.ctor(string) = ( 01 00 24 65 32 66 62 39 31 64 62 2D 64 31 39 63   // ..$e2fb91db-d19c
                                                                                                  2D 34 32 36 34 2D 38 33 61 35 2D 62 32 33 30 62   // -4264-83a5-b230b
                                                                                                  36 38 65 61 36 61 34 00 00 )                      // 68ea6a4..
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 31 2E 30 2E 30 2E 30 00 00 )             // ...1.0.0.0..

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module Rule1.exe
// MVID: {A49F98CB-F7F5-4B99-8702-481817CC79A9}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000003    //  ILONLY 32BITREQUIRED
// Image base: 0x00460000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit Rule1.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       42 (0x2a)
    .maxstack  2
    .locals init ([0] int32 number,
             [1] bool CS$4$0000)
    IL_0000:  nop
    IL_0001:  ldc.i4.s   42
    IL_0003:  stloc.0
    IL_0004:  ldloc.0
    IL_0005:  ldc.i4.2
    IL_0006:  rem
    IL_0007:  ldc.i4.0
    IL_0008:  ceq
    IL_000a:  ldc.i4.0
    IL_000b:  ceq
    IL_000d:  stloc.1
    IL_000e:  ldloc.1
    IL_000f:  brtrue.s   IL_001e

    IL_0011:  ldstr      "Hello, World!"
    IL_0016:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_001b:  nop
    IL_001c:  br.s       IL_0029

    IL_001e:  ldstr      "Hello, NDC!"
    IL_0023:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0028:  nop
    IL_0029:  ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class Rule1.Program


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file Rule1-b.res
