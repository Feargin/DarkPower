using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Color", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for a color", Color = VLEColor.Cyan)]
        public class VariableColor : Variable<Color> { }
    }
}