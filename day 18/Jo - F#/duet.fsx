#r @"..\..\dependencies\Jo\.paket\packages\Unquote\lib\net45\Unquote.dll"
open Swensen.Unquote

type RegisterName = RegisterName of char
type Registers = Map<RegisterName, int>
type Value = | FromRegister of RegisterName | RawValue of int
let registerName = RegisterName
let registerValue r = FromRegister <| RegisterName r
let integerValue i = RawValue i

type Instruction = 
    | Snd of Value
    | Set of (RegisterName * Value)
    | Add of (RegisterName * Value)
    | Mul of (RegisterName * Value)
    | Mod of (RegisterName * Value)
    | Rcv of RegisterName
    | Jgz of (Value * Value)

type State = { LastSoundPlayed : int option; Recovered : int Option; Pointer : int; Registers : Registers }
let init = { LastSoundPlayed = None; Recovered = None; Pointer = 0; Registers = Map.empty }

let split (delims : string array) (text : string) = 
    text.Split(delims, System.StringSplitOptions.RemoveEmptyEntries) 
    |> List.ofArray

let tryParseInt text = 
    text
    |> Seq.map string
    |> String.concat ""
    |> System.Int32.TryParse

let parseValue (text : char list) = 
    match tryParseInt text with
    | true, parsed -> integerValue parsed
    | _ -> registerValue (char text.[0])

let parseInstruction text =
    match text |> Seq.toList with
    | 's'::'n'::'d' :: ' ' :: freq -> Snd <| parseValue freq
    | 's'::'e'::'t' :: ' ' :: reg :: ' ' :: v -> Set <| (registerName reg, parseValue v)
    | 'a'::'d'::'d' :: ' ' :: reg :: ' ' :: v -> Add <| (registerName reg, parseValue v)
    | 'm'::'u'::'l' :: ' ' :: reg :: ' ' :: v -> Mul <| (registerName reg, parseValue v)
    | 'm'::'o'::'d' :: ' ' :: reg :: ' ' :: v -> Mod <| (registerName reg, parseValue v)
    | 'r'::'c'::'v' :: ' ' :: reg :: [] -> Rcv <| registerName reg
    | 'j'::'g'::'z' :: ' ' :: v1 :: ' ' :: v2 -> Jgz <| (parseValue [v1] (*OUCH, incorrect when multi-digit*), parseValue v2)
    | unknown -> failwithf "Failed to parse %A" unknown

let parse instructions =
    instructions
    |> split [|"\n"; "\r"|]
    |> List.map parseInstruction

let getRegisterValue register (registers : Registers) =
    registers
    |> Map.tryFind register
    |> (fun o -> defaultArg o 0)

let getValue v registers = 
    match v with
    | FromRegister registerName -> getRegisterValue registerName registers
    | RawValue rawValue -> rawValue

let setValue register v state = 
    let newRegisters =
        state.Registers 
        |> Map.add register v
    { state with Registers = newRegisters }

let incrementPointer increment state = 
    {state with Pointer = state.Pointer + increment}

let set reg value state = 
    let v = getValue value state.Registers
    state
    |> setValue reg v
    |> incrementPointer 1

let snd reg state = 
    let sound = 
        match reg with
        | FromRegister registerName -> getRegisterValue registerName state.Registers
        | RawValue rawValue -> rawValue
    { state with LastSoundPlayed = Some sound}
    |> incrementPointer 1

let apply operator reg value state = 
    let oldValue = getRegisterValue reg state.Registers
    let v = getValue value state.Registers
    let newValue = operator oldValue v
    state 
    |> setValue reg newValue
    |> incrementPointer 1

let add = apply (+)
let mul = apply (*)
let modulo = apply (%)

let rcv registerName state = 
    let v = getRegisterValue registerName state.Registers
    if v = 0 then
        state
        |> incrementPointer 1
    else
        {state with Recovered = state.LastSoundPlayed}
        |> incrementPointer 1

let jgz cond offset state =
    let v = getValue cond state.Registers
    if v > 0 then
        let offsetV = getValue offset state.Registers
        state
        |> incrementPointer offsetV
    else
        state
        |> incrementPointer 1

let runInstruction instruction state = 
    match instruction with
    | Snd reg -> snd reg state
    | Set (reg, value) -> set reg value state
    | Add (reg, value) -> add reg value state
    | Mul (reg, value) -> mul reg value state
    | Mod (reg, value) -> modulo reg value state
    | Rcv reg -> rcv reg state
    | Jgz (cond, offset) -> jgz cond offset state

let print state =
    let registers =
        state.Registers
        |> Map.toList
        |> List.map (fun (RegisterName r,v) -> sprintf "%A,%d" r v)
        |> String.concat ""
    sprintf 
        "Pointer: %d, LastPlayed: %A, Recovered: %A, Registers: %s" 
        state.Pointer 
        state.LastSoundPlayed 
        state.Recovered 
        registers

