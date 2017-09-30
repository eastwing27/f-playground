open System
open Types

[<EntryPoint>]
let main argv = 
//-------------------------------
    let getNumberNotLesserThan x =
        printf "Input number not lesser than %A: " x
        let y = Convert.ToInt32(Console.ReadLine())
        if y < x then
            failwith "Number is lesser!"
        else
            y

//-------------------------------

    [1..100] |> List.sum |> printfn "sum=%d"

    Console.ReadKey() |> ignore
    0