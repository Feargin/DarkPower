using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Update", Path = "uViLEd Components/Base/Mono", Tooltip = "The processing component of the Update method that is called each frame to update it", Color = VLEColor.Green)]        
        public class UpdateComponent : LogicComponent
        {                     
            [Tooltip("output point that is called each time Update is called")]
            public OUTPUT_POINT DoUpdate = new OUTPUT_POINT();

            [ExecuteOrder(1)]
            void Update()
            {
                DoUpdate.Execute();
            }
        }
    }
}
