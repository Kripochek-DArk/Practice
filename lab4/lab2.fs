open System

type Tree =
    | Empty
    | Node of string * Tree * Tree

let rec insert value tree =
    match tree with
    | Empty ->
        Node(value, Empty, Empty)
    | Node(current, left, right) ->
        if value < current then
            Node(current, insert value left, right)
        else
            Node(current, left, insert value right)

let buildTree (arr: string[]) =
    Array.fold (fun tree value -> insert value tree) Empty arr

let rec readStrings () =
    printf "Введите количество строк: "
    let input = Console.ReadLine()

    match Int32.TryParse input with
    | true, count when count > 0 ->
        let rnd = Random()
        let chars = "abcdefghijklmnopqrstuvwxyz"

        let randomString () =
            let length = rnd.Next(3, 10)
            String(
                Array.init length (fun _ ->
                    chars.[rnd.Next(chars.Length)])
            )

        Array.init count (fun _ -> randomString())

    | _ ->
        printfn "Некорректный ввод, попробуйте снова"
        readStrings ()

let rec foldTree folder state tree =
    match tree with
    | Empty -> state
    | Node(value, left, right) ->
        let leftState = foldTree folder state left
        let currentState = folder leftState value
        foldTree folder currentState right

let countStringsEndingWith symbol tree =
    foldTree
        (fun count value ->
            if not (String.IsNullOrEmpty value) && value.[value.Length - 1] = symbol then
                count + 1
            else
                count)
        0
        tree

let rec printTree indent tree =
    match tree with
    | Empty -> ()
    | Node(value, left, right) ->
        printTree (indent + "   ") right
        printfn "%s%s" indent value
        printTree (indent + "   ") left

[<EntryPoint>]
let main args =
    printfn "Будут сгенерированы случайные строки"
    let values = readStrings ()

    printfn "Сгенерированные строки: %A" values

    let tree = buildTree values

    printfn "Исходное дерево:"
    printTree "" tree
    printfn ""

    printf "Введите символ: "
    let inputChar = Console.ReadLine()

    if String.IsNullOrWhiteSpace inputChar || inputChar.Length <> 1 then
        printfn "Ошибка: нужно ввести ровно один символ"
    else
        let symbol = inputChar.[0]
        let result = countStringsEndingWith symbol tree
        printfn "Количество узлов, оканчивающихся на символ '%c': %d" symbol result

    0