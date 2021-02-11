using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "RunLogic", 
                           Path = "uViLEd Components/Base/Logic/Utils", 
                           Tooltip = "An component for loading and starting a logic from external resources (TextAsset)\n\n"+
                           "Note:  this component purpose is to execute logics, which are not added to the scene, and not initialized at the starting moment."+ 
                           "They can live in a variable, be loaded from resources or even from the remote server. With RunAsInstance mode enabled, such logic can be instantiated as multiple copies, for example, each controlling own object", 
                           Color = VLEColor.DarkCyan)]
        public class RunLogic : LogicComponent, IInputPointParse, IOutputPointParse, IDisposable
        {
            [Tooltip("input point for transferring data to logic instance. This point should be called before logic point")]
            public INPUT_POINT<object> Data = new INPUT_POINT<object>();
            [Tooltip("input point for loading and starting a logic from the TextAsset")]
            public INPUT_POINT<TextAsset> Logic = new INPUT_POINT<TextAsset>();
            [Tooltip("input point for clearing the component cache")]
            public INPUT_POINT ClearCaсhe = new INPUT_POINT();
            [Tooltip("output point, called when the logic is finished")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();
            [Tooltip("output point that transmits identifier of logic instance")]
            public OUTPUT_POINT<string> InstanceId = new OUTPUT_POINT<string>();

            [ViewInEditor]
            [Tooltip("a flag for run a logic as an instance")]
            public bool RunAsInstance = false;
            [Tooltip("a flag for caching diagram data (to prevent re-loading the logic from the asset if the logic was started earlier)")]
            public bool CaсheLogicData = false;

            private object _data = null;

            private LogicStorage _logicStorage;
            private TextAsset _prevBinaryData;
            private bool _logucRunned = false;

            public override void Constructor()
            {
                Data.Handler = DataHandler;
                Logic.Handler = LogicHandler;
                ClearCaсhe.Handler = ClearCaсheHandler;
            }          

            private void DataHandler(object value)
            {
                _data = value;   
            }

            private void ClearCaсheHandler()
            {
                _logicStorage = null;
                _prevBinaryData = null;
            }

            private void LogicHandler(TextAsset binaryData)
            {
                if (_logucRunned) return;

                void errorLoadLogic()
                {
                    Debug.LogErrorFormat("uViLEd: error deserialization logic data from [{0}], component [{1}]", binaryData.name, name);
                };

                if (!RunAsInstance)
                {
                    var logicStorage = LogicStorage.Load(binaryData);

                    if (logicStorage != null)
                    {
                        _logucRunned = true;

                        LogicController.Instance.RunLogicExternal(logicStorage);

                        Complete.Execute();
                    }
                    else
                    {
                        errorLoadLogic();
                    }                   
                }
                else
                {
                    if (CaсheLogicData)
                    {
                        if (_prevBinaryData != binaryData)
                        {
                            _logicStorage = LogicStorage.Load(binaryData);

                            _prevBinaryData = binaryData;
                        }

                        if (_logicStorage != null)
                        {
                            InstanceId.Execute(LogicController.Instance.RunLogicInstance(_logicStorage, _data));
                        }
                        else
                        {
                            errorLoadLogic();
                        }                        
                    }else
                    {
                        var logicStorage = LogicStorage.Load(binaryData);

                        if (logicStorage != null)
                        {
                            InstanceId.Execute(LogicController.Instance.RunLogicInstance(logicStorage, _data));                            
                        }
                        else
                        {
                            errorLoadLogic();
                        }
                    }                    
                }

                             
            }

            public IDictionary<string, object> GetOutputPoints()
            {
                var outputPoints = new Dictionary<string, object>();

                if(RunAsInstance)
                {
                    outputPoints.Add("InstanceId", this.GetType().GetField("InstanceId"));
                }else
                {
                    outputPoints.Add("Complete", this.GetType().GetField("Complete"));
                }

                return outputPoints;
            }

            public IDictionary<string, object> GetInputPoints()
            {
                var inputPoints = new Dictionary<string, object>();

                if (RunAsInstance)
                {
                    inputPoints.Add("Data", this.GetType().GetField("Data"));
                }
                
                inputPoints.Add("Logic", this.GetType().GetField("Logic"));                

                if(CaсheLogicData && RunAsInstance)
                {
                    inputPoints.Add("ClearCaсhe", this.GetType().GetField("ClearCaсhe"));
                }

                return inputPoints;
            }

            public void Dispose()
            {
                ClearCaсheHandler();
            }
        }
    }
}