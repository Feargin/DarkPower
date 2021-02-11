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
        [VLECustomComponentDrawer(typeof(Components.ComeFromLogic))]
        public class ComeFromLogicCustomDrawer : VLEComponent
        {
            public class CustomMenuWidget : MenuWidget
            {
                public CustomMenuWidget(IComponentWidget baseComponent) : base(baseComponent)
                {
                }

                protected override GenericMenu GetComponentMenu()
                {
                    var componentMenu = base.GetComponentMenu();

                    componentMenu.AddSeparator(string.Empty);

                    componentMenu.AddItem(new GUIContent("Open Come Logic"), false, () =>
                    {
                        var comeFromLogicComponent = (Components.ComeFromLogic)((VLEWidget)component).Instance;

                        VLECommon.ExternalOpenLogic(comeFromLogicComponent.LogicId.ToId);
                    });
                    
                    return componentMenu;
                }                
            }

            public ComeFromLogicCustomDrawer(LogicStorage.ComponentsStorage.ComponentData componentStorageData) : base(componentStorageData)
            {
            }

            protected override void PrepareChildWidget()
            {
                base.PrepareChildWidget();

                ReplaceWidget<MenuWidget, CustomMenuWidget>();
            }
        }
    }
}
