using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Vector4", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for 4D vector", Color = VLEColor.Cyan)]
        public class VariableVector4 : Variable<Vector4> { }
    }
}