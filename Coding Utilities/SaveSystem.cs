using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

//A basic save system that serializes custom classes into a .bin file.
//There are many examples included that SHOULD DEFINITELY be replaced with what your game needs instead.
public class SaveSystem : MonoBehaviour
{

    //The overarching class that should contain everything that will be saved.
    [Serializable]
    public class SaveData
    {
        //EX: Will save the example PlayerData class implemented below.
        public PlayerData Player;
    }

    //EX: A class that contains specific values that I wish to save for the player.
    //Keep in mind, EVERYTHING to be saved must use [Serializable].
    [Serializable]
    public class PlayerData
    {
        public PlayerData()
        {
        }

        public SerializableVector2 Position;
        public int MaxHealth;
        public float CurrentHealth;
        public int MaxFocus;
        public float CurrentFocus;

    }

    //Used in place for normal Vector2, as Vector2 is not Serializable.
    [Serializable]
    public class SerializableVector2
    {
        public SerializableVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float x, y;
    }


    [Header("The name of the file you wish to save/load to. If a file of this name doesn't exist, it will create a new one.")]
    public string FileName;

    private bool loading;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
      //You can (and should) call save and load somewhere else. This is just a test/example.
        if (Input.GetKeyDown(KeyCode.Alpha9))
            SaveGame();
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            LoadGame();
    }



    //==================================
    // SAVING
    //==================================

    //Creates new SaveData, assigns all the values that you wish to save from the game state, and writes it to a file.
    public void SaveGame()
    {
        SaveData save = new SaveData();

        //EX using the PlayerData. Code commented out for your compiler's sanity.
        // PlayerData player = new PlayerData();
        //
        // player.Position = new SerializableVector2(Services.Player.transform.position.x, Services.Player.transform.position.y);
        // player.CurrentFocus = Services.Player.CurrentFocus;
        // player.MaxFocus = Services.Player.AttackCount;
        // player.MaxHealth = Services.Player.MaxHealth;
        // player.CurrentHealth = Services.Player.GetHealth();
        //
        // save.Player = player;

        WriteSaveData(FileName, save);
        print("Saved to " + FileName);
    }

    //Writes the SaveData to the specified file name. Will create a new file if one of that name doesn't exist.
    private void WriteSaveData(string fileName, SaveData saveData) {
        var formatter = new BinaryFormatter();
        var stream = new FileStream(fileName + ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }



    //==================================
    // LOADING
    //==================================

    //Loads the current save data on file.
    public void LoadGame() {
        if(!loading) {
          StartCoroutine(LoadGameC());
        }
    }

    //Actual function that reads save data from a file and assigns the values to their in-game counterparts.
    private AsyncOperation loadingLevel;
    private IEnumerator LoadGameC()
    {
        loading = true;
        SaveData save = ReadSaveData(FileName);

        // Reloads the current scene. Can be changed to load a certain scene specified in SaveData instead.
        loadingLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!loadingLevel.isDone)
            yield return 0;

        // EX: Loading the PlayerData recieved from the file and assigning it back into the game.
        // Services.Player.MaxHealth = save.Player.MaxHealth;
        // Services.Player.SetHealth(save.Player.CurrentHealth);
        //
        // Services.Player.AttackCount = save.Player.MaxFocus;
        // Services.Player.CurrentFocus = save.Player.CurrentFocus;
        //
        // Services.Player.transform.position = new Vector2(save.Player.Position.x, save.Player.Position.y);

        print("Loaded from " + FileName);
        loading = false;

    }

    // Reads the data at the specified file name and creates a new SaveData class with it.
    private SaveData ReadSaveData(string fileName)
    {
        var formatter = new BinaryFormatter();
        var stream = new FileStream(fileName + ".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        SaveData newSave = (SaveData) formatter.Deserialize(stream);
        stream.Close();

        return newSave;
    }
}
