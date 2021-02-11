using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class SendAbstract<T> : LogicComponent
        {
            [Tooltip("input point that causes data to be transmitted to the output point")]
            public INPUT_POINT Send = new INPUT_POINT();

            [Tooltip("output point that transmits the data set in the parameters")]
            public OUTPUT_POINT<T> Data = new OUTPUT_POINT<T>();

            [ViewInEditor]
            [Tooltip("value for transmission to the output point")]
            public T Value;

            public override void Constructor()
            {
                Send.Handler = InputHandler;
            }

            private void InputHandler()
            {
                Data.Execute(Value);
            }
        }
    }
}
