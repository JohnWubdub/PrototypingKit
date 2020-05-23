// CustomTexTest.cs
// Last edited 9:17 PM 06/02/2015 by Aaron Freedman

using UnityEngine;

public class CustomTexTest : MonoBehaviour
{

    public enum Size
    {
        ThirtyTwo = 32,
        SixtyFour = 64,
        OneTwentyEight = 128,
        TwoFiftySix = 256,
    };

    public Size size;

    private Texture2D brush;
    public int subdivisions;
    private Color[] colors;
    public bool autoRandomize;

    private void Awake()
    {

    }

    void Start()
    {
        brush = new Texture2D((int)size, (int)size);
        brush.anisoLevel = 8;
        brush.filterMode = FilterMode.Point;
        colors = brush.GetPixels();
        //ChangeTheColor(Color.red);
        Rainbow();
        GetComponent<Renderer>().material.mainTexture = brush;
    }

    private void ChangeTheColor(Color c)
    {
        for (int i = 1; i <= subdivisions; i++)
        {
            for (var x = 0; x < i * (int)size / subdivisions; x++)
            {
                for (var y = 0; y < i * (int)size / subdivisions; y++)
                {
                    brush.SetPixel(x, y, c);
                }
            }
        }

        brush.Apply();
    }

    private void SetMip()
    {
        Renderer rend = GetComponent<Renderer>();

        // duplicate the original texture and assign to the material
        var texture = Instantiate(rend.material.mainTexture) as Texture2D;
        rend.material.mainTexture = texture;

        // colors used to tint the first 3 mip levels
        Color[] colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        int mipCount = Mathf.Min(3, texture.mipmapCount);

        // tint each mip level
        for (var mip = 0; mip < mipCount; ++mip)
        {
            var cols = texture.GetPixels(mip);
            for (var i = 0; i < cols.Length; ++i)
            {
                cols[i] = Color.Lerp(cols[i], colors[mip], 0.33f);
            }
            texture.SetPixels(cols, mip);
        }
        // actually apply all SetPixels, don't recalculate mip levels
        texture.Apply(false);
    }

    private void Rainbow()
    {
        for (var x = 0; x < (int)size; x++)
        {
            for (var y = 0; y < (int)size; y++)
            {
                brush.SetPixel(x, y, new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
            }
        }
        brush.Apply();
    }

    private void RainbowBetter()
    {
        Color c = new Color(0, 0, 0);
        for (int i = 0; i < colors.Length; i++)
        {
            c.r = Random.Range(0, 1f);
            c.g = Random.Range(0, 1f);
            c.b = Random.Range(0, 1f);
            colors[i] = c;
        }
        brush.SetPixels(colors);
        brush.Apply();
    }

    public void Update()
    {
        if (autoRandomize) RainbowBetter();
        if (Input.GetKeyDown(KeyCode.F))
        {
            RainbowBetter();
        }
    }

    private void OnGUI()
    {
        //GUILayout.Label(brush);
    }
}