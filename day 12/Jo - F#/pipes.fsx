#r @"..\..\dependencies\Jo\.paket\packages\Unquote\lib\net45\Unquote.dll"
open Swensen.Unquote
open System

let split (delimiter : string) (text : string) =
    text.Split([|delimiter|], StringSplitOptions.RemoveEmptyEntries)
    |> List.ofSeq

type Connection = int*int

let parse input =
    let parseInt = Int32.Parse
    let parseLine line =
        let src :: destinations :: _ = split " <-> " line
        destinations
        |> split ","
        |> List.map (fun d -> (parseInt src, parseInt d))

    input
    |> split "\n"
    |> List.collect parseLine

let input = @"0 <-> 1,2
1 <-> 2,3
2 <-> 4"

parse input