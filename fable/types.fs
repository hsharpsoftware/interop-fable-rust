namespace Library

open System
open Fable.Import.Browser        
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

module Types =
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