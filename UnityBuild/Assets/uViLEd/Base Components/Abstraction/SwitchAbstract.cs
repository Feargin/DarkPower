using System.Collections.Generic;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        
        public abstract class SwitchAbstract<T> : LogicComponent, IOutputPointParse
        {
            [Tooltip("input point for transmitting value, which should be checked")]
            public INPUT_POINT<T> ValueToBeChecked = new INPUT_POINT<T>();

            [Tooltip("set of values for branching")]
            public List<T> SwitchValues = new List<T>();

            protected Dictionary<string, object> outputPoints = new Dictionary<string, object>();            

            public override void Constructor()
            {
                ValueToBeChecked.Handler = ValueToBeCheckedHandler;
            }

            protected virtual bool CompareEqual(T first, T second)
            {
                return first.Equals(second);
            }

            protected virtual string GetValueString(T value)
            {
                var outputPontName = value.ToString();

#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    if (outputPoints.ContainsKey(outputPontName))
                    {
                        outputPontName += " ({0})".Fmt(outputPoints.Count);
                    }
                }
#endif

                return outputPontName;
            }

            private void ValueToBeCheckedHandler(T checkedValue)
            {                
                foreach (var value in SwitchValues)
                {
                    if( CompareEqual(checkedValue, value))
                    {                        
                        ((OUTPUT_POINT)outputPoints[GetValueString(value)]).Execute();

                        return;
                    }
                }
            }

            public IDictionary<string, object> GetOutputPoints()
            {

#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    outputPoints.Clear();
                }
#endif                
                if (outputPoints.Count == 0)
                {
                    foreach (var value in SwitchValues)
                    {
                        outputPoints.Add(GetValueString(value), new OUTPUT_POINT());
                    }
                }

                return outputPoints;
            }
        }
    }
}