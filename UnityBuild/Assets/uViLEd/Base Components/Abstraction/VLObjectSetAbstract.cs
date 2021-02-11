using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class VLObjectSetAbstract<UnityObjectType, SetParamType> : VLObjectAbstract<UnityObjectType>  where UnityObjectType : UnityEngine.Object
        {
            [Tooltip("Input point for setting a parameter from an external value")]
            public INPUT_POINT<SetParamType> SetExternal = new INPUT_POINT<SetParamType>();
            [Tooltip("Input point for setting a parameter from the internal value of the component")]
            public INPUT_POINT SetInternal = new INPUT_POINT();
            [Tooltip("output point that is called when the component work is finished")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();            

            public override void Constructor()
            {
                SetInternal.Handler = SetInternalHandler;
                SetExternal.Handler = SetExternalHandler;
            }

            protected abstract void SetValueToParam(SetParamType value);
            protected abstract SetParamType GetInternalValue();

            private void SetValue(SetParamType value)
            {
                DoActionAfterValidation(() =>
                {
                    SetValueToParam(value);

                    Complete.Execute();
                });                
            }

            private void SetInternalHandler()
            {
                SetValue(GetInternalValue());
            }

            private void SetExternalHandler(SetParamType value)
            {
                SetValue(value);
            }            
        }
    }
}
