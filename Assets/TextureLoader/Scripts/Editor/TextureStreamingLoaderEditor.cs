using UnityEngine;
using UnityEditor;

namespace TextureLoader
{
    [CustomEditor(typeof(TextureStreamingLoader))]
    [CanEditMultipleObjects]
    public class TextureStreamingLoaderEditor : TextureLoaderEditor
    {
        // simple overrides

        [MenuItem("Tools/Create/TextureLoader (from SteamingAssets)")]
        public static new GameObject Create()
        {
            return Create<TextureStreamingLoader>();
        }
    }
}