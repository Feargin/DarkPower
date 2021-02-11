using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Animations;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(MecanimParameterDefinition), true)]
        public class MecanimParameterDefinitionPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);                

                var componentVariableLinks = VLEditor.VLECommon.CurrentLogic.Links.GetVariableLinks(VLEditor.VLECommon.SelectedWidget.Id);

                if(componentVariableLinks.Count > 0)
                {
                    var link = componentVariableLinks[0];

                    var variable = VLEditor.VLECommon.CurrentLogic.Components.GetComponent(link.TargetComponent);                    

                    var component = (VariableVLObject)VLEditor.VLELogicEditor.GetComponentInstance(variable.Id);

                    if(component != null)
                    {
                        var vleController = VLEditor.VLECommon.CurrentLogicController;
                        var animator = vleController.GetObject(component.Value.Id) as Animator;

                        if (animator != null)
                        {                                                          
                            var assetPath = AssetDatabase.GetAssetPath(animator.runtimeAnimatorController);
                            var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(assetPath);                            

                            var nameProperty = property.FindPropertyRelative("Name");
                            var paramType = (AnimatorControllerParameterType)property.FindPropertyRelative("DataType").intValue;                            
                            var paramtersName = new List<string>();                            

                            for(var i = 0; i < controller.parameters.Length; i++)
                            {
                                var parameter = controller.parameters[i];

                                if (parameter.type == paramType)
                                {
                                    paramtersName.Add(parameter.name);
                                }
                            }

                            var index = paramtersName.IndexOf(nameProperty.stringValue);

                            index = EditorGUI.Popup(position, "MecanimParameter", index, paramtersName.ToArray());   
                            
                            if(index >= 0)
                            {
                                nameProperty.stringValue = paramtersName[index];
                            }
                        }                        
                    }

                }else
                {
                    EditorGUI.LabelField(position, "MecanimParameter: please select variable");
                }
                
                EditorGUI.EndProperty();
            }
        }
    }
}