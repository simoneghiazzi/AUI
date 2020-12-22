using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerMovementSimultor))]
public class PlayerMovementSimulatorEditor : Editor
{
    void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("The simulated player id is " + (((PlayerMovementSimultor)target).id + 1));

        EditorGUILayout.Space();
        EditorStyles.label.wordWrap = true;

        EditorGUILayout.LabelField("To move the player keep pressed the corresponding id number. To move forward and backward use the up and down arrow. To turn use Left and Right arrow. To jump press space");

        serializedObject.ApplyModifiedProperties();
    }
}
