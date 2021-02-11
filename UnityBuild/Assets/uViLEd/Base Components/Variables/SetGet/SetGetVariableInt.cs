namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableInt", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a int variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableInt : SetGetVariableAbstract<int> { }        
    }
}