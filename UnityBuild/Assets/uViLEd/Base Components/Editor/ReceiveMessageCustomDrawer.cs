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
        [VLECustomComponentDrawer(typeof(Components.ReceiveMessageComponent))]
        public class ReceiveMessageCustomDrawer : VLEComponent
        {
            public class ReceiveMessageMenuWidget : MenuWidget
            {
                public ReceiveMessageMenuWidget(IComponentWidget baseComponent) : base(baseComponent)
                {
                }

                protected override GenericMenu GetComponentMenu()
                {
                    var component = base.GetComponentMenu();

                    var senders = GetSenders();

                    if (senders.Count != 0)
                    {
                        component.AddSeparator(string.Empty);

                        foreach (var data in senders)
                        {
                            var name = data.Key;
                            var id = data.Value;

                            component.AddItem(new GUIContent("Senders/{0}".Fmt(name)), false, () =>
                            {
                                VLECommon.ExternalOpenLogic(id);
                            });
                        }
                    }

                    return component;
                }

                private Dictionary<string, string> GetSenders()
                {
                    var sendersData = new Dictionary<string, string>();
                    var receiveMessageComponent = (Components.ReceiveMessageComponent)((VLEWidget)component).Instance;

                    foreach (var logicData in VLECommon.CurrentLogicController.SceneLogicList)
                    {
                        var logic = LogicStorage.Load(logicData.BinaryData);

                        var receiver = logic.Components.Items.Find((item) =>
                        {
                            if (string.Compare(item.Type, "uViLEd.Components.SendMessageComponent", StringComparison.Ordinal) == 0)
                            {
                                var instanceType = AssemblyHelper.GetAssemblyType(item.Assembly, item.Type);
                                var instance = ScriptableObject.CreateInstance(instanceType) as Components.SendMessageComponent;

                                JsonUtility.FromJsonOverwrite(item.JsonData, instance);

                                if (instance != null)
                                {
                                    return instance.Message.Id == receiveMessageComponent.Message.Id;
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

                        if (receiver != null)
                        {
                            sendersData.Add(logic.Name, logic.Id);
                        }
                    }

                    return sendersData;
                }
            }

            public ReceiveMessageCustomDrawer(LogicStorage.ComponentsStorage.ComponentData componentStorageData) : base(componentStorageData)
            {
            }

            protected override void PrepareChildWidget()
            {
                base.PrepareChildWidget();

                ReplaceWidget<MenuWidget, ReceiveMessageMenuWidget>();
            }
        }
    }
}
