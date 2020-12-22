using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MagicRoomManager))]
[CanEditMultipleObjects]
public class MagicRommManagerEditor : Editor
{
    SerializedProperty portHTTP;

    SerializedProperty activityidentifier;
    SerializedProperty activityName;

    SerializedProperty Lights;
    SerializedProperty Appliances;
    SerializedProperty TextToSpeech;
    SerializedProperty Kinect;
    SerializedProperty SmartToy;

    SerializedProperty indexScene;

    void OnEnable()
    {
        portHTTP = serializedObject.FindProperty("portHTTP");
        Lights = serializedObject.FindProperty("Lights");
        Appliances = serializedObject.FindProperty("Appliances");
        TextToSpeech = serializedObject.FindProperty("TextToSpeech");
        Kinect = serializedObject.FindProperty("Kinect");
        SmartToy = serializedObject.FindProperty("SmartToy");
        indexScene = serializedObject.FindProperty("indexScene");
        activityidentifier = serializedObject.FindProperty("activityidentifier");
        activityName = serializedObject.FindProperty("activityName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MagicRoomManager m = (MagicRoomManager)target;

        EditorGUILayout.LabelField("Configure the basic Element of your\n apllication in the Magic Room", EditorStyles.boldLabel, GUILayout.Height(40));

        EditorGUILayout.Space(); 

        EditorGUILayout.PropertyField(activityidentifier, new GUIContent("Activity id: ", "Obtian the id form the registering system in Magika's developer website"));
        EditorGUILayout.PropertyField(activityName, new GUIContent("Activity name: ", "The common name associated to your game in Magika's developer website "));
        GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width), GUILayout.Height(2));

        EditorGUILayout.LabelField("Configuration Parameter For HTTP", EditorStyles.boldLabel);
        EditorGUILayout.IntSlider(portHTTP, 7000, 7099, "Port: ");
        //EditorGUILayout.PropertyField(addressHTTP, new GUIContent("Accepting address"));
        //EditorGUILayout.PropertyField(portHTTP);

        GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width), GUILayout.Height(2));

        EditorGUILayout.LabelField("Select the component to activate:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(Lights);
        EditorGUILayout.PropertyField(Appliances);
        EditorGUILayout.PropertyField(TextToSpeech);
        EditorGUILayout.PropertyField(Kinect);
        EditorGUILayout.PropertyField(SmartToy);

        GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width), GUILayout.Height(2));

        EditorGUILayout.LabelField("First scene index", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(indexScene);
        
        EditorGUILayout.IntSlider(indexScene, 0, UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings, "Menu Scene Index");

        GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width), GUILayout.Height(2));
        EditorGUILayout.Space();
        if (GUILayout.Button("Generate Activity Manifest"))
        {
            WriteManifestFile();
        }
        serializedObject.ApplyModifiedProperties();
    }


    private void WriteManifestFile() {
        MagicRoomManager m = (MagicRoomManager)target;
        dynamic manifest = new JObject();
        manifest.id = m.activityidentifier;
        manifest.friendlyName = m.activityName;
        manifest.executableName = Application.productName + ".exe";
        manifest.port = m.portHTTP;
        File.WriteAllText(Application.dataPath + "/Resources/manifest.dat", manifest.ToString());
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
