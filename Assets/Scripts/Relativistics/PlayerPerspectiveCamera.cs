using UnityEngine;

public class PlayerPerspectiveCamera : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Camera>().cullingMask = ~ LayerMask.GetMask(
            "CurrentLayer", "EnemyPerceptionLayer"
        );
    }
}
