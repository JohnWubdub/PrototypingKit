using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global love;

    public int currentNodeID;

    public bool day;
    
    public Dictionary<int, Node> DialogueDictionary = new Dictionary<int, Node>();
    
    void Awake()
    {
        love = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
