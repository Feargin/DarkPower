using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "AnimationCurve", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for the animation curve", Color = VLEColor.Cyan)]
        public class VariableAnimationCurve : Variable<AnimationCurve> { }
    }
}