namespace Library

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Types

module UI =
    module R = Fable.Helpers.React
    open R.Props

    type [<Pojo>] Props = {
        person : Person
    }
    type [<Pojo>] State = {
        person : Person
    }

    type Form(props, ctx) = 
        inherit React.Component<Props, State>(props)
        do base.setInitState({person=props.person})

        member this.render () =
            R.div [] [
                R.h1 [] [ R.str "Welcome to JavaScript Interop between F# (Fable) and Rust (through Emscripten) Demo" ]
                R.hr [] []
                R.label [] [ R.str "Name" ]
                R.input [
                    Type "text"
                    DefaultValue (U2.Case1 this.state.person.name)
                    Name "name"
                ][] 
            ]

    let render(person:Person) = 
        ReactDom.render(
            R.com<Form,Props, State> {person=person} [],
            Browser.document.getElementsByClassName("app").[0]
        )
