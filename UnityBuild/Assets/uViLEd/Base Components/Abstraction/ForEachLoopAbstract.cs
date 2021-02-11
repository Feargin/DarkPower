using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class ForEachLoopAbstract<T> : LogicComponent 
        {
            [Tooltip("input point without data transmission, executes the loop")]
            public INPUT_POINT Do = new INPUT_POINT();

            [Tooltip("output point transmitting the current value in the loop")]
            public OUTPUT_POINT<T> ReturnValue = new OUTPUT_POINT<T>();
            [Tooltip("output point that is called after the loop is completed")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();

            [Tooltip("reference to an array variable with the data type of the corresponding component")]
            public VARIABLE_LINK<T[]> ArrayVariable = new VARIABLE_LINK<T[]>();

            public override void Constructor()
            {
                Do.Handler = DoInternalHandler;                
            }

            private void DoInternalHandler()
            {
                foreach(var item in ArrayVariable.Value)
                {
                    ReturnValue.Execute(item);
                }

                Complete.Execute();
            }            
        }
    }
}