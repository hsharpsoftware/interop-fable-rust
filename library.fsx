#r "node_modules/fable-core/Fable.Core.dll"
#load "node_modules/fable-import-react/Fable.Import.React.fs"
#load "node_modules/fable-import-react/Fable.Helpers.React.fs"

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
    console.log( "Fable code invoked" )
    42
    