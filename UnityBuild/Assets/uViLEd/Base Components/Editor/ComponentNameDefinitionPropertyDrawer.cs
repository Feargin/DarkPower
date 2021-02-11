using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

using uViLEd.VLEditor;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(ComponentNameDefinition), true)]
        public class ComponentNameDefinitionPropertyDrawer : DefinitionPropertyDrawerAbstract
        {
            protected override void Draw(SerializedProperty property)
            {                                
                var assemblyAndTypeInfo = new List<KeyValuePair<string, string>>();                
                var shortNameList = new List<string>();
                
                var unityEngineAssemblys = AppDomain.CurrentDomain.GetAssemblies();                

                foreach (var assembly in unityEngineAssemblys)
                {
                    if (!assembly.FullName.Contains("UnityEditor") && 
                        !assembly.FullName.Contains("System") &&
                        !assembly.FullName.Contains("uViLEdCore"))
                    {
                        foreach (var type in assembly.GetTypes())
                        {
                            if ((typeof(Component).IsAssignableFrom(type) || typeof(MonoBehaviour).IsAssignableFrom(type)) &&
                                type != typeof(Component) &&
                                type != typeof(MonoBehaviour) &&
                                type != typeof(Behaviour))
                            {
                                assemblyAndTypeInfo.Add(new KeyValuePair<string, string>(assembly.FullName, type.FullName));                               
                                shortNameList.Add("{0}/{1}".Fmt(assembly.GetName().Name, type.Name));
                            }
                        }
                    }
                }          

                var componentNameProperty = property.FindPropertyRelative("_componentName");
                var componentShortNameProperty = property.FindPropertyRelative("_componentShortName");
                var assemblyProperty = property.FindPropertyRelative("_assembly");

                var currentIndex = assemblyAndTypeInfo.FindIndex((value) =>
                {
                    return value.Key == assemblyProperty.stringValue && value.Value == componentNameProperty.stringValue;
                });

                var newIndex = EditorGUILayout.Popup(currentIndex, shortNameList.ToArray());

                if (newIndex < assemblyAndTypeInfo.Count && newIndex >= 0)
                {
                    assemblyProperty.stringValue = assemblyAndTypeInfo[newIndex].Key;
                    componentNameProperty.stringValue = assemblyAndTypeInfo[newIndex].Value;
                }

                if (newIndex != currentIndex)
                {
                    var shortName = shortNameList[newIndex];
                    var startIndex = shortName.LastIndexOf('/') + 1;

                    componentShortNameProperty.stringValue = shortName.Substring(startIndex, shortName.Length - startIndex);                    

                    VLECommon.UnsavedChanges++;
                }                
            }
        }
    }
}
