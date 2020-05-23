// GetTexelColor.cs
// Last edited 11:18 AM 06/04/2015 by Aaron Freedman

using UnityEngine;

public class GetTexelColor : MonoBehaviour
{
    public Color texelColor;
    public bool bilinear;

    private void Start()
    {
        
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
            GetComponent<Renderer>().material.color = tex.GetPixel((int) pixelUV.x, (int) pixelUV.y);
        }
    }
}