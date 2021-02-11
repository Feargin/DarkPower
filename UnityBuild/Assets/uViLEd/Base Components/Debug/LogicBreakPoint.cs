using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "BreakPoint", 
                           Path = "uViLEd Components/Base/Debug", 
                           Tooltip = "An component that stops the logic. Connect it to any output in the logic chain to pause the execution. If you want it to be paused prior to the execution of the next component logic, use number 1 as the breakpoint link order", 
                           Color = VLEColor.Red)]
        public class LogicBreakPoint : LogicComponent 
        {
            [Tooltip("input point which is stopping execution of the diagram")]
            public INPUT_POINT Break = new INPUT_POINT();            

            public override void Constructor()
            {
                Break.Handler = BreakHandler;
            }

            private void BreakHandler()
            {
#if UNITY_EDITOR                
                VLEditor.VLEDebug.Break();
#endif                
            }
        }
    }
}