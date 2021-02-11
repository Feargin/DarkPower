using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class VLObjectSetGetAbstract<UnityObjectType, ParamType> : SetGetAbstract<ParamType> where UnityObjectType : UnityEngine.Object
        {
            public VARIABLE_LINK<VLObject> UnityObject = new VARIABLE_LINK<VLObject>();

            protected UnityObjectType unityObject
            {
                get
                {                    
                    return UnityObject.Value.Get<UnityObjectType>();
                }
            }

            protected abstract ParamType VLObjectGetValue();
            protected abstract void VLObjectSetValue(ParamType value);

            protected override ParamType GetValue()
            {
                var returnedValue = default(ParamType);

                DoActionAfterValidation(() =>
                {
                    returnedValue = VLObjectGetValue();
                });

                return returnedValue;
            }

            protected override void SetValue(ParamType value)
            {
                DoActionAfterValidation(() =>
                {
                    VLObjectSetValue(value);
                });              
            }

            protected virtual void DoActionAfterValidation(Action action, Action failedValidationHandler = null)
            {                
                if (unityObject != null)
                {
                    action();
                }
                else
                {
                    Debug.LogWarningFormat("[uViLEd]: unityObject [{0}] is null or was destroyed. [LogicComponent: {1}]", typeof(UnityObjectType).Name, name);

                    failedValidationHandler?.Invoke();
                }
            }
        }
    }
}