namespace Library

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Types
open Fable.PowerPack

module Util =
    let load<'T> key =
        Browser.localStorage.getItem(key) |> unbox
        |> Option.map (JS.JSON.parse >> unbox<'T>)

    let save key (data: 'T) =
        Browser.localStorage.setItem(key, JS.JSON.stringify data)
        
module Impl = 
    open Fable.Import.Browser        
    let age(person:Person) = 
        console.log("age function called")
        console.log(person)
        person.age
        
    let loadPerson() = 
        promise {
            console.log( "loadPerson function called" )
            return { name="Jack Sparrow"; age=29; phones=[ "555 123 456"; "123 456 789" ] }
        }
