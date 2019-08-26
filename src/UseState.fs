module ReactHooksSample.UseState

open Fable.Core.JsInterop
open Fable
open Fable.React
open Fable.React.Props
open ReactHooksSample.Bindings
open Browser.Types

let useInputValue (initialValue : string) =
    let (value, setValue) = useState (initialValue)
    let onChange (e : Event) =
        let value : string = e.target?value
        setValue (value)

    let resetValue() = setValue (System.String.Empty)

    value, onChange, resetValue

type FormProps = { OnSubmit : string -> unit }

let formComponent (props : FormProps) =
    let (value, onChange, resetValue) = useInputValue ""

    let onSubmit (ev : Event) =
        ev.preventDefault()
        props.OnSubmit(value)
        resetValue()

    form [ OnSubmit onSubmit; ] [
        input [ Value value; OnChange onChange; Placeholder "Enter todo"; ClassName "input" ]
    ]


type Todo = { Text : string; Complete : bool }

let appComponent() =
    let (todos, setTodos) = useState<Todo list> ([])

    let toggleComplete i =
        todos
        |> List.mapi (fun k todo ->
            if k = i then { todo with Complete = not todo.Complete } else todo
        )
        |> setTodos

    let renderTodos =
        todos
        |> List.mapi (fun idx todo ->
            let style =
                CSSProp.TextDecoration(if todo.Complete then "line-through" else "")
                |> List.singleton

            let key = sprintf "todo_%i" idx

            div [ Key key; OnClick(fun _ -> toggleComplete idx) ] [
                label [ ClassName "checkbox"; Style style ] [
                    input [ Type "checkbox"; Checked todo.Complete; OnChange (fun _ -> toggleComplete idx) ]
                    str todo.Text
                ]
            ]
        )

    let onSubmit text =
            { Text = text; Complete = false }
            |> List.singleton
            |> (@) todos
            |> setTodos

    div [] [
        h1 [ Class "title is-4" ] [ str "Todos" ]
        ofFunction formComponent { OnSubmit = onSubmit } []
        div [ ClassName "notification" ] renderTodos
    ]
