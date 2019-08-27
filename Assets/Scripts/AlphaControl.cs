using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControl : MonoBehaviour
{
    public float alphaFactor;

    // Start is called before the first frame update
    void Start()
    {
        alphaFactor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        alphaFactor = Mathf.PingPong(Time.time * 0.1f, 1.0f) + 0.5f;
        Shader.SetGlobalFloat("_AlphaFactor", alphaFactor);
    }
}
