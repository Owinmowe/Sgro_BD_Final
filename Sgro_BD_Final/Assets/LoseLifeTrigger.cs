using System;
using UnityEngine;

public class LoseLifeTrigger : MonoBehaviour
{

    public Action OnTriggerActivated;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerActivated?.Invoke();
    }
}
