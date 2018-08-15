using UnityEngine;
using UnityEditor;

namespace TextureLoader
{
    [CustomEditor(typeof(TextureLoader))]
    [CanEditMultipleObjects]
    public class TextureLoaderEditor : Editor
    {
        TextureLoader _target;

        void OnEnable()
        {
            _target = target as TextureLoader;
        }

        new void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        protected virtual void OnSceneGUI()
        {
            Vector3 pos = _target.transform.position;
            Handles.Label(pos, "[TEXTURE]" + _target.URL);
        }

        [MenuItem("Tools/Create/TextureLoader (from url)")]
        public static GameObject Create()
        {
            return Create<TextureLoader>();
        }

        protected static GameObject Create<T>() where T : Component
        {
            GameObject go = CreateTool.CreatePlane();
            go.AddComponent<T>();
            go.name = typeof(T).ToString();
            return go;
        }
    }
}