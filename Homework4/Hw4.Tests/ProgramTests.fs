module Hw4.Tests.ProgramTests

open System
open Hw4.Program
open Microsoft.VisualStudio.TestPlatform.TestHost
open Xunit

[<Theory>]
[<InlineData("a", "b", "q")>]
[<InlineData("a", "+", "q")>]
[<InlineData("a", "+", "3")>]
[<InlineData("1", "+", "q")>]
[<InlineData("a", "b", "c", "q")>]
[<InlineData("1", ";", "3")>]
let ``Incorrect data throw Exception``([<ParamArray>] values:  string[] ) =
    //arrange/act/assert
    Assert.Equal(-1, main values);


[<Fact>]
let ``Correct data give valid answer``() =
    // arrange
    let args = [|"10"; "+"; "5"|]
    
    // act/assert
    Assert.Equal(0, main args)
    
    
    


