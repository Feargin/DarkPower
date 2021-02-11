using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Vector3 []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of 3D vectors", Color = VLEColor.Cyan)]
        public class VariableArrayVector3 : Variable<Vector3[]> { }
    }
}