using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class RandomRangeAbstract<T> : LogicComponent
        {
            [Tooltip("input point for setting the minimum value")]
            public INPUT_POINT<T> SetMin = new INPUT_POINT<T>();
            [Tooltip("input point for setting the maximum value")]
            public INPUT_POINT<T> SetMax = new INPUT_POINT<T>();
            [Tooltip("input point for obtaining a random value lying between the minimum and maximum")]
            public INPUT_POINT Random = new INPUT_POINT();

            [Tooltip("output point for transmission of the received random value")]
            public OUTPUT_POINT<T> Result = new OUTPUT_POINT<T>();


            [ViewInEditor]
            [Tooltip("the default minimum value. If the value was set through the input point, then it will always be used")]
            public T DefaultMin;
            [ViewInEditor]
            [Tooltip("the default maximum value. If the value was set through the input point, then it will always be used")]
            public T DefaultMax;

            private T _min;
            private T _max;

            public override void Constructor()
            {
                _min = DefaultMin;
                _max = DefaultMax;

                SetMin.Handler = SetMinHandler;
                SetMax.Handler = SetMaxHandler;
                Random.Handler = RandomHandler;
            }

            protected abstract T RandomOperation(T min, T max);

            private void SetMinHandler(T value)
            {
                _min = value;
            }

            private void SetMaxHandler(T value)
            {
                _max = value;
            }

            private void RandomHandler()
            {
                Result.Execute(RandomOperation(_min, _max));               
            }            
        }
    }
}
