using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.VersionControl;

public class SkinnedMeshBaker : OdinEditorWindow
{

    [MenuItem("KevinTools/SkinnedMeshBaker")]
    private static void OpenWindow()
    {
        GetWindow<SkinnedMeshBaker>().Show();
    }

    public SkinnedMeshRenderer _targetSkinnedMesh; // skinned mesh target to bake to mesh

    [Button(ButtonSizes.Large)]
    //function to bake skinned mesh
    public void Bake() 
    {
        //create new mesh
        Mesh _bakedSkinnedMesh = new Mesh();
        //bake it and store it in mesh created
        _targetSkinnedMesh.BakeMesh(_bakedSkinnedMesh);

        //check if the specific folder exist
        if (Directory.Exists("Assets/_PROJECTS/Prefabs/BakedMesh")) 
        {
            //create path
            string localPath = "Assets/_PROJECTS/Prefabs/BakedMesh/"+ _targetSkinnedMesh.gameObject.name + "_mesh.asset";
            // Make sure the file name is unique, in case an existing Prefab has the same name.
            //localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            //create mesh assets
            AssetDatabase.CreateAsset(_bakedSkinnedMesh, localPath);
            //save the mesh so we can use it later on
            AssetDatabase.SaveAssets();
        }

        //create new obejct with targeted name
        GameObject _meshStorage_G = new GameObject($"{_targetSkinnedMesh.name}_Mesh");
        //add mesh filter component to baked skinned mesh object and set mesh to be baked mesh
        _meshStorage_G.AddComponent<MeshFilter>().mesh = _bakedSkinnedMesh;
        //add meshrender component to baked skinned mesh object
        MeshRenderer _meshStorageRenderer = _meshStorage_G.AddComponent<MeshRenderer>();
        //get material from skinned mesh and apply it
        _meshStorageRenderer.sharedMaterial = _targetSkinnedMesh.material;

    }
}
