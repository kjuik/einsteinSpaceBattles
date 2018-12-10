using System.Collections.Generic;
using UnityEngine;

public class RelativisticVisuals : MonoBehaviour
{
    struct HistoricalState
    {
        public float Timestamp;
        public Vector3 Position;
        public Quaternion Rotation;
    }
    
    [SerializeField] Transform Perceiver;
    [SerializeField] GameObject Visuals;

    private GameObject AfterImage;
    private readonly List<HistoricalState> History = new List<HistoricalState>();

    private bool isDestroyed;
    private float destructionTimestamp;
    
    void Awake()
    {
        if (Perceiver == null)
            Perceiver = FindObjectOfType<PlayerController>().transform;

        CreateAfterImage();
    }

    private void CreateAfterImage()
    {
        AfterImage = Instantiate(
            Visuals,
            Visuals.transform.position,
            Visuals.transform.rotation
        );

        SetLayer(
            AfterImage.transform,
            LayerMask.NameToLayer(Relativistics.PlayerPerceptionLayerName)
        );

        AfterImage.SetActive(false);
    }

    private void SetLayer(Transform o, int layer)
    {
        o.gameObject.layer = layer;
        foreach (Transform child in o.transform)
            SetLayer(child, layer);
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

        var perceivedState = GetStatePerceivedFrom(Perceiver.position);

        if (perceivedState.HasValue)
        {
            AfterImage.SetActive(true);
            AfterImage.transform.position = perceivedState.Value.Position;
            AfterImage.transform.rotation = perceivedState.Value.Rotation;
        }
        else
            AfterImage.SetActive(false);
    }
    
    private HistoricalState? GetStatePerceivedFrom(Vector3 perceiverPosition)
    {
        if (isDestroyed && destructionTimestamp < GetPerceivedTimestamp(transform.position, perceiverPosition))
            return null;
        else
        {
            for (var j = History.Count - 1; j >= 0; j--)
            {
                if (History[j].Timestamp <= GetPerceivedTimestamp(History[j].Position, perceiverPosition))
                    return History[j];
            }
        }
        return null;
    }

    public Vector3? GetPositionPerceivedFrom(Vector3 perceiverPosition) => 
        GetStatePerceivedFrom(perceiverPosition)?.Position;

    private float GetPerceivedTimestamp(Vector3 transformPosition, Vector3 perceiverPosition)
    {
        var distance = Vector3.Distance(transformPosition, perceiverPosition);
        var lightDelay = distance / Relativistics.C;
        return Time.time - lightDelay;
    }

    public void DestroyRelativistically()
    {
        isDestroyed = true;
        destructionTimestamp = Time.time;
        Visuals.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Destroy(AfterImage);
    }
}
