using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public GameObject manager;
    private JSONLoader loader;
    
    public Text question;
    public Text choice1;
    public Text choice2;
    public Text choice3;

    private int currentChoiceID1;
    private int currentChoiceID2;
    private int currentChoiceID3;

    void Start()
    {
        loader = manager.GetComponent<JSONLoader>();
    }

    
    void Update()
    {
        if (Global.love.DialogueDictionary.ContainsKey(Global.love.currentNodeID))
        {
            question.text = Global.love.DialogueDictionary[Global.love.currentNodeID].questionText;
            
            currentChoiceID1 = Global.love.DialogueDictionary[Global.love.currentNodeID].choiceID1;
            currentChoiceID2 = Global.love.DialogueDictionary[Global.love.currentNodeID].choiceID2;
            currentChoiceID3 = Global.love.DialogueDictionary[Global.love.currentNodeID].choiceID3;

            choice1.text = Global.love.DialogueDictionary[currentChoiceID1].choiceText;
            choice2.text = Global.love.DialogueDictionary[currentChoiceID2].choiceText;
            choice3.text = Global.love.DialogueDictionary[currentChoiceID3].choiceText;
        }

    }

    public void Add()
    {
        /*
        loader.AddEvent(index);
        */
    }
}
