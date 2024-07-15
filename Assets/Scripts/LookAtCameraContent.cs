using UnityEngine;

public class LookAtCameraContent : MonoBehaviour
{
    void Update()
    {
        Vector3 lookAtPosition = Camera.main.transform.position - transform.position;
        lookAtPosition.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookAtPosition);
        transform.rotation = rotation;
    }
}
