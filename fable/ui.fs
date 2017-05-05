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
            printfn "New state %A" processedState
            this.setState( processedState )            

        member this.render () =
            printfn "Will render state %A" this.state
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

            printf "Application state is %A..." this.state.state
            let actualState = 
                match this.state.state with
                | IState.Initial -> "Initial" 
                | IState.Loading -> "Loading"
                | IState.Loaded -> "Loaded"        
                | _ -> "Unknown"    

            printfn "... which is %A" actualState

            let footer = [ R.str ( actualState) ]

            let content = [header;personEdit;footer] |> List.concat
            R.div [] content

    let render(person:Person) = 
        ReactDom.render(
            R.com<Form,Props, State> {state=IState.Initial} [],
            Browser.document.getElementsByClassName("app").[0]
        )
