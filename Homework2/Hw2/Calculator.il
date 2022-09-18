.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public static float64 Calculate(float64 value1, valuetype Hw2.CalculatorOperation operation, float64 value2) cil managed
    {
        .maxstack 2
        .locals init([0] float64 result)
        ldarg.1
        ldc.i4.0
        beq Add
        
        ldarg.1
        ldc.i4.1
        beq Subtract
        
        ldarg.1
        ldc.i4.2
        beq Multiply
        
        ldarg.1
        ldc.i4.3
        beq Divide
        
        newobj instance void [mscorlib]System.ArgumentOutOfRangeException::.ctor()
        throw
        
        Add:
        ldarg.0
        ldarg.2
        add
        stloc.0
        ldloc.0
        ret
        
        Subtract:
        ldarg.0
        ldarg.2
        sub
        stloc.0
        ldloc.0
        ret
        
        Multiply:
        ldarg.0
        ldarg.2
        mul
        stloc.0
        ldloc.0
        ret
        
        Divide:
        ldarg.0
        ldarg.2
        div
        stloc.0
        ldloc.0
        ret
    }

}



