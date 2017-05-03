#r "../node_modules/fable-core/Fable.Core.dll"

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

module Util =
    let load<'T> key =
        Browser.localStorage.getItem(key) |> unbox
        |> Option.map (JS.JSON.parse >> unbox<'T>)

    let save key (data: 'T) =
        Browser.localStorage.setItem(key, JS.JSON.stringify data)
        
open Fable.Import.Browser        

type Person = {
    name: String;
    age: int;
    phones: string list;
}
    with 
        override this.ToString() =
            toJson this    

        member this.toString() =
            toJson this    

let fableFunc(person:Person) = 
    console.log("Fable code invoked again")
    console.log(person)
    System.DateTime.Now |> Util.save "current-time"
    person.age
    
let fableFuncS() : Person = 
    console.log( "Fable code invoked again" )
    System.DateTime.Now |> Util.save "current-time"
    { name="John Smith"; age=29; phones=[ "555 123 456"; "123 456 789" ] }
