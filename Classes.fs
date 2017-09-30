module Types 

open System

type Person =
    { Name: string
      Age: int
      Birth: DateTime }

type BST<'T> =
    | Empty
    | Node of 'T * BST<'T> * BST<'T>

let rec flip bst =
    match bst with
    | Empty -> bst
    | Node(item, left, right) -> Node(item, flip left, flip right)