using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(LogicMessageDefinition), true)]
        public class LogicMessagePropertyDrawer : PropertyDrawer
        {
            private List<string> messageNames = new List<string>();

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                var vleController = VLEditor.VLECommon.CurrentLogicController;

                var idProperty = property.FindPropertyRelative("Id");
                var nameValue = property.FindPropertyRelative("_name");

                var currentMessage = vleController.SceneLogicMessages.Find((data)=>
                {
                    return data.Id == idProperty.intValue;
                });

                var index = 0;

                if (currentMessage != null)
                {
                    index = vleController.SceneLogicMessages.IndexOf(currentMessage);
                }

                messageNames.Clear();

                for (var i = 0; i < vleController.SceneLogicMessages.Count; i++)
                {
                    messageNames.Add(vleController.SceneLogicMessages[i].Name);
                }

                index = EditorGUI.Popup(position, "Message", index, messageNames.ToArray());

                if (index >= 0)
                {
                    var newMessage = vleController.SceneLogicMessages[index];

                    idProperty.intValue = newMessage.Id;
                    nameValue.stringValue = newMessage.Name;
                }
                
                
                EditorGUI.EndProperty();
            }     
        }
    }
}
