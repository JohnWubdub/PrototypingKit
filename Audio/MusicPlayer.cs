// MusicPlayer.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.PrototypingKit.Utilities
{
    /// <summary>
    ///     From http://answers.unity3d.com/questions/652919/music-player-get-songs-from-directory.html
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        public enum SeekDirection
        {
            Forward,
            Backward
        }

        public AudioSource source;
        public List<AudioClip> clips = new List<AudioClip>();

        [SerializeField] [HideInInspector] private int currentIndex;

        private FileInfo[] soundFiles;

        private List<string> validExtensions = new List<string> {".ogg", ".wav", ".mp3"};
                             // Don't forget the "." i.e. "ogg" won't work - cause Path.GetExtension(filePath) will return .ext, not just ext.

        private string absolutePath = "./"; // relative path to where the app is running - change this to "./music" in your case
        public string subPath;

        private void Start()
        {
            //being able to test in unity
            if (Application.isEditor) absolutePath = "Assets/" + subPath;

            if (source == null) source = gameObject.AddComponent<AudioSource>();

            ReloadSounds();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Previous"))
            {
                Seek(SeekDirection.Backward);
                PlayCurrent();
            }
            if (GUILayout.Button("Play current"))
            {
                PlayCurrent();
            }
            if (GUILayout.Button("Next"))
            {
                Seek(SeekDirection.Forward);
                PlayCurrent();
            }
            if (GUILayout.Button("Reload"))
            {
                ReloadSounds();
            }
        }

        private void Seek(SeekDirection d)
        {
            if (d == SeekDirection.Forward)
                currentIndex = (currentIndex + 1) % clips.Count;
            else
            {
                currentIndex--;
                if (currentIndex < 0) currentIndex = clips.Count - 1;
            }
        }

        private void PlayCurrent()
        {
            source.clip = clips[currentIndex];
            source.Play();
        }

        private void ReloadSounds()
        {
            //clips.Clear();

            //// get all valid files
            //var info = new DirectoryInfo(absolutePath);
            //soundFiles = info.GetFiles()
            //    .Where(f => IsValidFileType(f.Name))
            //        .ToArray();

            //// and load them
            //foreach (var s in soundFiles)
            //    StartCoroutine(LoadFile(s.FullName));
        }

        private bool IsValidFileType(string fileName)
        {
            return validExtensions.Contains(Path.GetExtension(fileName));
            // Alternatively, you could go fileName.SubString(fileName.LastIndexOf('.') + 1); that way you don't need the '.' when you add your extensions
        }

        private IEnumerator LoadFile(string path)
        {
            var www = new WWW("file://" + path);
            print("loading " + path);

            AudioClip clip = www.GetAudioClip(false);
            while (!clip.isReadyToPlay)
                yield return www;

            print("done loading");
            clip.name = Path.GetFileName(path);
            clips.Add(clip);
        }
    }
}