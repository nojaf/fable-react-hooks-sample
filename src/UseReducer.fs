module ReactHooksSample.UseReducer

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import.React
open ReactHooksSample.Bindings

type Msg =
    | Increase
    | Decrease
    | Reset

type Model = { Value: int }

let intialState = { Value = 0}
let update model msg =
    match msg with
    | Increase -> { model with Value = model.Value + 1}
    | Decrease -> { model with Value = model.Value - 1}
    | Reset -> intialState

let view () =
    let (model, dispatch) = useReducer update intialState

    div [ClassName "container"] [
        button [OnClick (fun _ -> dispatch Increase)] [str "Increase"]
        button [OnClick (fun _ -> dispatch Decrease)] [str "Decrease"]
        button [OnClick (fun _ -> dispatch Reset)] [str "Reset"]
        p [] [sprintf "%i" model.Value |> str]
    ]