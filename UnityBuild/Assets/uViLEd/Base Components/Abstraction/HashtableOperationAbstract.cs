using System.Collections;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public struct HashtablePair
        {
            public object Key;
            public object Value;

            public HashtablePair(object key, object value)
            {
                Key = key;
                Value = value;
            }
        }

        public abstract class HashtableOperationAbstract : LogicComponent
        {
            public VARIABLE_LINK<Hashtable> Variable = new VARIABLE_LINK<Hashtable>();            
        }
    }
}
