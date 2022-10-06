using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;

public class DaySystem : MonoBehaviour
{
    public bool localDay;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddHandler<Events.NightTime>(OnDayChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.Instance.Fire(new NightTime(Global.love.day));
        }
    }

    public void OnDayChange(NightTime evt)
    {
        localDay = evt.GetBool;
        if (evt.GetBool == false)
        {
            Global.love.currentNodeID = 1;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<Events.NightTime>(OnDayChange);
    }
}
