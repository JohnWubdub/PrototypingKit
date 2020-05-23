using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by John Wanamaker
//passed down from generation to generation
public class Global : MonoBehaviour 
{
    //a singleton that allows for the storage of multiple variables
    
    public static Global me;
    
    //put your public variables in here
    
    void Awake()
    {
        me = this;
    }
}
