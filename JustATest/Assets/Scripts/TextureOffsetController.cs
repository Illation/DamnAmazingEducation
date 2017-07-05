using UnityEngine;
[RequireComponent(typeof(Renderer))]

public class TextureOffsetController : MonoBehaviour {

    public float xOffsetSpeed = 0.0f, yOffsetSpeed = 0.0f;
    public bool active = false;

    private Renderer _rend = null;
    private Vector2 _offset = Vector2.zero;

    void Start() {
        _rend = GetComponent<Renderer>();
    }
       
	void Update () {
        if (!active) return;
        _offset += new Vector2(xOffsetSpeed, yOffsetSpeed) * Time.deltaTime;
        _rend.material.SetTextureOffset("_MainTex", _offset);
	}

    public void ResetOffset() {
        _rend.material.SetTextureOffset("_MainTex", -_offset);
        _offset = Vector2.zero;
    }
}
