namespace Godot.FSharp.SourceGenerators

open System
open System.IO
open FSharp.Compiler.CodeAnalysis
open Fantomas.Core
open Myriad.Core
open Godot.FSharp.SourceGenerators.GodotStubs

open Generator
open GodotStubs
open FSharp.Compiler.Text
open FSharp.Compiler.Syntax

[<MyriadGenerator("fsharp.godot.example")>]
type Example() =
    interface IMyriadGenerator with
        member _.ValidInputExtensions = seq { ".fs" }

        member _.Generate(context: GeneratorContext) =

            let checker = FSharpChecker.Create(keepAssemblyContents = true)

            let file = context.InputFilename |> File.ReadAllText |> SourceText.ofString

            let (options, _) =
                checker.GetProjectOptionsFromScript(context.InputFilename, file)
                |> Async.RunSynchronously

            let (checkProjectResults, answer) =
                checker.ParseAndCheckFileInProject(context.InputFilename, 1, file, options)
                |> Async.RunSynchronously

            let answer =
                match answer with
                | FSharpCheckFileAnswer.Aborted -> "Could not parse file" |> System.Exception |> raise
                | FSharpCheckFileAnswer.Succeeded x -> x

            let file = answer.ProjectContext.GetReferencedAssemblies().Head

            let scriptModules =
                file.Contents.Entities
                |> Seq.filter (fun x -> x.IsFSharpModule)
                |> Seq.filter (fun x ->
                    x.Attributes
                    |> Seq.exists (fun x -> x.IsAttribute<Helper.NodeScriptAttribute>()))
                |> Seq.map (fun modu ->
                    let children = modu.GetPublicNestedEntities() |> Seq.toList

                    let state =
                        children
                        |> List.filter (fun child ->
                            child.IsFSharpRecord && child.HasAttribute<Helper.StateAttribute>())

                    if state.Length > 1 then
                        "More than 1 state found" |> System.Exception |> raise

                    ()
                    let state = state.Head

                    let node =
                        children
                        |> List.filter (fun child ->
                            child.HasAttribute<Helper.NodeAttribute>()
                            && child.IsClass
                            && Helper.isExtendingNode child)

                    if node.Length > 1 then
                        "More than 1 state found" |> System.Exception |> raise

                    let node = node[0]

                    let methods =
                        modu.MembersFunctionsAndValues
                        |> Seq.filter (fun x ->
                            let para = x.CurriedParameterGroups |> Helper.flatten |> Seq.toArray

                            if para.Length < 2 then
                                false
                            else
                                let last = para[para.Length - 1]
                                last.IsEffectivelySameAs state && x.ReturnParameter.IsEffectivelySameAs state)

                    state, node, methods)

            try
                let toGenerate: List<ToGenerateInfo> =
                    [
                        {

                            Extending = "Node"
                            Name = "MyNode"
                            methods =
                                [
                                    {
                                        MethodName = "_Ready"
                                        IsOverride = true
                                        MethodParams = []
                                        MethodFlags = MethodFlags.Default
                                    }
                                    {
                                        MethodName = "_Process"
                                        IsOverride = true
                                        MethodParams =
                                            [
                                                {

                                                    Name = "delta"
                                                    OfTypeName = "double"
                                                    OfType = Type.Float
                                                    PropertyHint = PropertyHint.None
                                                    UsageFlags = PropertyUsageFlags.Default
                                                    HintText = ""
                                                }
                                            ]
                                        MethodFlags = MethodFlags.Default
                                    }
                                ]

                            StateToGenerate =
                                {
                                    Name = "BasicState"
                                    ExportedFields =
                                        [
                                            {
                                                Name = "Hello"
                                                OfTypeName = "int"
                                                OfType = Type.Int
                                                PropertyHint = PropertyHint.None
                                                HintText = ""
                                                UsageFlags =
                                                    PropertyUsageFlags.Default ||| PropertyUsageFlags.ScriptVariable
                                            }
                                        ]
                                    InnerFields =
                                        [
                                            {
                                                Name = "IAmInner"
                                                OfTypeName = "string"
                                                OfType = Type.String
                                                PropertyHint = PropertyHint.None
                                                HintText = ""
                                                UsageFlags = PropertyUsageFlags.ScriptVariable
                                            }
                                        ]
                                }
                            InNamespace = "GeneratedNodes"
                            ModuleNameToOpen = "TestFSharpGodot.Say"
                        }
                    ]

                let generatedStr = toGenerate |> Seq.map generateClass |> String.concat "\n\n"
                let logger = File.CreateText "./output.debug.myriad.txt"
                logger.Write generatedStr
                logger.Flush()
                logger.Close()
                Output.Source generatedStr
            with x ->
                let logger = File.CreateText "./output.debug.myriad.txt"
                logger.Write x
                logger.Flush()
                logger.Close()
                raise x
