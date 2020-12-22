using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class BuildCustomProcess
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {

        string[] pathComponents = pathToBuiltProject.Split('/');
        string folderpath = pathToBuiltProject.Substring(0, pathToBuiltProject.Length - pathComponents.Last().Length);
        string executablepath = pathComponents.Last();
        Debug.Log("Code succesfully built");
        if (File.Exists(Application.dataPath + "/Resources/manifest.dat"))
        {
            string json = File.ReadAllText(Application.dataPath + "/Resources/manifest.dat");
            dynamic manifest = JObject.Parse(json);
            manifest.localLogFolder = PlayerSettings.productName;
            manifest.executableName = executablepath;
            manifest.version = Application.version;
            manifest.producer = UnityEditor.PlayerSettings.companyName;

            File.WriteAllText(folderpath + "manifest.json", manifest.ToString());
            Debug.Log("Manifest succesfully built");

            string zipPath = pathToBuiltProject.Substring(0, pathToBuiltProject.Length - (pathComponents.Last().Length + pathComponents.ElementAt(pathComponents.Length - 2).Length + 1));

            try
            {
                if (File.Exists(zipPath + pathComponents.Last().Replace(".exe", "") + ".zip"))
                {
                    File.Delete(zipPath + pathComponents.Last().Replace(".exe", "") + ".zip");
                }
                ZipFile.CreateFromDirectory(folderpath, zipPath + pathComponents.Last().Replace(".exe", "") + ".zip");
                Debug.Log("Zip succesfully created in " + zipPath + " with name " + pathComponents.Last().Replace(".exe", "") + ".zip");
            }
            catch
            {
                Debug.LogError("Error in compressing the folder");
            }
        }
        else
        {
            Debug.LogError("Missing manifest in Resource Folder");
        }
    }
}
