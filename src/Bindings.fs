module ReactHooksSample.Bindings

open Fable.Core.JsInterop
open Fable.Core

type SetState<'t> = 't -> unit
let useState<'t> (t: 't) : ('t * SetState<'t>) = import "useState" "react"

type ReduceFn<'state,'msg> = ('state -> 'msg -> 'state)
type Dispatch<'msg> ='msg -> unit
let useReducer<'state,'msg> (reducer: ReduceFn<'state,'msg>) (initialState:'state) : ('state * Dispatch<'msg>)  = import "useReducer" "react"

let useEffect (effect: (unit -> U2<unit, (unit -> unit)>)) (dependsOn: obj array) : unit = import "useEffect" "react"