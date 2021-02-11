using UnityEngine;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableVector2", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a Vector2 variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableVector2 : SetGetVariableAbstract<Vector2> { }
    }
}