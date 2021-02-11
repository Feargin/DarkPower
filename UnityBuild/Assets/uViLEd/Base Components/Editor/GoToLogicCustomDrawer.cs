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
        [VLECustomComponentDrawer(typeof(Components.GoToLogic))]
        public class GoToLogicCustomDrawer : VLEComponent
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

                    componentMenu.AddItem(new GUIContent("Open To Logic"), false, () =>
                    {
                        var goToLogicComponent = (Components.GoToLogic)((VLEWidget)component).Instance;

                        VLECommon.ExternalOpenLogic(goToLogicComponent.LogicId.ToId);
                    });

                    return componentMenu;
                }
            }

            public GoToLogicCustomDrawer(LogicStorage.ComponentsStorage.ComponentData componentStorageData) : base(componentStorageData)
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

