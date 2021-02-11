using System;
using UnityEngine;

namespace uViLEd
{
    namespace Components
    {            
        public abstract class GameObjectComponentSetGetAbstract<СacheComponent, ParamType> : VLObjectSetGetAbstract<GameObject, ParamType> where СacheComponent : Component
        {
            protected СacheComponent cacheComponent;

            public override void Constructor()
            {
                base.Constructor();

                UnityObject.AddChangedEventHandler(() =>
                {
                    cacheComponent = null;

                    ValidateComponent();
                });
            }

            protected abstract ParamType ComponentGetValue();
            protected abstract void ComponentSetValue(ParamType value);

            protected void Start()
            {
                ValidateComponent();
            }

            protected override ParamType VLObjectGetValue()
            {
                var returnedValue = default(ParamType);

                DoActionAfterValidation(() =>
                {
                    returnedValue = ComponentGetValue();
                });

                return returnedValue;
            }

            protected override void VLObjectSetValue(ParamType value)
            {
                DoActionAfterValidation(() =>
                {
                    ComponentSetValue(value);
                });
            }

            protected virtual void ValidateComponent()
            {
                base.DoActionAfterValidation(() =>
                {
                    if(cacheComponent == null)
                    {
                        cacheComponent = unityObject.GetComponent<СacheComponent>();
#if UNITY_EDITOR
                        if (cacheComponent == null)
                        {
                            Debug.LogWarningFormat("[uViLEd]: GameObject is not contains component {1}. [LogicComponent: {0}]", name, typeof(СacheComponent).Name);
                        }
#endif
                    }
                }, 
                ()=>
                {
                    cacheComponent = null;
                });
            }
            
            protected override void DoActionAfterValidation(Action action, Action failedValidationHandler = null)
            {
                ValidateComponent();

                if(cacheComponent != null)
                {
                    action();
                }else
                {
                    failedValidationHandler?.Invoke();
                }
            }
        }
    }
}