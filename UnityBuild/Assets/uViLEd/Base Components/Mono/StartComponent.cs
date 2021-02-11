using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Start", 
                           Path = "uViLEd Components/Base/Mono", 
                           Tooltip = "An component for processing the Start method, called before the first frame is rendered. This function called once for each logic component", 
                           Color = VLEColor.Green)]        
        public class StartComponent : LogicComponent
        {                        
            [Tooltip("output point that is called when the Start method is called")]
            public OUTPUT_POINT DoStart = new OUTPUT_POINT();            

            [ExecuteOrder(1)]
            void Start()
            {
                DoStart.Execute();        
            }           
        }
    }
}
