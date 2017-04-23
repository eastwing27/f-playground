namespace Helpers

open System.Linq

module Misc = 
    let add num1 num2 = 
        num1 + num2

    let any (items: seq<int>) =
        items.Any()

    let isSumEven n1 n2 =
        let sum = add n1 n2
        sum % 2 = 0

    let addTen num =
        (num+10, "The original value was " + num.ToString())

    let addMem n1 n2 = 
        let res = add n1 n2
        (res , "Values was " + n1.ToString() + " and " + n2.ToString())

    type Person ={
        name: string
        age: int
    }

    let isHim person name age = 
        let comp = {name = name; age = age}
        if person <> comp then
            if person.name = comp.name then
                "Hmm... Names are qeual"
            else
                "Nope, it's not him"
        else
            "It's him!"