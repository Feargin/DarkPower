using System;
using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        [ComponentDefinition(Name = "ReceiveMessage", Path = "uViLEd Components/Base/Logic/Messages", Tooltip = "An component that processes messages with a given identifier", Color = VLEColor.DarkOrange)]
        public class ReceiveMessageComponent : LogicComponent, IDisposable
        {            
            [Tooltip("output point that is called when the message with data is processed")]
            public OUTPUT_POINT<object> DataMessage = new OUTPUT_POINT<object>();
            [Tooltip("output point that is called when the message without data is processed")]
            public OUTPUT_POINT EmptyMessage = new OUTPUT_POINT();

            [ViewInEditor]
            public LogicMessageDefinition Message;

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
            private void OnLogicMessage(LogicMessage ev)
            {
                if(ev.MessageId == Message.Id)
                {
                    if(ev.Data != null)
                    {
                        DataMessage.Execute(ev.Data);
                    }else
                    {
                        EmptyMessage.Execute();
                    }
                }
            }            
        }
    }
}