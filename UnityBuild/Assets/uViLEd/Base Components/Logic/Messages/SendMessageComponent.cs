using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public sealed class LogicMessage : GlobalEvent.BaseEvent<LogicMessage>
        {
            public int MessageId { get; private set; }
            public object Data { get; private set; }

            public LogicMessage(int messageId, object data)
            {
                MessageId = messageId;
                Data = data;
            }
        }       

        [ComponentDefinition(Name = "SendMessage", Path = "uViLEd Components/Base/Logic/Messages", Tooltip = "An component sending a message with or without data", Color = VLEColor.DarkOrange)]
        public class SendMessageComponent : LogicComponent 
        {            
            [Tooltip("input point for sending a message with data")]
            public INPUT_POINT<object> SendDataMessage = new INPUT_POINT<object>();
            [Tooltip("input point for sending a message without data")]
            public INPUT_POINT SendEmptyMessage = new INPUT_POINT();
            [Tooltip("output point, called when the message is sent")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();

            [ViewInEditor]            
            public LogicMessageDefinition Message;

            public override void Constructor()
            {
                SendDataMessage.Handler = SendDataMessageHandler;
                SendEmptyMessage.Handler = SendEmptyMessageHandler;
            }

            private void SendDataMessageHandler(object value)
            {                
                LogicMessage.Call(Message.Id, value);
                Complete.Execute();
            }

            private void SendEmptyMessageHandler()
            {
                LogicMessage.Call(Message.Id, null);
                Complete.Execute();
            }
        }
    }
}