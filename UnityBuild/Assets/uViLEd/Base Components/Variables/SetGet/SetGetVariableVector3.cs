using UnityEngine;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableVector3", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a Vector3 variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableVector3 : SetGetVariableAbstract<Vector3> { }
    }
}