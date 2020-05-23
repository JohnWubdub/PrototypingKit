// CopyTextureArea.cs
// Last edited 10:01 AM 06/22/2015 by Aaron Freedman

using UnityEngine;

public class CopyTextureArea : MonoBehaviour
{
    public bool bilinear;
    public int size;
    public bool createCubes;
    private GameObject[,] cubes;
    public Transform cubeParent;
    public Material mat;
    public bool clamp;

    private void Start()
    {
        cubes = new GameObject[size, size];
        if (createCubes)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    GameObject o = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    o.transform.parent = cubeParent;
                    o.transform.localPosition = new Vector3(i, j, 1);
                    o.GetComponent<Renderer>().material = mat;
                    cubes[i, j] = o;
                }
            }
        }
    }

    private void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) return;

        var rend = hit.transform.GetComponent<Renderer>();
        var meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null) return;

        var tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;

        if (bilinear)
        {
            GetComponent<Renderer>().material.color = tex.GetPixelBilinear(pixelUV.x, pixelUV.y);
            //Note: don't do biliear filtering on a nearest-neighbor filtered texture or the color you grab won't match the color you're pointing to
        }
        else
        {
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            //GetComponent<Renderer>().material.color = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
            Color[] c = tex.GetPixels((int) pixelUV.x, (int) pixelUV.y, size, size); // TODO: check if out of bounds
            //if (clamp)
            //{
            //    for (int i = 0; i < c.Length; i++)
            //    {
            //        c[i] = new Color(Mathf.CeilToInt(c[i].r), Mathf.CeilToInt(c[i].g), Mathf.CeilToInt(c[i].b));
            //    }
                    
            //}
            var t = new Texture2D(size, size, TextureFormat.ARGB32, false);
            t.filterMode = FilterMode.Point;
            t.SetPixels(c);
            GetComponent<Renderer>().material.mainTexture = t;
            if (createCubes)
            {
                for (var i = 0; i < size; i++)
                {
                    var column = 0;
                    while (column < size)
                    {
                        Color color = c[i * size + column];
                        float lum = (color.r * 0.21f) + (color.g * .72f) + (color.b * 0.07f);
                        color.a = lum;
                        cubes[column, i].GetComponent<Renderer>().material.color = color;
                        column++;
                    }
                }
            }
        }
    }
}