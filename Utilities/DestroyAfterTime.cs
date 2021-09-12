using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gets rid of the attached GameObject after a specified amount of (scaled) time.
//Could be updated to use with an object pool (SetActive(false) instead of Destroy(gameObject)), or unscaled deltaTime.
//Hella simple, but I find myself making this script a lot.
public class DestroyAfterTime : MonoBehaviour
{
    public float TimeUntilDestruction;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = TimeUntilDestruction;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
