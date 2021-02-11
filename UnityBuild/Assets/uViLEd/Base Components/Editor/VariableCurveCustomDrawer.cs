﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace uViLEd
{
    namespace VLEditor
    {
        [VLECustomComponentDrawer(typeof(Components.VariableAnimationCurve))]
        public class VariableCurveCustomDrawer : VLEVariable
        {
            public class VariableCurveValueWidget : VariableValueWidget
            {
                public VariableCurveValueWidget(VariableValueData valueData, IWidgetContainer rootContainer) : base(valueData, rootContainer) { }

                public override void RegisterStyle() { }
                public override void UpdateView() { }
                public override void Draw()
                { 
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUI.CurveField(valueAreaRect, (AnimationCurve)valueData.Value);
                    EditorGUI.EndDisabledGroup();
                }
            }

            public VariableCurveCustomDrawer(Core.LogicStorage.ComponentsStorage.ComponentData componentStorage) : base(componentStorage) { }

            protected override void PrepareChildWidget()
            {
                ReplaceWidget<VariableValueWidget, VariableCurveValueWidget>();

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