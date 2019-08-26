module ReactHooksSample.UseEffect

open Fable.Core
open Fable.React
open Fable.React.Props
open ReactHooksSample.Bindings
open System
open Thoth.Json
open Browser.Types

let decodeRepoItem =
    Decode.field "name" Decode.string

let decodeResonse = Decode.array decodeRepoItem

let githubUsers =
        [ "fable-compiler"; "fsprojects"; "nojaf" ]


let loadRepos updateRepos user =
    let url = sprintf "https://api.github.com/users/%s/repos" user
    Fetch.fetch url []
    |> Promise.bind (fun res -> res.text())
    |> Promise.map (fun json -> Decode.fromString decodeResonse json)
    |> Promise.mapResult updateRepos
    |> ignore

let effectComponent() =
    let options =
        githubUsers
        |> List.map (fun name ->
            option [ Value name; Key name ] [ str name ]
        )
        |> (@) (List.singleton (option [ Value ""; Key "empty" ] []))

    let (selectedOrg, setOrganisation) = useState ("")
    let (repos, setRepos) = useState (Array.empty)
    let onChange (ev : Event) = setOrganisation (ev.Value)

    useEffect (fun () ->
        match System.String.IsNullOrWhiteSpace(selectedOrg) with
        | true -> setRepos Array.empty
        | false -> loadRepos setRepos selectedOrg
        |> U2.Case1
    ) [| selectedOrg |]

    let repoListItems =
        repos
        |> Array.sortWith (fun a b -> String.Compare(a, b, System.StringComparison.OrdinalIgnoreCase))
        |> Array.map (fun r -> li [ Key r ] [ str r ])

    div [ ClassName "content" ] [
        div [ ClassName "select" ] [
            select [ Value selectedOrg; OnChange onChange ] options
        ]
        ul [] repoListItems
    ]
