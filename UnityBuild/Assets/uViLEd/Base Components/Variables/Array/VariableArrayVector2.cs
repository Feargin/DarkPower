using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Vector2 []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of 2D vectors", Color = VLEColor.Cyan)]
        public class VariableArrayVector2 : Variable<Vector2[]> { }
    }
}