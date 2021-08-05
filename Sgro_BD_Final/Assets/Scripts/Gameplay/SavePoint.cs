using System;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Action OnPlayerCollided;

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerCollided?.Invoke();
    }

}
