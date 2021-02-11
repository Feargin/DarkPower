using System.Collections.Generic;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class ListOperationAbstract : LogicComponent
        {
            public VARIABLE_LINK<List<object>> Variable = new VARIABLE_LINK<List<object>>();
        }
    }
}