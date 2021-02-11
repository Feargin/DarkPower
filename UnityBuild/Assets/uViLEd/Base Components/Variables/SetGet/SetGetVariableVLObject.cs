namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "SetGetVariableVLObject", 
                           Path = "uViLEd Components/Base/Utils/Variable/SetGet", 
                           Tooltip = "An component for setting and retrieving the value of a VLObject variable", 
                           Color = VLEColor.Yellow)]
        public class SetGetVariableVLObject : SetGetVariableAbstract<VLObject> { }
    }
}