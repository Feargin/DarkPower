using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "LateUpdate", 
                           Path = "uViLEd Components/Base/Mono", 
                           Tooltip = "Component for processing the method LateUpdate, called after the update of the frame", Color = VLEColor.Green)]        
        public class LateUpdateComponent : LogicComponent
        {            
            [Tooltip("output point that is called on each LateUpdate")]
            public OUTPUT_POINT DoLateUpdate = new OUTPUT_POINT();

            [ExecuteOrder(1)]
            void LateUpdate()
            {
                DoLateUpdate.Execute();
            }
        }
    }
}
