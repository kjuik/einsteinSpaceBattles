using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RelativisticParticleBurst : MonoBehaviour
{   
    [SerializeField] Transform Perceiver;
    [SerializeField] ParticleSystem Visuals;

    private ParticleSystem AfterImage;

    private float startTimestamp;
    
    void Awake()
    {
        startTimestamp = Time.time;
        
        if (Perceiver == null)
            Perceiver = FindObjectOfType<PlayerController>().transform;

        AfterImage = Instantiate(
            Visuals,
            Visuals.transform.position, 
            Visuals.transform.rotation
        );
        SetLayer(
            AfterImage.transform, 
            LayerMask.NameToLayer(Relativistics.PlayerPerceptionLayerName)
        );            
    }

    private void SetLayer(Transform o, int layer)
    {
        o.gameObject.layer = layer;
        foreach (Transform child in o.transform)
        {
            SetLayer(child, layer);
        }
    }

    void Update()
    {
        try
        {
            Visuals.Simulate(Mathf.Clamp(Time.time - startTimestamp, 0f, 10f), true, true);
        }
        catch (Exception e)
        {
            // ignored
        }

        var distance = Vector3.Distance(Visuals.transform.position, Perceiver.position);
        var lightDelay = distance / Relativistics.C;
        var perceivedTimestamp = Time.time - lightDelay;

        try
        {
            AfterImage.Simulate(Mathf.Clamp(perceivedTimestamp - startTimestamp, 0f, 10f), true, true);
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    void OnDestroy()
    {
        Destroy(AfterImage);
    }
}
