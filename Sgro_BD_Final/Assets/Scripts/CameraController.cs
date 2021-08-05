using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Player")]
    [SerializeField] GameObject player;
    [SerializeField] Vector3 distanceFromPlayer;

    [Header("Camera Shake")]
    [SerializeField] float shakeDuration = 2f;
    [SerializeField] float shakeStrenght = .2f;

    public static CameraController instance = null;

    Vector3 startingLocation;

    bool following = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        startingLocation = transform.position;
    }

    private void LateUpdate()
    {
        if (following)
        {
            transform.position = player.transform.position + distanceFromPlayer;
        }
    }

    public void StartFollowingPlayer()
    {
        following = true;
    }

    public void StopFollowingPlayer()
    {
        following = false;
        startingLocation = transform.position;
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake(shakeDuration, shakeStrenght));
    }

    public void StrongShakeCamera()
    {
        StartCoroutine(StrongShake(shakeDuration, shakeStrenght));
    }

    IEnumerator Shake(float duration, float strenght)
    {
        float currentShakeTime = 0;
        while (currentShakeTime < duration)
        {
            currentShakeTime += Time.deltaTime;
            float x = Random.Range(startingLocation.x - strenght / 2, startingLocation.x + strenght / 2);
            float y = Random.Range(startingLocation.y - strenght / 2, startingLocation.y + strenght / 2);
            transform.position = new Vector3(x, y, startingLocation.z);
            yield return null;
        }
        transform.position = startingLocation;
    }

    IEnumerator StrongShake(float duration, float strenght)
    {
        float currentShakeTime = 0;
        while (currentShakeTime < duration * 2)
        {
            currentShakeTime += Time.deltaTime;
            float x = Random.Range(startingLocation.x - strenght, startingLocation.x + strenght);
            float y = Random.Range(startingLocation.y - strenght, startingLocation.y + strenght);
            transform.position = new Vector3(x, y, startingLocation.z);
            yield return null;
        }
        transform.position = startingLocation;
    }
}