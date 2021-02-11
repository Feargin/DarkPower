namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableFloat", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a float variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableFloat : SetGetVariableAbstract<float> { }
    }
}