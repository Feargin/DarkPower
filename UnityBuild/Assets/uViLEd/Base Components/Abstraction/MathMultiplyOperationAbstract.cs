using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class MathMultiplyOperationAbstract<T> : LogicComponent, IInputPointParse
        {            
            [Tooltip("output point transmitting the result of the addition operation")]
            public OUTPUT_POINT<T> Result = new OUTPUT_POINT<T>();

            private Dictionary<string, object> _inputPoints = new Dictionary<string, object>();

            [Tooltip("number of operands")]
            public int NumberOperands;
            [Tooltip("basic internal values of the first operand. The default value is zero")]
            public T InternalOperand;

            private List<T> _values = new List<T>();

            public override void Constructor()
            {
                _inputPoints.Clear();

                for (var i = 0; i < NumberOperands; i++)
                {
                    var inputPoint = new INPUT_POINT<T>();

                    inputPoint.Handler = (value) =>
                    {
                        OperandHandler(value);
                    };

                    _inputPoints.Add("Operand {0}".Fmt(i + 1), inputPoint);
                }
            }

            private void OperandHandler(T value)
            {
                _values.Add(value);

                if(_values.Count == NumberOperands)
                {
                    var result = Operation(_values);

                    _values.Clear();

                    Result.Execute(result);                    
                }
            }

            protected abstract T Operation(List<T> values);

            public IDictionary<string, object> GetInputPoints()
            {
#if UNITY_EDITOR
                void fillInputPoints()
                {
                    for (var i = 0; i < NumberOperands; i++)
                    {
                        _inputPoints.Add("Operand {0}".Fmt(i + 1), new INPUT_POINT<T>());
                    }
                };

                if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    _inputPoints.Clear();

                    fillInputPoints();
                }
                else
                {
                    if (_inputPoints.Count == 0)
                    {
                        fillInputPoints();
                    }
                }
#endif                         
                return _inputPoints;
            }
        }
    }
}