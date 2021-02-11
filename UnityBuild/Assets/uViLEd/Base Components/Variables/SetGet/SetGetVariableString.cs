namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableString", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a string variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableString : SetGetVariableAbstract<string> { }
    }
}