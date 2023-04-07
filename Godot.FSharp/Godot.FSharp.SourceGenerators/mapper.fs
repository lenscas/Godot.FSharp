namespace Godot.FSharp.SourceGenerators

module Mapper =
    open FSharp.Compiler.Syntax
    open Generator
    open FSharp.Compiler.Symbols


    // let getTypeNameFromIdent =
    //     List.map (fun (x: Ident) -> x.idText) >> String.concat (".")

    let seperateListBasedOn func l =
        let mutable l1 = []
        let mutable l2 = []

        for v in l do
            if func v then l1 <- v :: l1 else l2 <- v :: l2

        l1, l2

    // let getExtendingName (a: SynType) =
    //     match a with
    //     | SynType.LongIdent(SynLongIdent(ident: LongIdent, _, _)) ->
    //         ident |> List.map (fun x -> x.idText) |> String.concat (".")
    //     | x -> "Type is not a proper node abriviation" |> System.Exception |> raise

    // let generateField (a: SynField) =
    //     let SynField(attributes, _, idOpt, fieldType, _, _, _, _) as _ = a

    //     let identifier =
    //         match idOpt with
    //         | Some x -> x
    //         | None -> "Missing identifier for field in state" |> System.Exception |> raise

    //     let fieldName = identifier.idText
    //     let fieldTypeName = getExtendingName fieldType


    //     ()

    // let generateState ((state, b): (SynComponentInfo * List<SynField>)) =
    //     let SynComponentInfo(attributes, typeParams, constraints, longId, xmlDoc, preferPostfix, accessibility, range) as _ =
    //         state

    //     let name = getTypeNameFromIdent longId

    //     let exported, notExported =
    //         b
    //         |> seperateListBasedOn (fun x ->
    //             let SynField(attributes, _, _, _, _, _, _, _) as _ = x
    //             Helper.hasAttribute<Helper.ExportAttribute> attributes)

    //     ()

    let private generateFields (fields: list<FSharpMemberOrFunctionOrValue>) : List<Field> =
        fields
        |> List.map (fun x ->
            {
                Name = x.FullName
                OfTypeName = x.FullType.TypeDefinition.FullName
                PropertyHint = GodotStubs.PropertyHint.None
                HintText = ""
                UsageFlags = GodotStubs.PropertyUsageFlags.Default
            //OfType = x.FullType
            })

    let private generateState (state: FSharpEntity) =
        let exported, priv =
            state.MembersFunctionsAndValues
            |> seperateListBasedOn (fun x -> x.HasAttribute<Helper.ExportAttribute>())

        ()

    let private getExtendingName (a: FSharpEntity) =
        a.BaseType.Value.TypeDefinition.FullName

    let private map
        (
            state: FSharpEntity,
            node: FSharpEntity,
            methods: seq<FSharpMemberOrFunctionOrValue>
        ) : ToGenerateInfo =
        {
            InNamespace = "notKnownYet"
            ModuleNameToOpen = "notKnownYet"
            Extending = getExtendingName node
            Name = node.FullName
            StateToGenerate = generateState state
        }
