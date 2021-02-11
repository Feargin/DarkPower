using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Bool", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for a boolean value", Color = VLEColor.Cyan)]
        public class VariableBool : Variable<bool> { }
    }
}
