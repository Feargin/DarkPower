using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class VLObjectAbstract<UnityObjectType> : LogicComponent where UnityObjectType : UnityEngine.Object
        {
            public VARIABLE_LINK<VLObject> UnityObject = new VARIABLE_LINK<VLObject>();

            protected UnityObjectType unityObject
            {
                get
                {
                    return UnityObject.Value.Get<UnityObjectType>();
                }
            }

            protected virtual void DoActionAfterValidation(Action action, Action errorHandler = null)
            {                
                if (unityObject != null)
                {
                    action();
                }
                else
                {
                    Debug.LogWarningFormat("[uViLEd]: unityObject [{0}] is null or was destroyed. [LogicComponent: {1}]", typeof(UnityObjectType).Name, name);

                    errorHandler?.Invoke();
                }

            }
        }
    }
}