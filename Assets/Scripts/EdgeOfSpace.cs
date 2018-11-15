using System.Collections.Generic;
using UnityEngine;

public class EdgeOfSpace : MonoBehaviour
{
    [SerializeField]
    EdgeOfSpace otherEdge;
    [SerializeField]
    bool preserveX;
    [SerializeField]
    bool preserveZ;

    readonly List<Transform> dontSendBack = new List<Transform>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!dontSendBack.Contains(other.transform))
            otherEdge.Send(other.transform);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (dontSendBack.Contains(other.transform))
            dontSendBack.Remove(other.transform);
    }
    
    public void Send(Transform sentTransform)
    {
        dontSendBack.Add(sentTransform);
        
        sentTransform.transform.position = 
            new Vector3(
                preserveX ? sentTransform.transform.position.x : transform.position.x,
                sentTransform.transform.position.y, 
                preserveZ ? sentTransform.transform.position.z : transform.position.z);
    }
}
