using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class MathSingleOperationAbstract<T> : LogicComponent 
        {
            [Tooltip("Input point for transmitting value")]
            public INPUT_POINT<T> Operand = new INPUT_POINT<T>();
            [Tooltip("output point transmitting the result of the operatione")]
            public OUTPUT_POINT<T> Result = new OUTPUT_POINT<T>();            

            public override void Constructor()
            {
                Operand.Handler = OperandHandler;
            }

            protected abstract T Operation(T value);

            private void OperandHandler(T value)
            {
                Result.Execute(Operation(value));
            }
        }
    }
}