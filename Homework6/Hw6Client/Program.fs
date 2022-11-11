open System
open System.Net.Http
open Hw6.Parser
open Hw6.Calculator
open System.Linq

let fetchAsync(url:string) =
    async {
        while true do
            try
                let args = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                match parseCalcArguments args with
                | Ok (val1, operation, val2) ->
                    let uri = new Uri(url + $"/calculate?value1={val1}&operation={operation}&value2={val2}")
                    let handler = new HttpClientHandler()
                    let client = new HttpClient(handler)
                    let! response = client.GetAsync(uri) |> Async.AwaitTask
                    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                    printfn $"{content}"
                | Error error -> printfn $"{error}"
            with
                | ex -> printfn $"{ex.InnerException.Message}";
        }
    
[<EntryPoint>]
let main _ =
    "https://localhost:5001" |> fetchAsync |> Async.RunSynchronously
    0