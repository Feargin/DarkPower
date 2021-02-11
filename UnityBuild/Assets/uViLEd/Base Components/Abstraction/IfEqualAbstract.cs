using System.Collections.Generic;
using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class IfEqualAbstract<T> : LogicComponent, IInputPointParse
        {
            [Tooltip("Input point for transmitting value, which must be compared with the internal value of the component, set through the parameters")]
            public INPUT_POINT<T> CompareWithInternal = new INPUT_POINT<T>();

            [Tooltip("input point for transmitting the first value for the subsequent equality check")]
            public INPUT_POINT<T> FirstCompareValue = new INPUT_POINT<T>();
            [Tooltip("input point for transmitting the second value for subsequent equality check")]
            public INPUT_POINT<T> SecondCompareValue = new INPUT_POINT<T>();

            [Tooltip("output point without data transmission, called if input values are equal")]
            public OUTPUT_POINT True = new OUTPUT_POINT();
            [Tooltip("output point without data transmission, вызывается, called if input values are not equal")]
            public OUTPUT_POINT False = new OUTPUT_POINT();

            [Tooltip("configuration flag for input points")]
            public HideBranchingFlag HideFlag;

            [Tooltip("an internal value for comparison, used only if the value is passed through the input point CompareWithInternal")]
            public T ValueForCompare;

            private T _firstCompareValue;
            private T _secondCompareValue;

            private bool _firstCompareSet = false;
            private bool _secondCompareSet = false;

            public override void Constructor()
            {
                CompareWithInternal.Handler = CompareWithInternalHandler;
                FirstCompareValue.Handler = FirstCompareValueHandler;
                SecondCompareValue.Handler = SecondCompareValueHandler;
            }            

            protected virtual bool CompareEqual(T first, T second)
            {
                return first.Equals(second);
            }

            private void CompareWithInternalHandler(T value)
            {
                if (CompareEqual(value, ValueForCompare))
                {
                    True.Execute();
                }
                else
                {
                    False.Execute();
                }
            }

            private void FirstCompareValueHandler(T firstValue)
            {
                _firstCompareValue = firstValue;
                _firstCompareSet = true;

                if (_secondCompareSet)
                {
                    CompareExternal();
                }
            }

            private void SecondCompareValueHandler(T secondValue)
            {
                _secondCompareValue = secondValue;
                _secondCompareSet = true;

                if (_firstCompareSet)
                {
                    CompareExternal();
                }
            }

            private void CompareExternal()
            {
                _firstCompareSet = false;
                _secondCompareSet = false;

                if (CompareEqual(_firstCompareValue, _secondCompareValue))
                {
                    True.Execute();
                }
                else
                {
                    False.Execute();
                }
            }

            public IDictionary<string, object> GetInputPoints()
            {
                var inputPoints = new Dictionary<string, object>();

                switch (HideFlag)
                {                    
                    case HideBranchingFlag.None:
                        inputPoints.Add("CompareWithInternal", this.GetType().GetField("CompareWithInternal"));
                        inputPoints.Add("FirstValue", this.GetType().GetField("FirstCompareValue"));
                        inputPoints.Add("SecondValue", this.GetType().GetField("SecondCompareValue"));
                        break;
                    case HideBranchingFlag.HideExternal:
                        inputPoints.Add("CompareWithInternal", this.GetType().GetField("CompareWithInternal"));
                        break;
                    case HideBranchingFlag.HideInternal:
                        inputPoints.Add("FirstValue", this.GetType().GetField("FirstCompareValue"));
                        inputPoints.Add("SecondValue", this.GetType().GetField("SecondCompareValue"));
                        break;
                }

                return inputPoints;
            }
        }
    }
}
