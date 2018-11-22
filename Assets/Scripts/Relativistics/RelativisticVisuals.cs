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
            newAfterImage.gameObject.layer = LayerMask.NameToLayer(Relativistics.PerceiverLayerNames[i]);
            
            AfterImages.Add(newAfterImage);
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

            for (var j = History.Count-1; j > 0; j--)
            {
                if (History[j].Timestamp < perceivedTimestamp || j == 0)
                {
                    AfterImages[i].transform.position = History[j].Position;
                    AfterImages[i].transform.rotation = History[j].Rotation;
                    break;
                }
            }
        }
    }
    
    void OnDestroy()
    {
        AfterImages.ForEach(Destroy);
    }
}
