using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;      
#endif

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class InputAbstract : LogicComponent 
        {
            [Tooltip("input point for setting the state of the component (to process or not the input commands)")]
            public INPUT_POINT<bool> SetState = new INPUT_POINT<bool>();

            protected bool currentState = true;

            public override void Constructor()
            {
                SetState.Handler = (value) => currentState = value;
            }
        }

        public abstract class InputEventsAbstract : InputAbstract, IOutputPointParse
        {
            [Tooltip("output point for the press event")]
            public OUTPUT_POINT Down = new OUTPUT_POINT();
            [Tooltip("output point for the release event")]        
            public OUTPUT_POINT Up = new OUTPUT_POINT();
            [Tooltip("output point for the hold event")]
            public OUTPUT_POINT Hold = new OUTPUT_POINT();
            
            public InputEventsDefinition Events;

            public IDictionary<string, object> GetOutputPoints()
            {
                var outputPoints = new Dictionary<string, object>();

                var events = Events.GetEvents();                

                foreach (var ev in events)
                {
                    switch (ev)
                    {
                        case InputEventType.Down:
                            outputPoints.Add("Down", this.GetType().GetField("Down"));
                            break;
                        case InputEventType.Up:
                            outputPoints.Add("Up", this.GetType().GetField("Up"));
                            break;
                        case InputEventType.Hold:
                            outputPoints.Add("Hold", this.GetType().GetField("Hold"));
                            break;
                    }
                }

                return outputPoints;
            }
        }
    }
}