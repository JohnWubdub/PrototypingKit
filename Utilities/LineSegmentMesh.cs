// LineSegmentMesh.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit.Utilities
{
    /// <summary>
    ///     NOTE: This class doesn't actually work
    /// </summary>
    [RequireComponent(typeof (MeshFilter))]
    [RequireComponent(typeof (MeshRenderer))]
    public class LineSegmentMesh : MonoBehaviour
    {
        // Was gonna get fancy and do some custom mesh stuff but nevermind.

        private void Start()
        {
            var mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

//        int num = 4;

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];

            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(0, -1, 0);
            vertices[2] = new Vector3(1, 0, 0);
            vertices[3] = new Vector3(1, -1, 0);

            int[] triangles = {0, 3, 1, 0, 2, 3};

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        private void Update()
        {
//        Mesh mesh = GetComponent<MeshFilter>().mesh;
//        Vector3[] vertices = mesh.vertices;
//        Vector3[] normals = mesh.normals;
//        int i = 0;
//        while (i < vertices.Length) {
//            vertices[i] += normals[i] * Mathf.Sin(Time.time);
//            i++;
//        }
//        mesh.vertices = vertices;
        }
    }
}