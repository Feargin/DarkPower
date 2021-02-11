using System;
using UnityEngine;

namespace uViLEd
{
    namespace Components
    {       
        public abstract class GameObjectComponentAbstract<СacheComponent> : VLObjectAbstract<GameObject> where СacheComponent : Component
        {
            protected СacheComponent cacheComponent;

            public override void Constructor()
            {
                UnityObject.AddChangedEventHandler(() =>
                {
                    cacheComponent = null;

                    Validation();                    
                });
            }

            protected virtual void Start()
            {                
                Validation();                
            }                            

            protected override void DoActionAfterValidation(Action action, Action failedValidationHandler = null)
            {
                Validation();

                if (cacheComponent != null)
                {
                    action();
                }
                else
                {
                    failedValidationHandler?.Invoke();
                }
            }

            private void Validation()
            {
                if (unityObject != null && cacheComponent == null)
                {
                    cacheComponent = unityObject.GetComponent<СacheComponent>();

#if UNITY_EDITOR
                    if (cacheComponent == null)
                    {
                        Debug.LogWarningFormat("[uViLEd]: GameObject is not contains component {1}. [LogicComponent: {0}]", name, typeof(СacheComponent).Name);
                    }
#endif
                }
                else if (unityObject == null)
                {
                    cacheComponent = null;

#if UNITY_EDITOR
                    Debug.LogWarningFormat("[uViLEd]: GameObject is null or was destroyed. [LogicComponent: {0}]", name);
#endif
                }
            }
        }
    }
}
