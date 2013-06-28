using UnityEngine;
using System.Collections;

[AddComponentMenu("Common/TextureLoader (Streaming Assets)")]
public class TextureStreamingLoader : TextureLoader {
	
	public override void Load (string url)
	{
		string path;
		
		switch(Application.platform) {
			case RuntimePlatform.IPhonePlayer:
				path = "file://"+Application.dataPath + "/Raw/";
				break;
			case RuntimePlatform.Android:
				path = "jar:file://" + Application.dataPath + "!/assets/";
				break;
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.WindowsPlayer:
				path = "file://"+Application.dataPath + "/StreamingAssets/";
				break;
			default:
				path = Application.dataPath + "/StreamingAssets/";
				Debug.LogWarning("UNKNOWN Platform : " + Application.platform.ToString());
				break;
		}
		
		base.Load (path + url);
	}
}
