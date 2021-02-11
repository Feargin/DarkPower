using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "VLObject []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of references to objects, components, prefabs", Color = VLEColor.Cyan)]
        public class VariableArrayVLObject : Variable<VLObject[]> { }
    }
}