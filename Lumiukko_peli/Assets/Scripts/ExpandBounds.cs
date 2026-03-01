using UnityEngine;

[ExecuteAlways]
public class ExpandBounds : MonoBehaviour
{
    public float boundsScale = 2f;

    void Update()
    {
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
            return;

        meshFilter.sharedMesh.bounds = new Bounds(
            Vector3.zero,
            Vector3.one * boundsScale
        );
    }
}