using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "FixedUpdate", 
                           Path = "uViLEd Components/Base/Mono", 
                           Tooltip = "The processing component of the FixedUpdate method, called by fixed time intervals, defined by the fixedDeltaTime parameters in the Unity editor's physics settings", 
                           Color = VLEColor.Green)]        
        public class FixedUpdateComponent : LogicComponent
        {            
            [Tooltip("output point to call on each FixedUpdate")]
            public OUTPUT_POINT DoFixedUpdate = new OUTPUT_POINT();

            [ExecuteOrder(1)]
            void FixedUpdate()
            {
                DoFixedUpdate.Execute();
            }
        }
    }
}
