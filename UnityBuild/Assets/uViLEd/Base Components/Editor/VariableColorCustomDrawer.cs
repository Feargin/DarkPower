using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace uViLEd
{
    namespace VLEditor
    {
        [VLECustomComponentDrawer(typeof(Components.VariableColor))]        
        public class VariableColorCustomDrawer : VLEVariable
        {
            public class VariableColorValueWidget : VariableValueWidget
            {
                private GUIStyle _boxStyle;

                public VariableColorValueWidget(VariableValueData valueData, IWidgetContainer rootContainer) : base(valueData, rootContainer) { }
                                
                public override void UpdateView() { }
                public override void Draw()
                {                   
                    EditorGUI.DrawRect(valueAreaRect, (Color)valueData.Value);
                }
            }

            public VariableColorCustomDrawer(Core.LogicStorage.ComponentsStorage.ComponentData componentStorage) : base(componentStorage) { }

            protected override void PrepareChildWidget()
            {
                ReplaceWidget<VariableValueWidget, VariableColorValueWidget>();                

                base.PrepareChildWidget();
            }

            public override void UpdateParameterValues()
            {
                var propertyInfo = VLEditorUtils.GetVariable(InstanceType);

                valueData.Value = propertyInfo.GetValue(Instance, null);               
            }     
        }
    }
}
