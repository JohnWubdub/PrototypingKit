using System;

[System.Serializable]
public class Node 
{
    public int ID; //nodeID
    public string choiceText; //what to display for when it's a choice
    public string questionText; //what to display for when it's a question
    public int choiceID1; //associated node
    public int choiceID2;
    public int choiceID3;
    //could add a system design thing here that says what value it's adding to, so the value that it adds on selection can be balanced later
    //values can be adjusted later in global

    //constructor for each new node
    public Node(int nodeID, string nodeChoiceText, string nodeQuestionText, int nodeChoiceID1, int nodeChoiceID2, int nodeChoiceID3) 
    {
        ID = nodeID;
        choiceText = nodeChoiceText;
        questionText = nodeQuestionText;
        choiceID1 = nodeChoiceID1;
        choiceID2 = nodeChoiceID2;
        choiceID3 = nodeChoiceID3;
    }
}
