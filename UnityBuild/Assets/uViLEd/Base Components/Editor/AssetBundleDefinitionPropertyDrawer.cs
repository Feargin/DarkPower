using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace uViLEd
{
    namespace Components
    {
        [CustomPropertyDrawer(typeof(AssetBundleDefinition), true)]
        public class AssetBundleDefinitionPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);                             

                var bundleNames = AssetDatabase.GetAllAssetBundleNames();
                var selectedBundle = -1;
                var bundleUrlProperty = property.FindPropertyRelative("_assetBundleName");

                for (var i = 0; i < bundleNames.Length; i++)
                {
                    if (string.Compare(bundleNames[i], bundleUrlProperty.stringValue, System.StringComparison.Ordinal) == 0)
                    {
                        selectedBundle = i;
                        break;
                    }
                }

                var newSelectedBundle = EditorGUI.Popup(position, property.displayName, selectedBundle, bundleNames);

                if (newSelectedBundle != selectedBundle && newSelectedBundle >= 0)
                {
                    bundleUrlProperty.stringValue = bundleNames[newSelectedBundle];
                }else if (selectedBundle < 0)
                {
                    bundleUrlProperty.stringValue = string.Empty;
                }
                
                EditorGUI.EndProperty();
            }
        }
    }
}

