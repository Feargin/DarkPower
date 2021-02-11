using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using UnityEditor;
using uViLEd.Core;

namespace uViLEd
{
    namespace VLEditor
    {
        [VLECustomComponentDrawer(typeof(Components.SendMessageComponent))]
        public class SendMessageCustomDrawer : VLEComponent
        {
            public class SendMessageMenuWidget : MenuWidget
            {
                public SendMessageMenuWidget(IComponentWidget baseComponent) : base(baseComponent)
                {
                }

                protected override GenericMenu GetComponentMenu()
                {
                    var componentMenu = base.GetComponentMenu();

                    var recevers = GetReceivers();

                    if(recevers.Count != 0)
                    {
                        componentMenu.AddSeparator(string.Empty);

                        foreach (var data in recevers)
                        {
                            var name = data.Key;
                            var id = data.Value;

                            componentMenu.AddItem(new GUIContent("Receivers/{0}".Fmt(name)), false, () =>
                            {
                                VLECommon.ExternalOpenLogic(id);
                            });
                        }
                    }
                    
                    return componentMenu;
                }

                private Dictionary<string, string> GetReceivers()
                {
                    var receiversData = new Dictionary<string, string>();
                    var sendMessageComponent = (Components.SendMessageComponent)((VLEWidget)component).Instance;                    

                    foreach (var logicData in VLECommon.CurrentLogicController.SceneLogicList)
                    {
                        var logic = LogicStorage.Load(logicData.BinaryData);                        

                        var receiver = logic.Components.Items.Find((item) =>
                        {                         
                            if(string.Compare(item.Type, "uViLEd.Components.ReceiveMessageComponent", StringComparison.Ordinal) == 0)
                            {
                                var instanceType = AssemblyHelper.GetAssemblyType(item.Assembly, item.Type);
                                var instance = ScriptableObject.CreateInstance(instanceType) as Components.ReceiveMessageComponent;

                                JsonUtility.FromJsonOverwrite(item.JsonData, instance);

                                if(instance != null)
                                {
                                    return instance.Message.Id == sendMessageComponent.Message.Id;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }                                                                                   
                        });

                        if(receiver != null)
                        {
                            receiversData.Add(logic.Name, logic.Id);                            
                        }
                    }

                    return receiversData;
                }
            }

            public SendMessageCustomDrawer(LogicStorage.ComponentsStorage.ComponentData componentStorageData) : base(componentStorageData)
            {
            }

            protected override void PrepareChildWidget()
            {
                base.PrepareChildWidget();

                ReplaceWidget<MenuWidget, SendMessageMenuWidget>();
            }           
        }
    }
}
