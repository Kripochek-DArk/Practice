open System

let inputNumbers () =
    printfn "Введите цифры (0-9). Пустая строка завершает ввод."

    Seq.unfold (fun () ->
        let rec readDigit () =
            printf "Введите цифру: "
            let line = Console.ReadLine()

            if String.IsNullOrWhiteSpace line then
                None
            else
                match Int32.TryParse line with
                | true, digit when digit >= 0 && digit <= 9 ->
                    Some(digit, ())
                | _ ->
                    printfn "Ошибка: нужно ввести одну цифру от 0 до 9"
                    readDigit ()

        readDigit ()
    ) ()

[<EntryPoint>]
let main args =

    let result =
        inputNumbers ()
        |> Seq.fold (fun acc x ->
            if x % 2 = 0 then
                acc * 10 + x
            else
                acc
        ) 0

    printfn "Составленное число из чётных цифр: %d" result
    0