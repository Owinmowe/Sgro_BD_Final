using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime);
    }
}
