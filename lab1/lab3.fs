open System

let rec addLast x list =
    match list with
    | [] -> [x]
    | head :: tail -> head :: addLast x tail

let rec remove x list =
    match list with
    | [] -> []
    | head :: tail ->
        if head = x then
            tail
        else
            head :: remove x tail

let rec contains x list =
    match list with
    | [] -> false
    | head :: tail ->
        if head = x then
            true
        else
            contains x tail

let rec append list1 list2 =
    match list1 with
    | [] -> list2
    | head :: tail ->
        head :: append tail list2

let rec getAt index list =
    match list with
    | [] -> 
        printfn "Такого индекса нету"
        None
    | head :: tail ->
        if index = 0 then
            Some head
        else
            getAt (index - 1) tail


[<EntryPoint>]
    let main args = 
    let numbers = [1; 2; 3]

    printfn "Добавление в конец"
    printf "Список до : "
    printfn "%A" numbers   
    let newList = addLast 4 numbers
    printf "Список после : "
    printfn "%A" newList        

    printfn "Удаление"
    printf "Список до : "
    printfn "%A" numbers   
    let removed = remove 2 numbers
    printf "Список после : "
    printfn "%A" removed       

    printfn "Проверка на принадлжености к списку"
    printf "Список : "
    printfn "%A" numbers 
    printfn "Введите число : "
    let input_1 = Console.ReadLine() 
    printfn "Ответ : %b" (contains input_1 numbers)   

    printfn "Соединение списков"
    let list1  = [1;2]
    let list2 = [3;4]
    printf "Список 1 : "
    printfn "%A" list1 
    printf "Список 2 : "
    printfn "%A" list2 
    let combined = append [1;2] [3;4]
    printf "Список после : "
    printfn "%A" combined     

    printfn "Нахождение списка по индексу"
    printf "Список : "
    printfn "%A" numbers 
    printf "Введите индекс : "
    let input_2 = Console.ReadLine()
    match getAt input_2 numbers with
        | Some value -> printfn "%d" value

