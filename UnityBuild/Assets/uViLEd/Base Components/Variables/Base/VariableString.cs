using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "String", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for a string", Color = VLEColor.Cyan)]
        public class VariableString : Variable<string> { }
    }
}
