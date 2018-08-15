using System;
using System.Collections;
using UnityEngine;


namespace TextureLoader
{

    /// <summary>
    /// Common Texture loader
    /// </summary>
    [AddComponentMenu("Common/TextureLoader")]
    [RequireComponent(typeof(Renderer))]
    public class TextureLoader : MonoBehaviour
    {
        [SerializeField]
        string url;

        [SerializeField]
        bool autoStart = true;

        [SerializeField]
        bool aspectFit = true;

        [SerializeField]
        bool useCustomMaterial = false;


        Material _material;
        Renderer _renderer;

        public event Action<TextureLoader> OnFinish;

        #region life cycle
        void Awake()
        {
            this.isLoaed = false;
        }

        void Start()
        {
            _renderer = GetComponent<Renderer>();
            if (useCustomMaterial)
            {
                _material = _renderer.material;
            }
            else
            {
                // mat = new Material(_DEFAULT_SHADER);
                _material = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
                _renderer.material = _material;
            }

            if (autoStart)
            {
                Load(url);
            }

            _renderer.enabled = false;
        }

        void OnDrawGizmos()
        {

        }
        #endregion

        #region public 

        public virtual void Load(string url)
        {
            StartCoroutine(_load(url));
        }
        #endregion

        #region privates

        IEnumerator _load(string url)
        {
            using (WWW www = new WWW(url))
            {
                yield return www;

                if (string.IsNullOrEmpty(www.error))
                {
                    SetTexture(www.texture);
                    this.hasError = false;
                    this.isLoaed = true;
                }
                else
                {
                    this.error = www.error;
                    this.hasError = true;
                    this.isLoaed = true;

                    Debug.LogWarning("error:" + www.error);
                }
            }
            yield return null;

            if (OnFinish != null)
            {
                OnFinish(this);
            }
        }

        void SetTexture(Texture2D texture)
        {
            _material.mainTexture = texture;

            if (aspectFit)
            {
                Vector3 size = transform.localScale;
                float aspect = (float)texture.width / (float)texture.height;
                if (aspect < 1)
                {
                    size.x *= aspect;
                }
                else
                {
                    size.y /= aspect;
                }
                transform.localScale = size;
            }

            _renderer.enabled = true;
        }
        #endregion

        #region getter setter
        public string URL
        {
            get { return url; }
        }
        public bool isLoaed
        {
            get;
            protected set;
        }

        public string error
        {
            get;
            protected set;
        }

        public bool hasError
        {
            get;
            protected set;
        }

        #endregion
    }
}