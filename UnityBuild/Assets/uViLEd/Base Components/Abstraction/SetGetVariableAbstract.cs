using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {      
        public abstract class SetGetVariableAbstract<T> : SetGetAbstract<T>
        {                     
            public VARIABLE_LINK<T> Variable = new VARIABLE_LINK<T>();

            protected override void SetValue(T value)
            {
                Variable.Value = value;
            }

            protected override T GetValue()
            {
                return Variable.Value;
            }
        }
    }
}