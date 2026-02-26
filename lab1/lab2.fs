open System

let rec countOfString amount =
    printf "Введите строчку : "
    let string = Console.ReadLine()
    if String.IsNullOrWhiteSpace string then
        amount
    else
        countOfString (amount + 1)


[<EntryPoint>]
let main args = 
    printfn "Чтобы закончить ввод, введите пустую строчку"
    let amount = countOfString 0
    printf "Количество строк : %i" amount
    
    0