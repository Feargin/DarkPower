using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Int []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of integer numbers", Color = VLEColor.Cyan)]
        public class VariableArrayInt : Variable<int[]> { }        
    }
}