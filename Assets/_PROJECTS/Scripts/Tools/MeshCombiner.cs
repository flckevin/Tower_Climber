using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Sirenix.OdinInspector;

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.


public class MeshCombiner : OdinEditorWindow
{
    [MenuItem("KevinTools/MeshCombiner")]
    public static void OpenWindow() 
    {
        GetWindow<MeshCombiner>().Show();
    }
    public MeshFilter[] meshFiltTargets;
    public bool unit32Format_SupportMoreVertex;


    [Button(ButtonSizes.Large)]
    void Bake()
    {
        CombineInstance[] combine = new CombineInstance[meshFiltTargets.Length];

        int i = 0;
        while (i < meshFiltTargets.Length)
        {
            combine[i].mesh = meshFiltTargets[i].sharedMesh;
            combine[i].transform = meshFiltTargets[i].transform.localToWorldMatrix;
            //meshFiltTargets[i].gameObject.SetActive(false);

            i++;
        }

        Mesh mesh = new Mesh();
        if (unit32Format_SupportMoreVertex == true) { mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; }
        
        MeshFilter storage = new GameObject("MeshStorage").AddComponent<MeshFilter>();
        storage.gameObject.AddComponent<MeshRenderer>();
        mesh.CombineMeshes(combine);
        storage.sharedMesh = mesh;
    }
}