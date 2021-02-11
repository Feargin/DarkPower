using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "InstanceLogic", Path = "uViLEd Components/Base/Logic/Utils", Tooltip = "An component for initializing logic instance", Color = VLEColor.DarkCyan)]
        public class InstanceLogic : LogicComponent
        {
            [Tooltip("output point that transmits identifier of logic instance")]
            public OUTPUT_POINT<string> InstanceId = new OUTPUT_POINT<string>();
            [Tooltip("output point that transmits data for logic instance")]
            public OUTPUT_POINT<object> Data = new OUTPUT_POINT<object>();
        }
    }
}