let rec run instructions state =
    let l = instructions |> Seq.length
    if state.Pointer < 0 || state.Pointer >= l then
        state
    else
        let i = instructions |> List.item state.Pointer
        printfn "Executing %A on %s" i (print state)
        let newState = runInstruction i state
        match i with
        | Rcv _ when Option.isSome newState.Recovered -> newState
        | _ -> run instructions newState

printf "Testing..."

test <@ parse "snd 1" = [Snd <| integerValue 1] @>
test <@ parse "snd z" = [Snd <| registerValue 'z'] @>
test <@ parse "set a 3" = [Set <| (registerName 'a', integerValue 3)] @>
test <@ parse "set a c" = [Set <| (registerName 'a', registerValue 'c')] @>
test <@ parse "add g 331" = [Add <| (registerName 'g', integerValue 331)] @>
test <@ parse "add g x" = [Add <| (registerName 'g', registerValue 'x')] @>
test <@ parse "mul q -28" = [Mul <| (registerName 'q', integerValue -28)] @>
test <@ parse "mul r t" = [Mul <| (registerName 'r', registerValue 't')] @>
test <@ parse "mod z 89" = [Mod <| (registerName 'z', integerValue 89)] @>
test <@ parse "mod v p" = [Mod <| (registerName 'v', registerValue 'p')] @>
test <@ parse "rcv z" = [Rcv <| registerName 'z'] @>
test <@ parse "jgz 1 2" = [Jgz <| (integerValue 1, integerValue 2)] @>
test <@ parse "jgz i b" = [Jgz <| (registerValue 'i', registerValue 'b')] @>
test <@ parse "jgz k 0" = [Jgz <| (registerValue 'k', integerValue 0)] @>
test <@ parse "snd a\nsnd b" = [Snd <| registerValue 'a'; Snd <| registerValue 'b'] @>

test <@ (run [Set (registerName 'a', integerValue 10)] init).Registers |> Map.find (RegisterName 'a') = 10 @>
test <@ (run [Set (registerName 'a', integerValue 10); Set (registerName 'b', registerValue 'a')] init).Registers |> Map.find (RegisterName 'b') = 10 @>

test <@ (run [Set (registerName 'a', integerValue 3); Snd <| registerValue 'a'] init).LastSoundPlayed = Some 3 @>

test <@ (run [Set (registerName 'a', integerValue 3); Add <| (registerName 'a', integerValue 2)] init).Registers |> Map.find (RegisterName 'a') = 5 @>
test <@ (run [Set (registerName 'a', integerValue 3); Set (registerName 'b', integerValue 2); Add <| (registerName 'a', registerValue 'b')] init).Registers |> Map.find (RegisterName 'a') = 5 @>

test <@ (run [Set (registerName 'a', integerValue 3); Mul <| (registerName 'a', integerValue 2)] init).Registers |> Map.find (RegisterName 'a') = 6 @>
test <@ (run [Set (registerName 'a', integerValue 3); Set (registerName 'b', integerValue 2); Mul <| (registerName 'a', registerValue 'b')] init).Registers |> Map.find (RegisterName 'a') = 6 @>

test <@ (run [Set (registerName 'a', integerValue 5); Mod <| (registerName 'a', integerValue 2)] init).Registers |> Map.find (RegisterName 'a') = 1 @>
test <@ (run [Set (registerName 'a', integerValue 5); Set (registerName 'b', integerValue 2); Mod <| (registerName 'a', registerValue 'b')] init).Registers |> Map.find (RegisterName 'a') = 1 @>

test <@ (run [Rcv <| registerName 'a'] init).Recovered = None @>
test <@ (run [Set (registerName 'a', integerValue 1); Rcv <| registerName 'a'] init).Recovered = None @>
test <@ (run [Set (registerName 'a', integerValue 1); Snd <| integerValue 1337; Rcv <| registerName 'a'] init).Recovered = Some 1337 @>

test <@ (run [Jgz <| (integerValue 1, integerValue 3)] init).Pointer = 3 @>
test <@ (run [Set (registerName 'a', integerValue 3); Jgz <| (integerValue 1, registerValue 'a')] init).Pointer = 4 @>
test <@ (run [Set (registerName 'a', integerValue 1); Jgz <| (registerValue 'a', integerValue 3)] init).Pointer = 4 @>
test <@ (run [Set (registerName 'a', integerValue 1);  Set (registerName 'b', integerValue 3); Jgz <| (registerValue 'a', registerValue 'b')] init).Pointer = 5 @>
printfn "Done"

let example = "set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2"

test <@ Some 4 = (run (example |> parse) init).Recovered @>
let input = System.IO.File.ReadAllText( __SOURCE_DIRECTORY__ + "\input.txt" )
let instructions = input |> parse

let part1 = run instructions init