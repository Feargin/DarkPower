using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "StopLogic", 
                           Path = "uViLEd Components/Base/Logic/Utils", 
                           Tooltip = "An component for destroying a logic instance by identifier", 
                           Color = VLEColor.DarkCyan)]
        public class StopLogic : LogicComponent
        {
            [Tooltip("input point for transferring logic instance identifier")]
            public INPUT_POINT<string> InstanceId = new INPUT_POINT<string>();
            [Tooltip("output point, called when the logic is destroyed")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();

            public override void Constructor()
            {
                InstanceId.Handler = InstanceIdHandler;
            }

            private void InstanceIdHandler(string value)
            {
                LogicController.Instance.StopLogicInstance(value);
            }
        }
    }
}