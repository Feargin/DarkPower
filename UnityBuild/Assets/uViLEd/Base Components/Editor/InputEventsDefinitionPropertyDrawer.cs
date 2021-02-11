using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(InputEventsDefinition), true)]
        public class InputEventsDefinitionPropertyDrawer : PropertyDrawer
        {
            private string[] _inputEventsName = new string[] { "Down", "Up", "Hold" };

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                var mask = property.FindPropertyRelative("_mask");

                var newMask = EditorGUI.MaskField(position, "Events", mask.intValue, _inputEventsName);

                if (newMask < -1)
                {
                    mask.intValue = 0x07 & newMask;

                    Debug.Log(mask.intValue);
                }
                else
                {
                    mask.intValue = newMask;
                }
                
                EditorGUI.EndProperty();
            }
        }
    }
}