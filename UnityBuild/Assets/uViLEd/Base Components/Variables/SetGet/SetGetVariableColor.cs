using UnityEngine;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableColor", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a Color variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableColor : SetGetVariableAbstract<Color> { }
    }
}