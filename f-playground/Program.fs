// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open Helpers.Maths
open Helpers.Bools

[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    //0 // return an integer exit code
    let print m = 
        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + m)

    print "Hi!"
    print "2 + 2 is 4"
    Console.ReadKey() |> ignore
    add 1 -1