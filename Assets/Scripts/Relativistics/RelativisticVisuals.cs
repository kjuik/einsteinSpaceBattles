using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RelativisticVisuals : MonoBehaviour
{
    struct HistoricalState
    {
        public float Timestamp;
        public Vector3 Position;
        public Quaternion Rotation;
    }
    
    [SerializeField] List<Transform> Perceivers;
    [SerializeField] GameObject Visuals;

    private readonly List<GameObject> AfterImages = new List<GameObject>();
    private readonly List<HistoricalState> History = new List<HistoricalState>();
    
    void Awake()
    {
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
        History.Add(new HistoricalState() {
            Timestamp = Time.time, 
            Position = Visuals.transform.position,
            Rotation = Visuals.transform.rotation
        });
        
        for (var i = 0; i < Perceivers.Count; i++)
        {
            var distance = Vector3.Distance(Visuals.transform.position, Perceivers[i].position);
            var lightDelay = distance / Relativistics.C;
            var perceivedTimestamp = Time.time - lightDelay;

            for (var j = History.Count-1; j >= 0; j--)
            {
                if (History[j].Timestamp < perceivedTimestamp)
                {
                    ToggleRenderers(AfterImages[i], true);
                    AfterImages[i].transform.position = History[j].Position;
                    AfterImages[i].transform.rotation = History[j].Rotation;
                    break;
                }
                else if (j == 0)
                {
                    ToggleRenderers(AfterImages[i], false);
                }
            }
        }
    }

    private void ToggleRenderers(GameObject afterImage, bool on)
    {
        afterImage.GetComponent<Renderer>().enabled = on;
        foreach (var renderer in afterImage.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }
        
        foreach (var particles in 
            afterImage.GetComponentsInChildren<ParticleSystem>()
            .Concat(new List<ParticleSystem> { afterImage.GetComponent<ParticleSystem>() }))
        {
            if (particles == null)
                continue;
            
            if (particles.isPlaying && !on)
                particles.Pause();
            
            if (!particles.isPlaying && on)
                particles.Play();            
        }
    }

    public GameObject GetAfterImageByLayerIndex(int layerIndex)
    {
        return AfterImages[layerIndex];
    }
    
    void OnDestroy()
    {
        AfterImages.ForEach(Destroy);
    }
}
