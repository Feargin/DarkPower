using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(InputAxisDefinition), true)]
        public class InputAxisDefinitionPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                var axis = GetInputAxis();         
                
                var nameField = property.FindPropertyRelative("Name");
                var nameIndex = 0;

                if (nameField.stringValue.Length > 0)
                {
                    nameIndex = axis.IndexOf(nameField.stringValue);
                }

                nameIndex = EditorGUILayout.Popup(property.name, nameIndex, axis.ToArray());

                nameField.stringValue = axis[nameIndex];                
                
                EditorGUI.EndProperty();
            }

            public List<string> GetInputAxis()
            {
                var allAxis = new List<string>();
                var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
                var axesProperty = serializedObject.FindProperty("m_Axes");

                axesProperty.Next(true);
                axesProperty.Next(true);

                while (axesProperty.Next(false))
                {
                    SerializedProperty axis = axesProperty.Copy();
                    axis.Next(true);
                    allAxis.Add(axis.stringValue);
                }

                return allAxis;
            }
        }
    }
}
