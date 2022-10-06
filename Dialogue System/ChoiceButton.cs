using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    public int choiceNumber;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void Select()
    {
        switch (choiceNumber)
        {
            case 1:
                Global.love.currentNodeID = Global.love.DialogueDictionary[Global.love.currentNodeID].choiceID1;
                break;
            case 2:
                Global.love.currentNodeID = Global.love.DialogueDictionary[Global.love.currentNodeID].choiceID2;
                break;
            case 3:
                Global.love.currentNodeID = Global.love.DialogueDictionary[Global.love.currentNodeID].choiceID3;
                break;
        }
    }
    
    
    public void OnPointerClick(PointerEventData eventData) 
    {
        Select();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    
 
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
