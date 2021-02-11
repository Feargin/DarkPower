namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableBool", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a bool variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableBool : SetGetVariableAbstract<bool> { }
    }
}