using UnityEngine;
using UnityEngine.UI;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class GUIComponentAbstract<T> : VLObjectAbstract<T> where T : Selectable
        {            
            [Tooltip("input point for setting the interactable parameter")]
            public INPUT_POINT<bool> Interactable = new INPUT_POINT<bool>();
            [Tooltip("input point for setting the interactable parameter")]
            public INPUT_POINT Update = new INPUT_POINT();

            public override void Constructor()
            {
                base.Constructor();

                Interactable.Handler = InteractibleHandler;
                Update.Handler = UpdateHandler;
            }

            protected abstract void UpdateHandler();

            private void InteractibleHandler(bool value)
            {
                DoActionAfterValidation(() =>
                {
                    unityObject.interactable = value;
                });                
            }
        }
    }
}
