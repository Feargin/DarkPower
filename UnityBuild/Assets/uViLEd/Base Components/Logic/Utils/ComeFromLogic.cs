using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {       
        [ComponentDefinition(Name = "ComeFromLogic", 
                           Path = "uViLEd Components/Base/Logic/Utils", 
                           Tooltip = "An component for processing he data came from another logic.\n\n"+
                                     "Note: each logic can receive data from several other logic and process them in different ways", Color = VLEColor.DarkCyan)]
        public class ComeFromLogic : LogicComponent, IDisposable
        {
            [Tooltip("output point that transmits data came from another logic")]
            public OUTPUT_POINT<object> Data = new OUTPUT_POINT<object>();
            [Tooltip("output point for processing transition from another logic without data")]
            public OUTPUT_POINT Empty = new OUTPUT_POINT();

            [ViewInEditor]
            public LogicEventDefinition LogicId;

            public override void Constructor()
            {
                GlobalEvent.Instance.Subscribe(this);
            }

            public void Dispose()
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    GlobalEvent.Instance.Unsubscribe(this);
                }
#else
                GlobalEvent.Instance.Unsubscribe(this);
#endif      
            }

            [GlobalEvent.HandlerEvent]
            private void OnGoToLogicEvent(GoToLogicEvent ev)
            {                
                if (string.Compare(ev.ToLogicId, LogicId.FromId, StringComparison.Ordinal) == 0 &&
                    string.Compare(ev.FromLogicId, ev.FromLogicId, StringComparison.Ordinal) == 0)
                {             
                    if(ev.Data == null)
                    {
                        Empty.Execute();
                    }else
                    {
                        Data.Execute(ev.Data);
                    }
                }
            }
        }
    }
}