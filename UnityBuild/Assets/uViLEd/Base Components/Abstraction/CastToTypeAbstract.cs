using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {               
        public abstract class CastToTypeAbstract<T> : LogicComponent
        {
            [Tooltip("input point for transmitting data for the conversion")]
            public INPUT_POINT<T> Value = new INPUT_POINT<T>();                    

            public override void Constructor()
            {
                Value.Handler = ValueHandler;
            }

            protected abstract bool CastValue(T value);

            private void ValueHandler(T value)
            {
                var castWithoutError = CastValue(value);               

                if (!castWithoutError)
                {
                    Debug.LogErrorFormat("[uViLEd]: LogicComponent [{0}] error cast [value = {1}, type = {2}]]", name, value, value.GetType().Name);
                }
            }                        
        }
    }
}
