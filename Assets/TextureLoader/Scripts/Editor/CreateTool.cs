using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;



/// <summary>
/// Editor Utility tool.
/// author koki ibukuro
/// </summary>
public class CreateTool : ScriptableObject
{

    static GameObject _CreateChild(string name)
    {
        GameObject go = new GameObject(name);
        Transform t = go.transform;
        t.parent = Selection.activeTransform;
        t.localScale = Vector3.one;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;

        Transform parent = Selection.activeTransform;
        if (parent != null)
        {
            go.layer = parent.gameObject.layer;
        }
        Selection.activeGameObject = go;

        return go;
    }

    /// <summary>
    /// Create selected object's child. 
    /// SHORTCUT KEY [cmd shift N]
    /// </summary>
    /// <returns>
    /// created gameobject
    /// </returns>
    [MenuItem("Tools/Create/Blank Child %#n")]
    public static GameObject CreateChild()
    {
        return _CreateChild("GameObject");
    }

    /// <summary>
    /// Creates 4 poly plane.
    /// </summary>
    /// <returns>
    /// The plane gameobject
    /// </returns>
    [MenuItem("Tools/Create/Plane")]
    public static GameObject CreatePlane()
    {
        GameObject go = _CreateChild("Plane");
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.shadowCastingMode = ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        Mesh mesh = new Mesh();
        go.AddComponent<MeshFilter>().sharedMesh = mesh;

        // make 4 vertices plane
        Vector3 offset = new Vector3(-0.5f, -0.5f, 0); // centering offset		
        Vector3[] vertices = new Vector3[4] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0) };

        /*
			
		 1-2
		 |/|
		 0-3
		 
		 */
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += offset;
        }

        mesh.vertices = vertices;
        mesh.uv = new Vector2[4] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        mesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };

        return go;
    }

    /// <summary>
    /// Delete all player preferences.
    /// </summary>
    [MenuItem("Tools/Delete/Delete PlayerPref")]
    public static void DeletePlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("deleted");
    }


}
