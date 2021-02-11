using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(LogicEventDefinition), true)]
        public class LogicEventDefinitionPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {                
                var logicController = VLEditor.VLECommon.CurrentLogicController;

                var toIdProperty = property.FindPropertyRelative("ToId");
                var fromIdProperty = property.FindPropertyRelative("FromId");
                var nameProperty = property.FindPropertyRelative("_name");

                var sceneLogicList = new List<Core.LogicController.SceneLogicData>(logicController.SceneLogicList);                                

                var openLogic = logicController.SceneLogicList.Find((data) =>
                {
                    return data.Id == VLEditor.VLECommon.CurrentLogic.Id;
                });

                sceneLogicList.Remove(openLogic);                

                var names = new List<string>();

                for(var i = 0; i < sceneLogicList.Count; i++)
                {
                    names.Add(sceneLogicList[i].Name);
                }

                var currentLogic = sceneLogicList.Find((data) =>
                {
                    return data.Id == toIdProperty.stringValue;
                });

                var index = sceneLogicList.IndexOf(currentLogic);

                index = EditorGUI.Popup(position, "Logic", index, names.ToArray());

                if (index >= 0)
                {
                    currentLogic = sceneLogicList[index];

                    toIdProperty.stringValue = currentLogic.Id;
                    fromIdProperty.stringValue = VLEditor.VLECommon.CurrentLogic.Id;
                    nameProperty.stringValue = currentLogic.Name;                    
                }                
            }
        }
    }
}

