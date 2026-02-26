open System 

let inputCorect romanNumber=
    match romanNumber with 
    | "I" | "II" | "III" | "IV" | "V" | "VI" | "VII" | "VIII" | "IX" -> true
    | _ -> false

let RomanToNumber romanNumber=
    match romanNumber with 
    | "I" -> 1
    | "II" -> 2
    | "III" -> 3
    | "IV" -> 4
    | "V" -> 5
    | "VI" -> 6
    | "VII" -> 7
    | "VIII" -> 8
    | "IX" -> 9
    | _ -> 0

let rec inputOfRoman spisok =
    printf "Введите римское число от I до IX : "
    let romanNumber = Console.ReadLine()
    if String.IsNullOrWhiteSpace romanNumber then
        spisok
    else 
        if inputCorect romanNumber then
            inputOfRoman (romanNumber :: spisok)
        else 
            printfn "Введены некоректные данные"
            inputOfRoman spisok


[<EntryPoint>]
let main args = 
    printfn "Чтобы закочить ввод продолжите ничего не вводя" 
    let romans = inputOfRoman []
    printf "Список римских чисел : "
    romans |> List.iter(printf " %s,")

    let numbers = romans |> List.map RomanToNumber

    printf "\nСписок десятичных чисел : "
    numbers |> List.iter(printf " %i,")
    0