open System

type Tree =
    | Empty
    | Node of int * Tree * Tree

let rec buildTree (arr: int[]) index =
    if index >= arr.Length then
        Empty
    else
        Node(
            arr.[index],
            buildTree arr (2 * index + 1),
            buildTree arr (2 * index + 2)
        )

let readNumbers () =
    let rec loop acc =
        printf "Введите число: "
        let input = Console.ReadLine()

        if String.IsNullOrWhiteSpace input then
            List.rev acc
        else
            match Int32.TryParse input with
            | true, value when value >= 0 ->
                loop (value :: acc)

            | true, value when value < 0 ->
                printfn "отрицательные числа запрещены"
                loop acc

            | false, _ ->
                printfn "введено не число"
                loop acc

    loop [] |> List.toArray
let rec mapTree changeFunction tree =
    match tree with
    | Empty -> Empty
    | Node(value, left, right) ->
        Node(
            changeFunction value,
            mapTree changeFunction left,
            mapTree changeFunction right
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
        printfn "%s%A" indent value
        printTree (indent + "   ") left

[<EntryPoint>]
let main args =
    printfn "Введите числа (пустая строка — конец ввода)"

    let values = readNumbers ()

    let tree = buildTree values 0

    printfn "Исходное дерево:"
    printTree "" tree
    printfn ""

    let newTree = transformTree tree

    printfn "Новое дерево:"
    printTree "" newTree
    printfn ""

    0