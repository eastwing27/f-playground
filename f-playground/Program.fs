// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open Helpers.Misc

[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    //0 // return an integer exit code
    let print m = 
        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + m.ToString())

    let sum, message = addMem 12 134
    print sum
    print message

    let person1 = {
        name = "Vasya"
        age = 20
    }

    let person2 = {
        name = "Zhopa"
        age = 42
    }

    print (isHim person1 "Vasya" 20)
    print (isHim person2 "Zhopa" 100)
    print (isHim person2 "Adelaida" 1)

    Console.ReadKey() |> ignore
    0