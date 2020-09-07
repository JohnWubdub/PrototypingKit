using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour // script by John Wanamaker
{
    [SerializeField] private string photoFolder;

    public int numOfPhotos = 0;

    public Camera camera;

    public List<GameObject> photoList = new List<GameObject>();
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private GameObject gallery;

    void Start()
    {
        camera = this.GetComponent<Camera>();
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TakePicture();
            numOfPhotos++;
        }
    }
    
    public void TakePicture()
    {
        Texture2D texture;

        if (!Directory.Exists(photoFolder))
        {
            Directory.CreateDirectory(photoFolder); //this photo is located with the other folders (Photography - Camera - Mechanics/PhotoFolder)
        }
        
        texture = CaptureTexture(photoFolder + "/" + numOfPhotos + ".png");

        AddPhoto(texture);
        
    }

    
    
    public Texture2D CaptureTexture(string _path)
    {
        // captures the camera image that is the width and height of the screen
        int sqr = 512;
        int width = Screen.width;
        int height = Screen.height;

        RenderTexture tempRT = new RenderTexture(width, height, 24);
        // the 24 can be 0,16,24, formats like
        // RenderTextureFormat.Default, ARGB32 etc.
        camera.targetTexture = tempRT;
        camera.Render();
        RenderTexture.active = tempRT;
        
        Texture2D photo = new Texture2D(width, height, TextureFormat.RGB24, false); // false, meaning no need for mipmaps
        photo.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        photo.Apply();

        RenderTexture.active = null; //can help avoid errors 
        camera.targetTexture = null;

        Destroy(tempRT);

        SaveTextureAsPNG(photo, _path); //saves the photo to the specified folder

        return photo;
    }
    
    void AddPhoto(Texture2D texture) //saving to the list of photos
    {
        GameObject temp = Instantiate(photoPrefab, gallery.transform);
        temp.GetComponent<RawImage>().texture = texture;
        photoList.Add(temp);
    } 
    
    public void SaveTextureAsPNG(Texture2D texture, string path)
    {
        byte[] bytes;
        bytes = texture.EncodeToPNG();
        
        File.WriteAllBytes(path, bytes);
    }
}
