using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Vector2", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for 2D vector", Color = VLEColor.Cyan)]
        public class VariableVector2 : Variable<Vector2> { }
    }
}