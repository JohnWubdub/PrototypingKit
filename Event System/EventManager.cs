using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Large parts taken from Tim Miller's post on Type Safe Events in Unity 3D: http://www.willrmiller.com/?p=87
public class GameEvent{} //This just creates the Type GameEvent to be used in the system

public class EventManager
{
    //These are the delegates that will hold the info about events within the dictionaries below
    public delegate void EventDelegate<T> (T e) where T : GameEvent;
    private delegate void EventDelegate (GameEvent e);

    //These take all of the Handlers as they get added and are used to pass the delegates to the handlers
    private readonly Dictionary<Type, EventDelegate> _delegates = new Dictionary<System.Type, EventDelegate>();
    private readonly Dictionary<Delegate, EventDelegate> _delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
    private readonly List<GameEvent> _queuedEvents = new List<GameEvent>();
    private readonly object _queueLock = new object();

    //Singleton stuff
    private static readonly EventManager _instance = new EventManager();
    public static EventManager Instance => _instance;

    private EventManager() {}

    //When a piece of code needs to read an event, it adds a handler here.
    public void AddHandler<T> (EventDelegate<T> del) where T : GameEvent
    {
        if (_delegateLookup.ContainsKey(del))
        {
            return;
        }

        EventDelegate internalDelegate = (e) => del((T)e);
        _delegateLookup[del] = internalDelegate;

        EventDelegate tempDel;
        if (_delegates.TryGetValue(typeof(T), out tempDel)) {
            _delegates[typeof(T)] = tempDel += internalDelegate;
        } else {
            _delegates[typeof(T)] = internalDelegate;
        }
    }

    //This removes the handlers when the piece of code no longer needs them
    //MAKE SURE TO DO THIS
    public void RemoveHandler<T>(EventDelegate<T> del) where T : GameEvent
    {
        EventDelegate internalDelegate;
        if (_delegateLookup.TryGetValue(del, out internalDelegate)) {
            EventDelegate tempDel;
            if (_delegates.TryGetValue(typeof(T), out tempDel)) {
                tempDel -= internalDelegate;
                if (tempDel == null) {
                    _delegates.Remove(typeof(T));
                } else {
                    _delegates[typeof(T)] = tempDel;
                }
            }
            _delegateLookup.Remove(del);
        }
    }

    //Clears the dictionaries when they are empty
    public void Clear()
    {
        lock (_queueLock)
        {
            if (_delegates != null) _delegates.Clear();
            if (_delegateLookup != null) _delegateLookup.Clear();
            if (_queuedEvents != null) _queuedEvents.Clear();
        }
    }

    //This launches the handler of the given delegate
    public void Fire(GameEvent e)
    {
        EventDelegate del;
        if (_delegates.TryGetValue(e.GetType(), out del))
        {
            del.Invoke(e);
        }
    }

    //This is if we have asynchronous events and will work like Fire
    public void ProcessQueuedEvents()
    {
        List<GameEvent> events;
        lock (_queueLock)
        {
            if (_queuedEvents.Count > 0)
            {
                events = new List<GameEvent>(_queuedEvents);
                _queuedEvents.Clear();
            }
            else
            {
                return;
            }
        }

        foreach(var e in events)
        {
            Fire(e);
        }
    }

    //Adds delegates to the queue as they come in
    //These delegates are then processed by ProcessQueuedEvents
    public void Queue(GameEvent e)
    {
        lock (_queueLock)
        {
            _queuedEvents.Add(e);
        }
    }
}


