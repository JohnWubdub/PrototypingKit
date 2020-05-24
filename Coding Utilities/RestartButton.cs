using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Throw this on a gameobject to quickly reload your scene with the 'R' key.
//Makes for faster testing.

//@srgovindan
public class RestartButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Scene loadedLevel = SceneManager.GetActiveScene ();
            SceneManager.LoadScene (loadedLevel.buildIndex);
        }
    }
}
