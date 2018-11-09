module ReactHooksSample.App

open Fable.Helpers.React
open Fable.Helpers.React.Props
open ReactHooksSample
open ReactHooksSample.Bindings


let app () =
    let (page, setPage) = useState "useState()"
    
    let tabs =
        [ "useState()" ; "useReducer()" ; "useEffect()" ]
        |> List.map (fun tab ->
            li [ Class (if page = tab then "is-active" else "") ; Key tab ] [
                a [ Href "#" ; OnClick (fun _ -> setPage tab) ] [ str tab ]
            ]
        )
    
    let activePage =
        match page with
        | "useState()" -> ofFunction UseState.appComponent () []
        | "useReducer()" -> ofFunction UseReducer.reducerComponent () []
        | _ -> str "other page"
    
    div [] [
        div [ ClassName "tabs" ] [
            ul [] tabs
        ]
        activePage
    ]


mountById "app" (ofFunction app () [])
