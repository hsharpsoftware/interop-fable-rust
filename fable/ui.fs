namespace Library

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Types

module UI =
    module R = Fable.Helpers.React
    open R.Props

    [<Emit("Module.ccall('process_state', 'string', ['string'], [$0]);")>]
    let processState(s:string) : String = jsNative

    type Form(props, ctx) = 
        inherit React.Component<Props, State>(props)
        do base.setInitState({state=props.state;person=None})

        member this.componentDidMount () =
            let processedState : State = processState(this.state |> toJson) |> ofJson
            this.setState( processedState )

        member this.render () =
            let header = [
                R.h1 [] [ R.str "Welcome to JavaScript Interop between F# (Fable) and Rust (through Emscripten) Demo" ]
                R.hr [] []
            ]

            let personEdit = 
                if this.state.state = IState.Loaded then
                    [
                        R.label [] [ R.str "Name" ]
                        R.input [
                            Type "text"
                            DefaultValue (U2.Case1 this.state.person.Value.name)
                            Name "name"
                        ] []
                    ]
                else []

            let footer = [ 
                R.str (
                    match this.state with
                    | Initial -> "Initial" 
                    | Loading -> "Loading"
                    | Loaded -> "Loaded"
                ) 
            ]

            let content = [header;personEdit;footer] |> List.concat
            R.div [] content

    let render(person:Person) = 
        ReactDom.render(
            R.com<Form,Props, State> {state=IState.Initial} [],
            Browser.document.getElementsByClassName("app").[0]
        )
