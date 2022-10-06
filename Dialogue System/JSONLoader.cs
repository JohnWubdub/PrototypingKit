using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class JSONLoader : MonoBehaviour
{
    [SerializeField] 
    private string path = "Assets/";
    
    [SerializeField] 
    private string filename;
    
    private JSONNode packet;

    

    void Awake()
    {
        path += "" + filename;
        LoadJSON();
    }
    
    public void LoadJSON()
    {
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            packet = JSON.Parse(json);
            
            AssignData(); //assigns data to the list
        }
    }

    public void AssignData()
    {
        for (int i = 0; i < packet.Count; i++)
        {
            Global.love.DialogueDictionary.Add(packet[i]["ID"], new Node(packet[i]["ID"], packet[i]["choiceText"], packet[i]["questionText"],
                packet[i]["choiceID1"], packet[i]["choiceID2"], packet[i]["choiceID3"]));
        }
    }
}
