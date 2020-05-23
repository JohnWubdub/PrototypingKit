using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Stores static references to many commonly used scripts and systems in a game.
/// </summary>
public static class Services
{
	//Yes, this is empty, but it is hella useful and I don't see many people using a Services manager.
	//This class is where you put references to scripts that you want to be able to easily access from anywhere in your game.
	//It gets rid of using Singletons (which are gross imo), and you can easily add and keep track of what scripts are "global"

	//Examples:
	public static GameManager Game;
	public static PlayerController Player;
	public static AudioManager Audio;

	//In each of these scripts, they would assign themselves to one of these variables in Awake() or another method.
	//Ex (This would be in Awake() in AudioManager):
	Services.Audio = this;

	//Then to use something from AudioManager in some random script, you just do:
	Services.Audio.PlaySound(someSoundIdk);
	//Also AudioManager isn't a built-in thing this is just an example ty

	//Thats all this script is filled with pretty much. You can add other functions if you really need too I guess.
	//This became a long write-up for a very simple concept, thanks for coming to my TED Talk
}
