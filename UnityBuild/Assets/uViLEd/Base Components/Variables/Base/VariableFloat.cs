using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Float", Path = "uViLEd Components/Base/Variable/Base", Tooltip = "Variable for a floating-point number", Color = VLEColor.Cyan)]
        public class VariableFloat : Variable<float> { }
    }
}
