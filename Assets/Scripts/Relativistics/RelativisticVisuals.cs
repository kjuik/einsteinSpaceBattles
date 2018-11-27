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

    private bool isDestroyed;
    private float destructionTimestamp;
    
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
            newAfterImage.SetActive(false);
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
        if (!isDestroyed)
        {
            History.Add(new HistoricalState()
            {
                Timestamp = Time.time,
                Position = Visuals.transform.position,
                Rotation = Visuals.transform.rotation
            });
        }

        for (var i = 0; i < Perceivers.Count; i++)
        {
            if (isDestroyed && destructionTimestamp < GetPerceivedTimestamp(Visuals.transform.position, i))
            {
                AfterImages[i].SetActive(false);
            }
            else
            {
                for (var j = History.Count - 1; j >= 0; j--)
                {
                    if (History[j].Timestamp < GetPerceivedTimestamp(History[j].Position, i))
                    {
                        AfterImages[i].gameObject.SetActive(true);
                        AfterImages[i].transform.position = History[j].Position;
                        AfterImages[i].transform.rotation = History[j].Rotation;
                        break;
                    }
                    else if (j == 0)
                    {
                        AfterImages[i].SetActive(false);
                    }
                }
            }
        }
    }

    private float GetPerceivedTimestamp(Vector3 transformPosition, int perceiverIndex)
    {
        var distance = Vector3.Distance(transformPosition, Perceivers[perceiverIndex].position);
        var lightDelay = distance / Relativistics.C;
        return Time.time - lightDelay;
    }

    public void DestroyRelativistically()
    {
        isDestroyed = true;
        destructionTimestamp = Time.time;
        Visuals.gameObject.SetActive(false);
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
