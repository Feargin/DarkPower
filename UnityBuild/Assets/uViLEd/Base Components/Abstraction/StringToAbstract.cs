using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {        
        public abstract class StringToAbstract<T> : LogicComponent
        {
            [Tooltip("input point for transferring a string for conversion")]
            public INPUT_POINT<string> String = new INPUT_POINT<string>();
            [Tooltip("output point transmitting the result of the conversion")]
            public OUTPUT_POINT<T> ParsingValue = new OUTPUT_POINT<T>();

            public override void Constructor()
            {
                String.Handler = StringHandler;
            }

            protected abstract bool TryParse(string value, out T parsingValue);

            private void StringHandler(string value)
            {
                T parsingValue;                

                if (TryParse(value, out parsingValue))
                {
                    ParsingValue.Execute(parsingValue);
                }
                else
                {
                    Debug.LogErrorFormat("[uViLEd]: LogicComponent [{0}] error parsing string [{1}] to [{2}]", name, value, typeof(T).GetType().Name);
                }
            }
        }
    }
}
