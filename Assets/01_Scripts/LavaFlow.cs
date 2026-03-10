using System;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    [SerializeField] private float flowSpeed;
    private Renderer _lavaRenderer;
    private Vector2 _offset; // to offset the texture coordinates

    void Start()
    {
        _lavaRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _offset.y += flowSpeed * Time.deltaTime;
        _lavaRenderer.material.SetTextureOffset("_BaseMap", _offset);
    }
    
}
