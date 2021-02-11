using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "Float []", Path = "uViLEd Components/Base/Variable/Array", Tooltip = "Variable for an array of floats", Color = VLEColor.Cyan)]
        public class VariableArrayFloat : Variable<float[]> { }
    }
}