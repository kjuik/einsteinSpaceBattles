using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RelativisticParticleBurst : MonoBehaviour
{   
    [SerializeField] List<Transform> Perceivers;
    [SerializeField] ParticleSystem Visuals;

    private readonly List<ParticleSystem> AfterImages = new List<ParticleSystem>();

    private float startTimestamp;
    
    void Awake()
    {
        startTimestamp = Time.time;
        
        if (Perceivers.Count == 0)
        {
            Perceivers.Add(FindObjectOfType<PlayerController>().transform);
            Perceivers.Add(FindObjectOfType<EnemyController>().transform);
        }
        
        for (var i = 0; i < Perceivers.Count; i++)
        {
            var newAfterImage = Instantiate(
                Visuals,
                Visuals.transform.position, 
                Visuals.transform.rotation
            );
            SetLayer(
                newAfterImage.transform, 
                LayerMask.NameToLayer(Relativistics.PerceiverLayerNames[i])
            );
            
            AfterImages.Add(newAfterImage);
        }
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

        for (var i = 0; i < Perceivers.Count; i++)
        {
            var distance = Vector3.Distance(Visuals.transform.position, Perceivers[i].position);
            var lightDelay = distance / Relativistics.C;
            var perceivedTimestamp = Time.time - lightDelay;

            try
            {
                AfterImages[i].Simulate(Mathf.Clamp(perceivedTimestamp - startTimestamp, 0f, 10f), true, true);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }

    void OnDestroy()
    {
        AfterImages.ForEach(Destroy);
    }
}
