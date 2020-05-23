// PaintBrush.cs
// Last edited 11:08 AM 06/04/2015 by Aaron Freedman

using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    private void Start() {}

    private void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) return;

        var rend = hit.transform.GetComponent<Renderer>();
        var meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null) return;

        var tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        tex.SetPixel((int) pixelUV.x, (int) pixelUV.y, Color.black);
        tex.Apply();
    }
}