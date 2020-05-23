
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AutoAudio : AssetPostprocessor
{
   public static List<AudioClip> assetsToProcess = new List<AudioClip>();
   public static bool runOnAllAssets;
   public static bool newAssetsToProcess = false;
   public static AudioImporterSampleSettings mySettings;
   public static AudioImporterSampleSettings defaultSettings;

   void OnEnable()
   {
      runOnAllAssets = EditorPrefs.GetBool("autoAudioRunOnAllAssetsKey");
      AudioImporter importer = (AudioImporter)assetImporter;
      importer.preloadAudioData = true;
      defaultSettings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;
      defaultSettings.loadType = AudioClipLoadType.DecompressOnLoad;
      defaultSettings.compressionFormat = AudioCompressionFormat.PCM;
      importer.defaultSampleSettings = defaultSettings;
   }

   public static void OnPostprocessAllAssets(
    string[] importedAssets,
    string[] deletedAssets,
    string[] movedAssets,
    string[] movedFromAssetPaths)
   {
      if (!runOnAllAssets) return;
      foreach (string path in importedAssets) //loading any audio clips that were imported & adding those to a list
      {
         AudioClip importedClip = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)) as AudioClip; //loading it so it'll be ready for the editor update to initiate analysis
         assetsToProcess.Add(importedClip);
         newAssetsToProcess = true;
      }
   }
}

public class AutoAudioPreferences {
   private static bool prefsLoaded = false;
   public static bool runonAllAssets = false;

   [PreferenceItem ("AutoAudio")]
   private static void CustomPreferencesGUI() {
      if (!prefsLoaded)
      {
         runonAllAssets = EditorPrefs.GetBool("autoAudioRunOnAllAssetsKey", false);
         prefsLoaded = true;
      }

      EditorGUILayout.LabelField("Version 0.3");
      runonAllAssets = EditorGUILayout.Toggle("Automatically Run on Import:", runonAllAssets);

      if (GUI.changed)
      {
         EditorPrefs.SetBool("autoAudioRunOnAllAssetsKey", runonAllAssets);
      }
   }

}

[InitializeOnLoad]
class AudioAnalyzer
{
   [MenuItem("Assets/AutoAudio Check")]
   public static void testClips()
   {
      Object[] activeGO = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);
      foreach (object o in activeGO)
      {
         testAudioClip((AudioClip)o);
      }
   }

   [MenuItem("Assets/AutoAudio Check", true)]
   public static bool checkForAudioClips()
   {
      Object[] activeGO = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);
      return (activeGO.Length >= 1) ? true : false;
   }

   static AudioAnalyzer()
   {
      EditorApplication.update += Update; //subscribing to the editor's update callback so this runs every editor update. Wish I didn't have to, but it's the only way...
   }

   private static List<AudioClip> toBeRemoved = new List<AudioClip>(); //list of clips we're going to remove after processing

   static void Update()
   {
      if (AutoAudio.newAssetsToProcess == false) return; //no new assets
      if (AutoAudio.assetsToProcess.Count == 0) return;
      toBeRemoved.Clear();
      foreach (AudioClip importedClip in AutoAudio.assetsToProcess)
      {
         try
         {
            if (importedClip.loadState == AudioDataLoadState.Loaded)
            {
               testAudioClip(importedClip); //calls function that tests the audio clips
               toBeRemoved.Add(importedClip); //after testing, add it to the list to remove
            }
         }
         catch (System.Exception e)
         {
            Debug.Log("Auto Audio: Error in checking Audioclips. Ensure " + importedClip.name + " isn't opened in another program. " + e.Message);
            toBeRemoved.Add(importedClip);
            break;
         }
      }
      foreach (AudioClip clip in toBeRemoved) //Removing clips from the list in a separate for loop to avoid errors
      {
         try
         {
            AutoAudio.assetsToProcess.Remove(clip);
         }
         catch (System.Exception e)
         {
            Debug.Log("Auto Audio: Error removing audioclips: " + e.Message);
         }
      }
      if (AutoAudio.assetsToProcess.Count == 0) AutoAudio.newAssetsToProcess = false; //Are we done?
   }



   static void testAudioClip(AudioClip importedClip)
   {
      float[] samples; //array to keep our samples in
      bool silenceAtStart = true; //guilty until proven innocent!
      int countOfSilence = 0;
      bool silenceChecked = false;
      int countOfClips = 0;
      samples = new float[importedClip.samples * importedClip.channels]; //each channel multiplies the number of samples in our array
      if (importedClip.loadType == AudioClipLoadType.CompressedInMemory)
      {
         Debug.Log("Clips with Load Type Compressed in Memory cannot be checked by AutoAudio. Change " + importedClip.name + "'s load type to Decompress on Load.");
         return;
      }
      importedClip.GetData(samples, 0);

      for (int i = 0; i < importedClip.channels; i++)
      {
         if (Mathf.Abs(samples[i]) > 0.01f) //testing to see if each channels first sample is effectively 0
         {
            Debug.LogWarning("AutoAudio detected that " + importedClip.name + " doesn't start with a 0 audio crossing. First sample is " + samples[i] + ". Consider re-exporting clip.");
         }
      }         

      for (int i = 0; i < samples.Length - 1; i++) //looping through the samples
      {
         float absSample = Mathf.Abs(samples[i]);
         if (!silenceChecked)
         {
            silenceChecked = true;
            int j = 0;
            do
            {
               if (Mathf.Abs(samples[i + j]) < .05) //if we continue to detect samples quieter than .05, we'll warn the user
                  countOfSilence++;
               else
                  silenceAtStart = false;
               j++;
            }
            while (j < ((importedClip.frequency * importedClip.channels) / 20) && silenceAtStart); // continue searching through the first .1 seconds, as long as we don't see signal > .1

            if (silenceAtStart && countOfSilence > 100) Debug.LogWarning("AutoAudio detected that " + importedClip.name + " might start with silence. Consider removing silence at clip start.");
         }
         if (absSample == Mathf.Abs(samples[i + 1]) && absSample > .98f) // Two identical samples in a row greater than .98f are likely clips
         {
            countOfClips++;
         }
      }
      if (countOfClips != 0) Debug.LogWarning("AutoAudio detected " + countOfClips + " clips in " + importedClip.name + ". Consider re-exporting file without clipping.");
   }
}