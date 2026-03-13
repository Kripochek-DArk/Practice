open System
open System.IO

let printSeq inputSeq = 
    for x in inputSeq do
        printfn "\t%s" x

let getExtensions dir =
    Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories)
    |> Seq.map (fun file -> Path.GetExtension(file).ToLower())
    |> Seq.filter (fun ext -> ext <> "")
    |> Seq.countBy id
    |> Seq.sortBy snd

let findRarestExtensions dir =
    let extCounts = getExtensions dir
    
    if extCounts |> Seq.isEmpty then
        printfn "В каталоге '%s' нет файлов с расширениями" dir
    else
        let minCount = extCounts |> Seq.minBy snd |> snd
        let rarest = extCounts |> Seq.filter (fun (_, count) -> count = minCount)
        
        printfn "В каталоге '%s' и его подкаталогах:" dir
        printfn "Расширения, которые встречаются реже всего (по %d раз):" minCount
        rarest |> Seq.iter (fun (ext, count) -> printfn "  %s" ext)

[<EntryPoint>]
let main argv = 
    printf "Введите путь к каталогу: "
    let dir = Console.ReadLine()
    
    if Directory.Exists(dir) then
        findRarestExtensions dir
    else
        printfn "Ошибка: Каталог '%s' не существует" dir
    
    0