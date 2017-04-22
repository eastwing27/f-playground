namespace Helpers

open System.Linq

module Maths = 
    let add num1 num2 = 
        num1 + num2

module Bools =
    let any (items: seq<int>) =
        items.Any()

    let isSumEven n1 n2 =
        let sum = Maths.add n1 n2
        sum % 2 = 0

//module Tuples =
//    let 
