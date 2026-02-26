    printfn "Чтобы закончить ввод продолжите ничего не вводя"
open System

let tryParseInt (input:string)  = 
    match System.Int32.TryParse input with 
    | true, _ -> true
    | false, _ -> false



let rec inputNumbers spisok = 
    printf "Введите число : "
    let number = Console.ReadLine()
    if String.IsNullOrWhiteSpace number then
        List.rev spisok
    else 
        if tryParseInt number then
            let intNumber =  int number
            if(intNumber < 10 && intNumber >= 0) then
                inputNumbers (int number :: spisok)
            else 
                printfn "число не десятичное"
                inputNumbers spisok 
        else
            printfn "введенно не число"
            inputNumbers spisok 


[<EntryPoint>]
let main args = 
    printfn "Чтобы закончить ввод продолжите ничего не вводя"
    let numbers = inputNumbers []

    let newNumber =
        numbers |> List.fold(fun acc x ->
        if x % 2 = 0 then 
            acc * 10 + x
        else 
            acc
        ) 0

    printfn "Новое число : %i" newNumber 
    0