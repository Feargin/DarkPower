using System.Collections.Generic;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "List", 
                           Path = "uViLEd Components/Base/Variable/Dynamic", 
                           Tooltip = "Variable for a list of packed (boxed) values", 
                           Color = VLEColor.Cyan)]
        public class VariableList : Variable<List<object>> { }
    }
}