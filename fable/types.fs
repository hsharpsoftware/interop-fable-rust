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

    type IState = 
        Initial = 0 | Loading = 1 | Loaded = 2

    type [<Pojo>] Props = {  
        state : IState
    }
        with 
            override this.ToString() =
                toJson this    

            member this.toString() =
                toJson this

    type [<Pojo>] State = {  
        state : IState
        person : Person Option
    }        
        with 
            override this.ToString() =
                toJson this    

            member this.toString() =
                toJson this