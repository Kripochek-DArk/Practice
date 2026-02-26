open System

let tryParseInt (input:string)  = 
    match System.Int32.TryParse input with 
    | true, _ -> true
    | false, _ -> false
    
let rec listOfNumber list =
    printf "Введите число : "
    let input = Console.ReadLine() 
    if String.IsNullOrWhiteSpace input then
        list
    else
        if tryParseInt input then
            listOfNumber(( int input - 1 ) :: list )
        else 
            printfn "Введенно не число"
            listOfNumber list

[<EntryPoint>]
let main args =
    let numbers = []
    printfn "Чтобы закончить ввод, продолжите ничего не вводя"
    let newNumbers = listOfNumber numbers

    printf "Список : "
    newNumbers |> List.iter(printf " %i,")

    0
    
