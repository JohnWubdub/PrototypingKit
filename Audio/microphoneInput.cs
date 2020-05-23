using UnityEngine;
using UnityEngine.Audio;

public class microphoneInput : MonoBehaviour
{
   public float sensitivity = 100;
   public float loudness = 0;
   private float total;

   void Start()
   {
      total = 0f;
      GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, 44100);
      GetComponent<AudioSource>().loop = true;
      GetComponent<AudioSource>().mute = false;
      while (!(Microphone.GetPosition(null) > 0)) { }
      GetComponent<AudioSource>().Play();
   }

   void Update()
   {
      loudness = GetAveragedVolume() * sensitivity;
      Debug.Log(loudness);
   }

   float GetAveragedVolume()
   {
      float[] data = new float[256];
      float a = 0;
      GetComponent<AudioSource>().GetOutputData(data, 0);
      foreach (float s in data)
      {
         a += Mathf.Abs(s);
      }
      return a / 256;
   }
}