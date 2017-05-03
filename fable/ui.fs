namespace Library

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

module UI =
    module R = Fable.Helpers.React
    open R.Props

    type Form(props, ctx) = 
        inherit React.Component<obj, obj>()
        do base.setInitState()

        member this.render () =
            R.div [] [
                R.h1 [] [ R.str "Welcome to JavaScript Interop between F# (Fable) and Rust (through Emscripten) Demo" ]
            ]

    let render() = 
        ReactDom.render(
            R.com<Form,obj, obj> null [],
            Browser.document.getElementsByClassName("app").[0]
        )
