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

    print (addTen 2)
    print (addTen)

    Console.ReadKey() |> ignore
    0