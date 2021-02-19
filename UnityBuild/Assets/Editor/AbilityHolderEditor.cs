using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AbilityHolder.AbilityID))]
public class AbilityHolderEditor : EditorWindow
{
    public AbilityHolder.AbilityID abilityID;
    [MenuItem("Examples/Editor GUILayout Enum Popup usage")]
    /*static void Init()
    {
        EditorWindow window = GetWindow(typeof(AbilityHolderEditor));
        window.Show();
    }*/
    
    public void OnInspectorGUI()
    {
        abilityID = (AbilityHolder.AbilityID)EditorGUILayout.EnumPopup("Тип скилла:", abilityID);
    }
}
