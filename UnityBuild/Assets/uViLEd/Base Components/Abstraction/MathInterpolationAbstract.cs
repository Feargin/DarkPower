using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class MathInterpolationAbstract<T> : LogicComponent
        {
            [Tooltip("input point for setting the initial value")]
            public INPUT_POINT<T> SetFrom = new INPUT_POINT<T>();
            [Tooltip("input point for setting the end value")]
            public INPUT_POINT<T> SetTo = new INPUT_POINT<T>();
            [Tooltip("Input point for transmitting of the normalized time value")]
            public INPUT_POINT<float> Tt = new INPUT_POINT<float>();

            [Tooltip("output point for transmitting the result of the operation")]
            public OUTPUT_POINT<T> Result = new OUTPUT_POINT<T>();

            [ViewInEditor]
            [Tooltip("default start value. If the value was set through the input point, then it will be used")]
            public T DefaultFrom;
            [ViewInEditor]
            [Tooltip("default end value. If the value was set through the input point, then it will be used")]
            public T DefaultTo;

            public VARIABLE_LINK<AnimationCurve> Curve = new VARIABLE_LINK<AnimationCurve>();

            private T _from;
            private T _to;

            public override void Constructor()
            {
                _from = DefaultFrom;
                _to = DefaultTo;

                SetFrom.Handler = SetFromHandler;
                SetTo.Handler = SetToHandler;
                Tt.Handler = InputTimeHandler;
            }

            protected abstract T Interpolate(T from, T to, float t);

            private void SetFromHandler(T value)
            {
                _from = value;
            }

            private void SetToHandler(T value)
            {
                _to = value;
            }

            private void InputTimeHandler(float value)
            {
                if (Curve.VariableWasSet)
                {
                    Result.Execute(Interpolate(_from, _to, Curve.Value.Evaluate(value)));
                } else
                {
                    Result.Execute(Interpolate(_from, _to, value));
                }
            }            
        }
    }
}