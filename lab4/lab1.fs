open System

type Tree =
    | Empty
    | Node of int * Tree * Tree

let rec insert value tree =
    match tree with
    | Empty ->
        Node(value, Empty, Empty)
    | Node(current, left, right) ->
        if value < current then
            Node(current, insert value left, right)
        else
            Node(current, left, insert value right)

let buildTree (arr: int[]) =
    Array.fold (fun tree value -> insert value tree) Empty arr

let rec readNumbers () =
    printf "Введите количество элементов: "
    let input = Console.ReadLine()

    match Int32.TryParse input with
    | true, value when value > 0 ->
        let rnd = Random()
        Array.init value (fun _ -> rnd.Next(1, 100))

    | _ , _ ->
        printfn "Некорректный ввод, попробуйте снова"
        readNumbers ()

let rec mapTree transform tree =
    match tree with
    | Empty -> Empty
    | Node(value, left, right) ->
        Node(
            transform value,
            mapTree transform left,
            mapTree transform right
        )

let transformNumber number =
    number
    |> string
    |> Seq.map (fun symbol ->
        let digit = int symbol - int '0'
        if digit = 0 then
            '1'
        else
            char (digit - 1 + int '0'))
    |> Seq.toArray
    |> String
    |> int

let transformTree tree =
    mapTree transformNumber tree

let rec printTree indent tree =
    match tree with
    | Empty -> ()
    | Node(value, left, right) ->
        printTree (indent + "   ") right
        printfn "%s%d" indent value
        printTree (indent + "   ") left

[<EntryPoint>]
let main args =

    let values = readNumbers ()
    let tree = buildTree values

    printfn "Исходное дерево:"
    printTree "" tree
    printfn ""

    let newTree = transformTree tree

    printfn "Новое дерево:"
    printTree "" newTree
    printfn ""

    0