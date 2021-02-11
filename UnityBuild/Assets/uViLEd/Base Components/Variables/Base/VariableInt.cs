using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Int", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for a integer number", Color = VLEColor.Cyan)]
        public class VariableInt : Variable<int> { }
    }
}
