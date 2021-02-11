using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public class GoToLogicEvent : GlobalEvent.BaseEvent<GoToLogicEvent>
        {
            public string FromLogicId { get; private set; }
            public string ToLogicId { get; private set; }
            public object Data { get; private set; }

            public GoToLogicEvent(string toLogicId, string fromLogicId, object data)
            {
                FromLogicId = fromLogicId;
                ToLogicId = toLogicId;
                Data = data;
            }
        }        

        [ComponentDefinition(Name = "GoToLogic", Path = "uViLEd Components/Base/Logic/Utils", Tooltip = "An component of transition the logic flow to another logic with or without data transfer", Color = VLEColor.DarkCyan)]
        public class GoToLogic : LogicComponent 
        {
            [Tooltip("input point for the transition to the logic and the transfer of data to it")]
            public INPUT_POINT<object> Data = new INPUT_POINT<object>();
            [Tooltip("input point for the transition to the logic without data transmission")]
            public INPUT_POINT Empty = new INPUT_POINT();

            [ViewInEditor]
            public LogicEventDefinition LogicId;

            public override void Constructor()
            {
                Data.Handler = DataHandler;
                Empty.Handler = EmptyHandler;
            }

            private void DataHandler(object data)
            {
                GoToLogicEvent.Call(new GoToLogicEvent(LogicId.ToId, LogicId.FromId,  data));
            }

            private void EmptyHandler()
            {
                GoToLogicEvent.Call(new GoToLogicEvent(LogicId.ToId, LogicId.FromId, null));
            }
        }
    }
}