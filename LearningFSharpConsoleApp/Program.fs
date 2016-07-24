// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

//type TaskSession = { Start:System.DateTime; End:System.DateTime }
open System.Collections.Generic
open System.Linq

type TaskSession(startTime: string, endTime: string) as this =
    let d = System.DateTime.Today.ToString("yyyy-MM-dd" + " ")
    let Start = System.DateTime.Parse( d + startTime)
    let End = System.DateTime.Parse(d + endTime)
    member this.GetElapsedHours : double = 
        (End - Start).TotalHours

let ReportTotalHours(sessions: List<TaskSession>) : double = 
    sessions |> Seq.sumBy(fun x -> x.GetElapsedHours)

let ReportTotalHours'(session: TaskSession) : double = 
    let lst = new List<TaskSession>()
    lst.Add(session)
    ReportTotalHours lst

[<EntryPoint>]
let main argv = 
//    let x = new TaskSession("9:00", "10:00")
//    printfn "%A" x
    printfn "Multiple Tasks Single Sessions"

    let s = new TaskSession("9:00", "10:00");

    printfn "%f" (ReportTotalHours' s)

    printfn "\n\nOne Task Multiple Sessions"

    let lst = new List<TaskSession>()

    lst.Add(new TaskSession("09:00", "10:00"))
    lst.Add(new TaskSession("10:00", "11:00"))
    lst.Add(new TaskSession("11:00", "12:00"))

    printfn "%f" (ReportTotalHours lst)
    
    printfn "Press any key to continue..."
    
    System.Console.ReadLine()
    0 // return an integer exit code

