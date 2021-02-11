using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(SceneDefinition), true)]
        public class SceneDefinitionPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);

                var sceneNameProperty = property.FindPropertyRelative("_sceneName");
                var sceneIndexProperty = property.FindPropertyRelative("_sceneIndex");

                var sceneNames = new List<string>();

                foreach (var scene in EditorBuildSettings.scenes)
                {
                    if (scene.enabled)
                    {
                        string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                        name = name.Substring(0, name.Length - 6);
                        sceneNames.Add(name);
                    }
                }

                sceneIndexProperty.intValue = EditorGUI.Popup(position, "Scene", sceneIndexProperty.intValue, sceneNames.ToArray());

                if (sceneIndexProperty.intValue >= 0 && sceneNames.Count > 0)
                {
                    sceneNameProperty.stringValue = sceneNames[sceneIndexProperty.intValue];
                }

                EditorGUI.EndProperty();
            }
        }
    }
}
