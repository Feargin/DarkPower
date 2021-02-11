using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "VLObject", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for the reference to object, component, prefab", Color = VLEColor.Cyan)]
        public class VariableVLObject : Variable<VLObject> { }
    }
}
