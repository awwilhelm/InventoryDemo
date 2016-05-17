using UnityEngine;
using System.Collections;
using UnityEditor;

public class ItemEditor : Editor {

    SerializedProperty lookAtPoint;
    private float percentage;
    private float other;
    
    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        //EditorGUILayout.PropertyField(lookAtPoint);
        //serializedObject.ApplyModifiedProperties();

        //   SerializedProperty prop = serializedObject.FindProperty("m_Script");
        //EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
        //serializedObject.ApplyModifiedProperties();

        //percentage = EditorGUILayout.FloatField("Percentage: ", percentage);
        //other = EditorGUILayout.FloatField("other: ", other);
        GUILayout.Label("Type two selected!");
        GUILayout.Label("Type two option!");
        //Items it = EditorGUILayout.ObjectField(it, typeof(Items), true) as Items;
    }
}
