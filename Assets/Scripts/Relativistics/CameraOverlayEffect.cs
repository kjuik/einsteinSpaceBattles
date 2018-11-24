using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverlayEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Transform perceiver;
    
    private Camera cam;
    private float radius = 0f;
    
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        radius += Time.deltaTime * Relativistics.C;
    }
    
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var center = cam.WorldToScreenPoint(perceiver.position);
        var screenRadius =
            (center - cam.WorldToScreenPoint(perceiver.position + Vector3.right * radius)).magnitude;
        
        var invertedYCenter = new Vector3(center.x, Screen.height - center.y, center.z);
        
        material.SetVector("_Center", invertedYCenter);
        material.SetFloat("_Radius", screenRadius);
        
        Graphics.Blit(src, dest, material);
    }
}
