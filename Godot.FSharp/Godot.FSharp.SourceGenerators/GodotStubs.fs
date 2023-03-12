namespace MyriadgodotMyriad

module GodotStubs =
    type PropertyHint =
        | None = 0l
        | Range = 1l
        | Enum = 2l
        | EnumSuggestion = 3l
        | ExpEasing = 4l
        | Link = 5l
        | Flags = 6l
        | Layers2DRender = 7l
        | Layers2DPhysics = 8l
        | Layers2DNavigation = 9l
        | Layers3DRender = 10l
        | Layers3DPhysics = 11l
        | Layers3DNavigation = 12l
        | File = 13l
        | Dir = 14l
        | GlobalFile = 15l
        | GlobalDir = 16l
        | ResourceType = 17l
        | MultilineText = 18l
        | Expression = 19l
        | PlaceholderText = 20l
        | ColorNoAlpha = 21l
        | ObjectId = 22l
        | TypeString = 23l
        | NodePathToEditedNode = 24l
        | ObjectTooBig = 25l
        | NodePathValidTypes = 26l
        | SaveFile = 27l
        | GlobalSaveFile = 28l
        | IntIsObjectid = 29l
        | IntIsPointer = 30l
        | ArrayType = 31l
        | LocaleId = 32l
        | LocalizableString = 33l
        | NodeType = 34l
        | HideQuaternionEdit = 35l
        | Password = 36l
        | Max = 37l

    type Type =
        | Nil = 0l
        | Bool = 1l
        | Int = 2l
        | Float = 3l
        | String = 4l
        | Vector2 = 5l
        | Vector2I = 6l
        | Rect2 = 7l
        | Rect2I = 8l
        | Vector3 = 9l
        | Vector3I = 10l
        | Transform2D = 11l
        | Vector4 = 12l
        | Vector4I = 13l
        | Plane = 14l
        | Quaternion = 15l
        | Aabb = 16l
        | Basis = 17l
        | Transform3D = 18l
        | Projection = 19l
        | Color = 20l
        | StringName = 21l
        | NodePath = 22l
        | Rid = 23l
        | Object = 24l
        | Callable = 25l
        | Signal = 26l
        | Dictionary = 27l
        | Array = 28l
        | PackedByteArray = 29l
        | PackedInt32Array = 30l
        | PackedInt64Array = 31l
        | PackedFloat32Array = 32l
        | PackedFloat64Array = 33l
        | PackedStringArray = 34l
        | PackedVector2Array = 35l
        | PackedVector3Array = 36l
        | PackedColorArray = 37l
        | Max = 38l

    type PropertyUsageFlags =
        | None = 0l
        | Storage = 2l
        | Editor = 4l
        | Internal = 8l
        | Checkable = 16l
        | Checked = 32l
        | Group = 64l
        | Category = 128l
        | Subgroup = 256l
        | ClassIsBitfield = 512l
        | NoInstanceState = 1024l
        | RestartIfChanged = 2048l
        | ScriptVariable = 4096l
        | StoreIfNull = 8192l
        | UpdateAllIfModified = 16384l
        | ScriptDefaultValue = 32768l
        | ClassIsEnum = 65536l
        | NilIsVariant = 131072l
        | Array = 262144l
        | AlwaysDuplicate = 524288l
        | NeverDuplicate = 1048576l
        | HighEndGfx = 2097152l
        | NodePathFromSceneRoot = 4194304l
        | ResourceNotPersistent = 8388608l
        | KeyingIncrements = 16777216l
        | DeferredSetResource = 33554432l
        | EditorInstantiateObject = 67108864l
        | EditorBasicSetting = 134217728l
        | ReadOnly = 268435456l
        | Default = 6l
        | NoEditor = 2l

    type MethodFlags =
        | Normal = 1l
        | Editor = 2l
        | Const = 4l
        | Virtual = 8l
        | Vararg = 16l
        | Static = 32l
        | ObjectCore = 64l
        | Default = 1l
