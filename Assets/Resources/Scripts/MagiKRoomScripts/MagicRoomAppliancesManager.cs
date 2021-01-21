using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MagicRoomAppliancesManager : MonoBehaviour
{
    public List<string> Appliances
    {
        get;
        private set;
    }

    private readonly string address = "http://localhost:7071";

    void Start()
    {
        MagicRoomManager.instance.Logger.AddToLogNewLine("ServerSP", "Searched Magic Room Appliances");
        Appliances = new List<string>();
        SendConfigurationRequest();
    }

    private void SendConfigurationRequest() {
        SmartPlugCommand cmd = new SmartPlugCommand
        {
            type = "SmartPlugDiscovery"
        };
        StartCoroutine(SendCommand(cmd, (body) =>
        {
            Debug.Log(body);
            ServerSmartPlugConfiguration conf = JsonUtility.FromJson<ServerSmartPlugConfiguration>(body);
            Appliances.Clear();
            Appliances.AddRange(conf.configuration);
            MagicRoomManager.instance.Logger.AddToLogNewLine("ServerSP", Appliances.ToString());
        }));
    }

    public void SendChangeCommand(string appliance, string cmd)
    {
        cmd = cmd.ToUpper();
        if (cmd != "ON" && cmd != "OFF")
            return;

        SmartPlugCommand command = new SmartPlugCommand()
        {
            type = "SmartPlugCommand",
            command = cmd,
            id = appliance
        };
        MagicRoomManager.instance.Logger.AddToLogNewLine(appliance, cmd.ToUpper());
        StartCoroutine(SendCommand(command));
    }

    private IEnumerator SendCommand(SmartPlugCommand command, MagicRoomManager.WebCallback callback = null)
    {
        string json = JsonUtility.ToJson(command);
        byte[] body = new System.Text.UTF8Encoding().GetBytes(json);
        UnityWebRequest request = new UnityWebRequest(address, "POST")
        {
            uploadHandler = new UploadHandlerRaw(body),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (!request.isNetworkError)
        {
            callback?.Invoke(request.downloadHandler.text);
        }
    }
}

[Serializable]
public class SmartPlugCommand
{
    public string type;
    public string command;
    public string id;
}
[Serializable]
public class ServerSmartPlugConfiguration
{
    public string[] configuration;
}


