using System.Collections;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Hashtable", 
                           Path = "uViLEd Components/Base/Variable/Dynamic", 
                           Tooltip = "Variable for a hash table. The store of key-value pairs. Both the key and the value can be of any arbitrary type", 
                           Color = VLEColor.Cyan)]
        public class VariableHashtable : Variable<Hashtable> { }
    }
}