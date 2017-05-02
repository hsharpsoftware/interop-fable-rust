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
let fableFunc() = 
    console.log( "Fable code invoked again" )
    System.DateTime.Now |> Util.save "current-time"
    2017
    
let fableFuncS() = 
    console.log( "Fable code invoked again" )
    System.DateTime.Now |> Util.save "current-time"
    "Hello World!"

