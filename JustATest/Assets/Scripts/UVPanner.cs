using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVPanner : MonoBehaviour {

    public int MaterialIndex = 0;
    public Vector2 UvAnimationRate = new Vector2(1.0f, 0.0f);
    public string TextureName = "_MainTex";
    private Renderer _renderer;
    private Vector2 _uvOffset = Vector2.zero;
    void Start()
    {
        _renderer = this.GetComponent<Renderer>();
    }
    void LateUpdate()
    {
        _uvOffset += (UvAnimationRate * Time.deltaTime);
        if (_renderer.enabled)
        {
            _renderer.materials[MaterialIndex].SetTextureOffset(TextureName, _uvOffset);
        }
    }
}
