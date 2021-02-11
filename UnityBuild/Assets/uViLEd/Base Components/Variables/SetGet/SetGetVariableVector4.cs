using UnityEngine;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableVector4", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a Vector4 variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableVector4 : SetGetVariableAbstract<Vector4> { }
    }
}