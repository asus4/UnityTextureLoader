using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Common Texture loader
/// </summary>
[AddComponentMenu("Common/TextureLoader")]
[RequireComponent(typeof(Renderer))]
public class TextureLoader : MonoBehaviour {
	// Transparent/Diffuse
//	const string _DEFAULT_SHADER = "Mobile/Particles/Alpha Blended";
	const string _DEFAULT_SHADER = "Shader \"SimpleAlphaTexture\" {Properties { _MainTex (\"Texture\", 2D) = \"white\" { }}Category { Tags { \"Queue\"=\"Transparent\" \"IgnoreProjector\"=\"True\" \"RenderType\"=\"Transparent\" } Blend SrcAlpha OneMinusSrcAlpha Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) } BindChannels { Bind \"Color\", color Bind \"Vertex\", vertex Bind \"TexCoord\", texcoord } SubShader { Pass { SetTexture [_MainTex] { combine texture * primary } } }}}";
	
	[SerializeField]
	string url;
	
	[SerializeField]
	bool autoStart = true;
	
	[SerializeField]
	bool aspectFit = true;
	
	[SerializeField]
	bool useCustomMaterial = false;
	
	Material mat;
	
	public event Action<TextureLoader> OnFinish;

#region life cycle
	void Awake() {
		this.isLoaed = false;
	}
	
	void Start () {
		if(useCustomMaterial) {
			mat = renderer.material;
		}
		else {
			mat = new Material(_DEFAULT_SHADER);
			renderer.material = mat;
		}
		
		if(autoStart) {
			Load(url);
		}
		
		renderer.enabled = false;
	}
	
	void OnDrawGizmos() {
		
	}
#endregion

#region public 
	
	public virtual void Load(string url) {
		 StartCoroutine(_load(url));
	}
#endregion
	
#region privates
	
	IEnumerator _load(string url) {
		using(WWW www = new WWW(url)) {
			yield return www;
			
			if(string.IsNullOrEmpty(www.error)) {
				SetTexture(www.texture);
				this.hasError = false;
				this.isLoaed = true;
			}
			else {
				this.error = www.error;	
				this.hasError = true;
				this.isLoaed = true;
				
				Debug.LogWarning ("error:"+www.error);
			}
		}
		yield return null;
		
		if(OnFinish != null) {
			OnFinish(this);
		}
	}
	
	void SetTexture(Texture2D texture) {
		mat.mainTexture = texture;
		
		if(aspectFit) {
			Vector3 size = transform.localScale;
			float aspect = (float) texture.width / (float) texture.height;
			if(aspect < 1) {
				size.x *= aspect;
			}
			else {
				size.y /= aspect;
			}
			transform.localScale = size;
		}
		
		renderer.enabled = true;
	}
#endregion
	
#region getter setter
	public string URL {
		get {return url;}
	}
	public bool isLoaed {
		get;
		protected set;
	}
	
	public string error {
		get;
		protected set;
	}
	
	public bool hasError {
		get;
		protected set;
	}
	
#endregion
}
