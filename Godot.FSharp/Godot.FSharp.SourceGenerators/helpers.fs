namespace Godot.FSharp.SourceGenerators

open System
open FSharp.Compiler.Syntax
open Fantomas.Core
open Myriad.Core.Ast


module Helper =
    open System.Collections.Generic

    type NodeScriptAttribute() =
        inherit System.Attribute()

    type NodeAttribute() =
        inherit System.Attribute()

    type StateAttribute() =
        inherit System.Attribute()

    type ExportAttribute() =
        inherit System.Attribute()

    type NodeOrState =
        | Node of SynComponentInfo
        | State of SynComponentInfo

    type INodeWithState<'Node, 'State> =
        abstract member GetState: unit -> 'State
        abstract member SetState: 'State -> unit
        abstract member GetNode: unit -> 'Node

    let rec isExtendingNode (entity: FSharp.Compiler.Symbols.FSharpEntity) =
        match entity.FullName = "Godot.Node", entity.BaseType with
        | true, _ -> true
        | _, None -> false
        | _, Some x -> x.TypeDefinition |> isExtendingNode

    let flatten (a: IList<IList<'a>>) =
        seq {
            for v in a do
                yield! v
        }
