using System.Collections;
using System.Collections.Generic;
//using EasyWiFi.Core;
using UnityEngine;


//This class is where we will put specific Events that we can call and listen for
public class Events
{
    #region Tutorial
    
    //When you make a new Event, it has to be a class that inherits from GameEvent
    public class ExampleEvent : GameEvent
    {
        //Some Events you can keep empty, but sometimes you will want to pass variables around with an event
        //In that case you will create a public Getter variable of whatever type you need
        public int GetVar { get; }

        //In both empty and full Events, you will need to create a public function with the same name as the Event
        //This is what you will call when calling the Event
        public ExampleEvent(int getVar)
        {
            //If the Event has a variable that needs to be passed, you will pass it through the function like this
            GetVar = getVar;
        }
    }

    /*
    public class FeedbackEvent : GameEvent
    {
        public bool GetBool { get; }

        public FeedbackEvent(bool triggered)
        {
            GetBool = triggered;
        }
        
    }
    */

    //In order to receive your Events, you will need to set up Handlers in each script you want to receive an Event from
    
    //In the Start() function of the script, you will type EventManager.Instance.AddHandler<ExampleEvent>(OnExampleEvent)
    
    //In the OnDestroy() function of the script, you MUST add EventManager.Instance.RemoveHandler<ExampleEvent>(OnExampleEvent)
    
    //YOU MUST REMOVE ALL HANDLERS IN ONDESTROY OR YOU WILL CAUSE MEMORY LEAKS
    
    //Once you have your Handler, you must create your OnExampleEvent function. Create it as public as you would any function.
    //This function must include a constructor that handles the event.
    //public void OnExampleEvent(ExampleEvent evt){}
    //Now you can fill in that function with whatever you want it to do
    //If you want to use any of the variables in the Event, use evt.GetVar (evt."variableName")
    
    //To Fire an Event, all you have to do is call EventManager.Instance.Fire(new ExampleEvent(VariableValue));
    //When this happens, all scripts that have a Handler for this Event will be informed of the Event
    
    #endregion

    public class MoveCompletedEvent : GameEvent
    {
        
    }

    public class NightTime : GameEvent
    {
        public bool GetBool { get; }

        public NightTime(bool triggered)
        {
            GetBool = triggered;
        }
    }

    /// <summary>
    /// Fires whenever a player shakes
    /// Contains a player ID so that only specific things will activate on shake
    /// </summary>
    public class ShakeCompletedEvent : GameEvent
    {
        /*
        public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }
        public Vector3 GetAccelerationVector3 { get; }

        public ShakeCompletedEvent(EasyWiFiConstants.PLAYER_NUMBER getPlayerId, Vector3 getAccelerationVector3)
        {
            GetPlayerID = getPlayerId;
            GetAccelerationVector3 = getAccelerationVector3;
        }
        */
    }

    /// <summary>
    /// Fires when a player is swiping on their phone screen
    /// Passes vector information so that something can be moved with this event over several frames
    /// </summary>
    public class SwipeEvent : GameEvent
    {
        /*
        public Vector3 GetActionVector { get; }
        public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }

        public SwipeEvent(Vector3 getActionVector, EasyWiFiConstants.PLAYER_NUMBER getPlayerId)
        {
            GetActionVector = getActionVector;
            GetPlayerID = getPlayerId;
        }
        */
    }

    /// <summary>
    /// Fires when a player taps on their screen
    /// A tap is when a player presses and then releases on the screen within a short period of time
    /// Passes a Vector3 that says where the tap was in case we need that
    /// </summary>
    public class TapEvent : GameEvent
    {
        /*
        public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }

        public TapEvent(EasyWiFiConstants.PLAYER_NUMBER getPlayerId)
        {
            GetPlayerID = getPlayerId;    
        }
        */
    }

    /// <summary>
    /// Fires when a player shakes their phone to the beat of the song
    /// Used to activate UI elements that celebrate marching correctly
    /// </summary>
    public class MetronomeShake : GameEvent
    {
        /*
        public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }

        public MetronomeShake(EasyWiFiConstants.PLAYER_NUMBER getPlayerId)
        {
            GetPlayerID = getPlayerId;
        }
        */
    }

    /// <summary>
    /// Fires when a player does something that should vibrate their phone
    /// Can vibrate one person's device or everyone's
    /// </summary>
    public class VibratePhone : GameEvent
    {
        /*
        public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }

        public VibratePhone(EasyWiFiConstants.PLAYER_NUMBER getPlayerId)
        {
            GetPlayerID = getPlayerId;
        }
        */
    }

    /// <summary>
    /// Called from scripts that should make a sound on the phone
    /// Can send data to individual phones
    /// </summary>
    public class PhoneSound : GameEvent
    {
        /*
        public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }
        
        public int GetSoundID { get; }

        public PhoneSound(EasyWiFiConstants.PLAYER_NUMBER getPlayerId, int getSoundId)
        {
            GetPlayerID = getPlayerId;
            GetSoundID = getSoundId;
        }
        */
    }

    /// <summary>
    /// Called whenever a metronome beat happens
    /// Used by OnBeatReactors to call their effects
    /// </summary>
    public class MetronomeBeatEvent : GameEvent
    {
        
    }

    public class CleanupMovesEvent : GameEvent
    {
        
    }

    /// <summary>
    /// Called when pepita stamp sections end to remove all the stamps
    /// </summary>
    public class CleanupStampsEvent : GameEvent
    {
        
    }

    public class LightAccelerationEvent : GameEvent
    {
        //public EasyWiFiConstants.PLAYER_NUMBER GetPlayerID { get; }
        public Vector3 GetAccelerationVector3 { get; }

        /*
        public LightAccelerationEvent(EasyWiFiConstants.PLAYER_NUMBER getPlayerId, Vector3 getAccelerationVector3)
        {
            GetPlayerID = getPlayerId;
            GetAccelerationVector3 = getAccelerationVector3;
        }
        */
    }
}
