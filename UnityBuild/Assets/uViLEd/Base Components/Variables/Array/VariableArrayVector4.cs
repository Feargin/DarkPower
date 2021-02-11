using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Vector4 []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of 4D vectors", Color = VLEColor.Cyan)]
        public class VariableArrayVector4 : Variable<Vector4[]> { }
    }
}