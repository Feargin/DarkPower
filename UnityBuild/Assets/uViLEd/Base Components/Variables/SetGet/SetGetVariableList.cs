using System.Collections.Generic;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableList", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a List<object> variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableList : SetGetVariableAbstract<List<object>> { }
    }
}
