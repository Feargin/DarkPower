using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class MathMinMaxAbstract<T> : MathSingleOperationAbstract<T> 
        {
            [Tooltip("input point for setting the minimum value")]
            public INPUT_POINT<T> SetMin = new INPUT_POINT<T>();
            [Tooltip("input point for setting the maximum value")]
            public INPUT_POINT<T> SetMax = new INPUT_POINT<T>();

            [ViewInEditor]
            [Tooltip("the default minimum value. If the value was set through the input point, then it will always be used")]
            public T DefaultMin;
            [ViewInEditor]
            [Tooltip("the default maximum value. If the value was set through the input point, then it will always be used")]
            public T DefaultMax;

            protected T min;
            protected T max;

            public override void Constructor()
            {
                base.Constructor();

                SetMin.Handler = SetMinHandler;
                SetMax.Handler = SetMaxHandler;

                min = DefaultMin;
                max = DefaultMax;
            }            

            private void SetMinHandler(T value)
            {
                min = value;
            }

            private void SetMaxHandler(T value)
            {
                max = value;
            }          
        }
    }
}