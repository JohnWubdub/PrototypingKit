using UnityEngine;
using System.Collections;

public static class FreqToNote {

   public static float Note2Freq(Note _note)
   {
      float freq;
      int noteIndex;
      int noteNameIndex = System.Array.IndexOf(MusicReference.Notes, _note.NoteName);

      if (noteNameIndex < 3) {
         noteNameIndex += 12;
      }
      noteIndex = noteNameIndex + ((_note.Octave - 1) * 12) + 1;

      freq = MusicReference.Freqs[noteIndex];
      return freq;
   }

   public static float Note2Freq(int _noteNum)
   {
      float freq = MusicReference.Freqs[_noteNum];
      return freq;
   }
}

public class Note { 
   string _noteName;
   int _octave;

   public Note(string noteName, int octave)
   {
      int pos = System.Array.IndexOf(MusicReference.Notes, noteName);
      if (pos > -1)
      {
         _noteName = noteName;
         _octave = octave;
      }
      else 
      {
         Debug.LogError("Invalid Note Name: " + noteName + ". Ensure note is member of MusicReference.Notes.");
      }
   }

   public string NoteName
   {
      get { return _noteName; }
   }

   public int Octave
   {
      get { return _octave; }
   }
}
public static class MusicReference
{
   public static string[] Notes = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

   public static float[] Freqs = {   16.35f, 17.32f, 18.35f, 19.45f, 20.60f, 21.83f, 23.12f, 24.50f, 25.96f, 27.50f, 29.14f, 30.87f,
                  32.70f, 34.65f, 36.71f, 38.89f, 41.20f, 43.65f, 46.25f, 49.00f, 51.91f, 55.00f, 58.27f, 61.74f,
                  65.41f, 69.30f, 73.42f, 77.78f, 82.41f, 87.31f, 92.50f, 98.00f, 103.8f, 110.0f, 116.5f, 123.5f,
                  130.8f, 138.6f, 146.8f, 155.6f, 164.8f, 174.6f, 185.0f, 196.0f, 207.7f, 220.0f, 233.1f, 246.9f,
                  261.6f, 277.2f, 293.7f, 311.1f, 329.6f, 349.2f, 370.0f, 392.0f, 415.3f, 440.0f, 466.2f, 493.9f,
                  523.3f, 554.4f, 587.3f, 622.3f, 659.3f, 698.5f, 740.0f, 784.0f, 830.6f, 880.0f, 932.3f, 987.8f,
                  1047f, 1109f, 1175f, 1245f, 1319f, 1397f, 1480f, 1568f, 1661f, 1760f, 1865f, 1976f,
                  2093f, 2217f, 2349f, 2489f, 2637f, 2794f, 2960f, 3136f, 3322f, 3520f, 3729f, 3951f,
                  4186f, 4435f, 4699f, 4978f, 5274f, 5588f, 5920f, 6272f, 6645f, 7040f, 7459f, 7902f};
}