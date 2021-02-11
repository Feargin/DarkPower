using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "String []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of strings", Color = VLEColor.Cyan)]
        public class VariableArrayString : Variable<string[]> { }
    }
}