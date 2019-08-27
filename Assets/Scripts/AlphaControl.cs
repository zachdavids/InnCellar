using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControl : MonoBehaviour
{
    private float _alphaFactor;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.shader = Shader.Find("AlphaModification");
    }

    // Update is called once per frame
    void Update()
    {
        _alphaFactor = Mathf.PingPong(Time.time * 0.1f, 1.0f) + 0.5f;
        _renderer.material.SetFloat("_AlphaFactor", _alphaFactor);
    }
}