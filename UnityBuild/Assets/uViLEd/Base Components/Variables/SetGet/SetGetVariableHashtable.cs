using System.Collections;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableHashtable", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a Hashtable variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableHashtable : SetGetVariableAbstract<Hashtable> { }
    }
}