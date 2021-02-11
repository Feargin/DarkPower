using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Color []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of colors", Color = VLEColor.Cyan)]
        public class VariableArrayColor : Variable<Color[]> { }
    }
}