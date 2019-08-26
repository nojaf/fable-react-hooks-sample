module ReactHooksSample.UseReducer

open Fable.React
open Fable.React.Props
open ReactHooksSample.Bindings

type Msg =
    | Increase
    | Decrease
    | Reset

type Model = { Value : int }

let intialState = { Value = 0 }

let update model msg =
    match msg with
    | Increase -> { model with Value = model.Value + 1 }
    | Decrease -> { model with Value = model.Value - 1 }
    | Reset -> intialState

let reducerComponent() =
    let (model, dispatch) = useReducer update intialState

    div [] [
        button [ ClassName "button"; OnClick(fun _ -> dispatch Increase) ] [ str "Increase" ]
        button [ ClassName "button"; OnClick(fun _ -> dispatch Decrease) ] [ str "Decrease" ]
        button [ ClassName "button"; OnClick(fun _ -> dispatch Reset) ] [ str "Reset" ]
        p [ ClassName "title is-2 has-text-centered" ] [ sprintf "%i" model.Value |> str ]
    ]
