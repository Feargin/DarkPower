using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public enum DebugInfoType
        {
            Log,
            Warning,
            Error,
            LogFormat,
            WarningFormat,
            ErrorFormat
        }

        [ComponentDefinition(Name = "Debug", 
                           Path = "uViLEd Components/Base/Debug", 
                           Tooltip = "the component outputs debugging data to the Unity console. Also, outputs data to the log file in the application build", 
                           Color = VLEColor.DarkOrange)]        
        public class DebugComponent : LogicComponent
        {                            
            [Tooltip("input point for outputting external data to the console, if the debugging type uses formatting, then the data will be substituted into the DebugStr string if it has the form of a formatted string")]
            public INPUT_POINT<object> LogExternalData = new INPUT_POINT<object>();
            [Tooltip("input point of outputting the line set in the DebugStr parameter to the console")]
            public INPUT_POINT LogDebugStr = new INPUT_POINT();

            [ViewInEditor]
            [Tooltip("flag specifying the way to output debugging data. It uses the same type as Unity")]
            public DebugInfoType DebugType;

            [ViewInEditor]
            [Tooltip("the string for output to the console, can have a formatted view. Can be formatted")]
            public string DebugStr = string.Empty;          

            public override void Constructor()
            {
                LogExternalData.Handler = LogExternalDataHandler;
                LogDebugStr.Handler = LogDebugStrHandler;                
            }

            private void LogExternalDataHandler(object value)
            {
                switch (DebugType)
                {
                    case DebugInfoType.Log:
                        Debug.Log(value.ToString());
                        break;
                    case DebugInfoType.Warning:
                        Debug.LogWarning(value.ToString());
                        break;
                    case DebugInfoType.Error:
                        Debug.LogError(value.ToString());
                        break;
                    case DebugInfoType.LogFormat:
                        Debug.LogFormat(DebugStr, value);
                        break;
                    case DebugInfoType.WarningFormat:
                        Debug.LogWarningFormat(DebugStr, value);
                        break;
                    case DebugInfoType.ErrorFormat:
                        Debug.LogErrorFormat(DebugStr, value);
                        break;
                }
            }

            private void LogDebugStrHandler()
            {
                switch(DebugType)
                {
                    case DebugInfoType.Log:
                        Debug.Log(DebugStr);
                        break;                    
                    case DebugInfoType.Warning:
                        Debug.LogWarning(DebugStr);
                        break;
                    case DebugInfoType.Error:
                        Debug.LogError(DebugStr);
                        break;
                    
                }
            }           
        }
    }
}
