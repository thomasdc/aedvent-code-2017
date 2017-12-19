#r @"..\..\dependencies\Jo\.paket\packages\Unquote\lib\net45\Unquote.dll"
open Swensen.Unquote

type Element =
    | Path
    | Corner
    | Letter of char
type Location = int * int
type Direction = Up| Down| Left |Right

type State = { Location : Location; Facing : Direction; Encountered : char list }

let parseElement =
    function
    | '|' 
    | '-' -> 
        Some <| Path
    | '+' -> 
        Some <| Corner
    | c when List.contains c ['A'..'Z'] -> 
        Some <| Letter c
    | _ -> 
        None

let splitLines (text : string) = text.Split([|'\n'|]) |> Array.toList

let parseLine text =
    text 
    |> Seq.toList
    |> List.map parseElement

let index grid =
    [
        for rowNumber, row in List.indexed grid do
        for columnNumber, column in List.indexed row ->
            (rowNumber, columnNumber), column
    ]

let relevantCell (location, element) =
    element 
    |> Option.map (fun el -> location, el)

let parse text = 
    text
    |> splitLines
    |> List.map parseLine
    |> index
    |> List.choose relevantCell
    |> Map.ofList

let findStart maze =
    let isStart ((row,_), element) =
        match row, element with
        | 0, Path -> true
        | _ -> false

    maze 
    |> Map.toList
    |> List.find isStart
    |> fst

let neighbourAt (r,c) =
    function
    | Up -> (r-1,c)
    | Down -> (r+1, c)
    | Left -> (r, c-1)
    | Right -> (r, c+1)

let turn currentLocation currentDirection maze = 
    let turns =
        match currentDirection with
        | Up -> [Left; Right]
        | Down -> [Left; Right]
        | Left -> [Up; Down]
        | Right -> [Up; Down]
    turns 
    |> List.filter (fun t -> maze |> Map.containsKey (neighbourAt currentLocation t))
    |> List.head

let takeStep maze state = 
    let next = neighbourAt state.Location state.Facing
    let elementAtNext = maze |> Map.tryFind next

    elementAtNext
    |> Option.map 
        (function 
            | Path -> { state with Location = next }
            | Letter l -> {state with Location = next; Encountered = state.Encountered @ [l]}
            | Corner -> {state with Location = next; Facing = turn next state.Facing maze})

let rec run maze state =
    let next = takeStep maze state
    match next with
    | None -> state
    | Some n -> run maze n

let example = @"     |          
     |  +--+    
     A  |  C    
 F---|----E|--+ 
     |  |  |  D 
     +B-+  +--+ 

"

let part1 text = 
    let maze = parse text
    let start = findStart maze
    let init = {Location = start; Encountered = []; Facing = Down}
    let completeRun = run maze init
    (completeRun).Encountered |> List.map string |> String.concat ""

part1 example

let input = System.IO.File.ReadAllText( __SOURCE_DIRECTORY__ + "\input.txt")
part1 input