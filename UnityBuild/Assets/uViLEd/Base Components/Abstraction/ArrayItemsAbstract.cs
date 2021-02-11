using System;

using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class ArrayItemsAbstract<T> : LogicComponent
        {
            [Tooltip("input point to get the value by index")]
            public INPUT_POINT<int> GetItem = new INPUT_POINT<int>();
            [Tooltip("input point for checking the value existence in the array")]
            public INPUT_POINT<T> ContainsItem = new INPUT_POINT<T>();

            [Tooltip("output point that transmits the value of the array member according to the index obtained through the GetItem")]
            public OUTPUT_POINT<T> ItemValue = new OUTPUT_POINT<T>();
            [Tooltip("output point that transmits the result (result is index of item, if -1, then item not found) of checking the value existence in the array")]
            public OUTPUT_POINT<int> ContainsResult = new OUTPUT_POINT<int>();

            public VARIABLE_LINK<T[]> Variable = new VARIABLE_LINK<T[]>();

            public override void Constructor()
            {
                GetItem.Handler = GetItemHandler;
                ContainsItem.Handler = ContainsItemHandler;
            }

            protected virtual int ChekingValueContains(T value)
            {
                return Array.IndexOf(Variable.Value, value);
            }

            private void GetItemHandler(int index)
            {                
                if(index < Variable.Value.Length && index >= 0)
                {
                    ItemValue.Execute(Variable.Value[index]);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogErrorFormat("[uViLEd]: index [{0}] for get item in array out of range. [LogicComponent = {1}]", index, name);                    
                }
#endif
            }

            private void ContainsItemHandler(T value)
            {
                ContainsResult.Execute(ChekingValueContains(value));
            }
        }
    }
}
