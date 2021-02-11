using System.Collections.Generic;
using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public enum HideSetGetFlags
        {
            None,
            HideSet,
            HideGet
        }
       
        public abstract class SetGetAbstract<T> : LogicComponent, IInputPointParse, IOutputPointParse
        {
            [Tooltip("input point for setting value from external data")]
            public INPUT_POINT<T> SetExternal = new INPUT_POINT<T>();
            [Tooltip("input point for setting from the internal value of the component")]
            public INPUT_POINT SetInternal = new INPUT_POINT();
            [Tooltip("input point for get value")]
            public INPUT_POINT Get = new INPUT_POINT();

            [Tooltip("output point that is called when the value is set")]
            public OUTPUT_POINT CompleteSet = new OUTPUT_POINT();
            [Tooltip("output point transmitting the value")]
            public OUTPUT_POINT<T> ReturnValue = new OUTPUT_POINT<T>();

            [Tooltip("a flag for configuring input and output points")]
            public HideSetGetFlags HideFlag;

            [Tooltip("internal value for set")]
            public T InternalValue;

            public override void Constructor()
            {
                SetExternal.Handler = HandlerExternalSet;
                SetInternal.Handler = HandlerInternalSet;
                Get.Handler = HandlerGet;
            }

            protected abstract void SetValue(T value);
            protected abstract T GetValue();

            private void HandlerExternalSet(T data)
            {
                SetValue(data);

                CompleteSet.Execute();
            }

            private void HandlerInternalSet()
            {
                SetValue(InternalValue);                

                CompleteSet.Execute();
            }

            private void HandlerGet()
            {
                ReturnValue.Execute(GetValue());
            }

            public IDictionary<string, object> GetInputPoints()
            {
                var returnInputPoint = new Dictionary<string, object>();

                switch (HideFlag)
                {
                    case HideSetGetFlags.HideSet:
                        returnInputPoint.Add("Get", this.GetType().GetField("Get"));
                        break;
                    case HideSetGetFlags.HideGet:
                        returnInputPoint.Add("SetExternal", this.GetType().GetField("SetExternal"));
                        returnInputPoint.Add("SetInternal", this.GetType().GetField("SetInternal"));
                        break;
                    case HideSetGetFlags.None:
                        returnInputPoint.Add("SetExternal", this.GetType().GetField("SetExternal"));
                        returnInputPoint.Add("SetInternal", this.GetType().GetField("SetInternal"));
                        returnInputPoint.Add("Get", this.GetType().GetField("Get"));
                        break;
                }

                return returnInputPoint;
            }

            public IDictionary<string, object> GetOutputPoints()
            {
                var returnOutputPoint = new Dictionary<string, object>();

                switch (HideFlag)
                {
                    case HideSetGetFlags.HideSet:
                        returnOutputPoint.Add("ReturnValue", this.GetType().GetField("ReturnValue"));
                        break;
                    case HideSetGetFlags.HideGet:
                        returnOutputPoint.Add("CompleteSet", this.GetType().GetField("CompleteSet"));
                        break;
                    case HideSetGetFlags.None:
                        returnOutputPoint.Add("ReturnValue", this.GetType().GetField("ReturnValue"));
                        returnOutputPoint.Add("CompleteSet", this.GetType().GetField("CompleteSet"));
                        break;
                }

                return returnOutputPoint;
            }
        }
    }
}
