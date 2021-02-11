using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Animations;
using System;

namespace uViLEd
{    
    namespace Components
    {
        [CustomPropertyDrawer(typeof(AnimationEventDefinition), true)]
        public class AnimationEventDefinitionPropertyDrawer : VLEditor.DefinitionPropertyDrawerAbstract
        {
            private Dictionary<int, bool> _foldOutState = new Dictionary<int, bool>();

            protected override void Draw(SerializedProperty property)
            {
                var componentVariableLinks = VLEditor.VLECommon.CurrentLogic.Links.GetVariableLinks(VLEditor.VLECommon.SelectedWidget.Id);

                if (componentVariableLinks.Count > 0)
                {
                    var link = componentVariableLinks[0];
                    var variable = VLEditor.VLECommon.CurrentLogic.Components.GetComponent(link.TargetComponent);
                    var component = (VariableVLObject)VLEditor.VLELogicEditor.GetComponentInstance(variable.Id);

                    if (component != null)
                    {
                        var vleController = VLEditor.VLECommon.CurrentLogicController;
                        var animator = vleController.GetObject(component.Value.Id) as Animator;

                        if (animator != null)
                        {
                            var assetPath = AssetDatabase.GetAssetPath(animator.runtimeAnimatorController);
                            var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(assetPath);

                            var clipProperty = property.FindPropertyRelative("ClipIndex");
                            var clipsName = new List<string>();

                            for (var i = 0; i < controller.animationClips.Length; i++)
                            {
                                var clip = controller.animationClips[i];

                                clipsName.Add(clip.name);
                            }

                            clipProperty.intValue = EditorGUILayout.Popup("Animation Clip", clipProperty.intValue, clipsName.ToArray());

                            if (clipProperty.intValue >= 0)
                            {
                                var clip = controller.animationClips[clipProperty.intValue];

                                var frames = (int)(clip.length * clip.frameRate);

                                var framesArray = property.FindPropertyRelative("FrameEvents");

                                if (GUILayout.Button("Add Frame"))
                                {
                                    framesArray.InsertArrayElementAtIndex(framesArray.arraySize);

                                    var frameParameterProperty = framesArray.GetArrayElementAtIndex(framesArray.arraySize - 1);
                                    var eventNameProperty = frameParameterProperty.FindPropertyRelative("Name");

                                    eventNameProperty.stringValue = "Event {0}".Fmt(framesArray.arraySize);
                                }

                                for (var i = 0; i < framesArray.arraySize; i++)
                                {
                                    var frameParameterProperty = framesArray.GetArrayElementAtIndex(i);

                                    var eventNameProperty = frameParameterProperty.FindPropertyRelative("Name");
                                    var frameProperty = frameParameterProperty.FindPropertyRelative("Frame");

                                    if (!_foldOutState.ContainsKey(i))
                                    {
                                        _foldOutState.Add(i, false);
                                    }

                                    EditorGUILayout.BeginHorizontal();

                                    if (_foldOutState[i] = EditorGUILayout.Foldout(_foldOutState[i], eventNameProperty.stringValue))
                                    {
                                        EditorGUILayout.BeginVertical();

                                        EditorGUILayout.Space();
                                        EditorGUILayout.Space();
                                        EditorGUILayout.Space();

                                        eventNameProperty.stringValue = EditorGUILayout.TextField(eventNameProperty.stringValue);
                                        frameProperty.intValue = EditorGUILayout.IntSlider(frameProperty.intValue, 0, frames);

                                        EditorGUILayout.EndVertical();
                                    }

                                    if (GUILayout.Button("-", GUILayout.Width(25f)))
                                    {
                                        framesArray.DeleteArrayElementAtIndex(i);
                                    }

                                    EditorGUILayout.EndHorizontal();
                                }
                            }
                        }
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("AnimationEventParameter: please select variable");
                }
            }
        }
    }
}
