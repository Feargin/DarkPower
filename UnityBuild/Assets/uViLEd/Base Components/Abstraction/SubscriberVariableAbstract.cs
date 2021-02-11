using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class SubscriberVariableAbstract<T> : LogicComponent
        {
            public OUTPUT_POINT OnChanged = new OUTPUT_POINT();
            public OUTPUT_POINT<T> OnSet = new OUTPUT_POINT<T>();

            public VARIABLE_LINK<T> Variable = new VARIABLE_LINK<T>();

            public override void Constructor()
            {             
                Variable.AddChangedEventHandler(ChangedHandler);
                Variable.AddSetEventHandler(SetHandler);
            }

            private void SetHandler(T newValue)
            {
                OnSet.Execute(newValue);
            }

            private void ChangedHandler()
            {
                OnChanged.Execute();
            }            
        }
    }
}
