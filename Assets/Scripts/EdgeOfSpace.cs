using System.Collections.Generic;
using UnityEngine;

public class EdgeOfSpace : MonoBehaviour
{
    [SerializeField]
    EdgeOfSpace otherEdge;
    [SerializeField]
    bool workInX;
    [SerializeField]
    bool workInZ;
    
    private void OnTriggerStay(Collider subject)
    {
        subject.transform.position = new Vector3(
            workInX && Mathf.Abs(subject.transform.position.x) > Mathf.Abs(transform.position.x) 
                ? otherEdge.transform.position.x 
                : subject.transform.position.x,
            subject.transform.position.y,
            workInZ && Mathf.Abs(subject.transform.position.z) > Mathf.Abs(transform.position.z) 
                ? otherEdge.transform.position.z 
                : subject.transform.position.z);
    }
}
