.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
     .method public static void ParseCalcArguments(string[] args,  
    [out] float64& val1, 
    [out] valuetype Hw2.CalculatorOperation& operation, 
    [out] float64& val2) cil managed
    {
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse.s Throw
        
        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldarg.1
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse.s Throw
        
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse.s Throw
        
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        ret
    
        Throw:
        ldstr "Govno sdelal Parse Args"
        call void [mscorlib]System.Console::WriteLine(string)
        newobj instance void [mscorlib]System.ArgumentException::.ctor()
        throw
    }
    
    .method private static bool IsArgLengthSupported(string[] args) cil managed
    {
        .maxstack 8
    
        ldarg.0
        ldlen
        conv.i4
        ldc.i4.3
        ceq
        ret
    }
    
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg) cil managed
    {
        .maxstack 2
        .locals init ([0] valuetype Hw2.CalculatorOperation result)
        
        ldarg.0
        ldstr "+"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Plus
        
        ldarg.0
        ldstr "-"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Minus
        
        ldarg.0
        ldstr "*"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Multiply
        
        ldarg.0
        ldstr "/"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Divide
        
        ldstr "Govno sdelal Parser"
        call void [mscorlib]System.Console::WriteLine(string)
        newobj instance void [mscorlib]System.InvalidOperationException::.ctor()
        throw
        
        Plus:
        ldc.i4.0
        stloc.0
        ldloc.0
        ret
        
        Minus:
        ldc.i4.1
        stloc.0
        ldloc.0
        ret
        
        Multiply:
        ldc.i4.2
        stloc.0
        ldloc.0
        ret
        
        Divide:
        ldc.i4.3
        stloc.0
        ldloc.0
        ret
    }
}


