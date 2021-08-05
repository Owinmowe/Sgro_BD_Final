using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [Header("Setup")]
    [SerializeField] Renderer rend;
    [SerializeField] Animator anim;

    [Header("General")]
    [SerializeField] int score = 10;
    [SerializeField] float flickerFrequency = 2f;
    [SerializeField] ParticleSystem goodParticles = null;
    [SerializeField] ParticleSystem badParticles = null;
    [SerializeField] float timeForDestroy = 5f;

    public Action<bool, int> OnCollision;

    bool redCoin = false;

    Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        StartCoroutine(FlickerCoroutine());
    }

    IEnumerator FlickerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flickerFrequency);
            anim.SetTrigger("Change State");
            redCoin = !redCoin;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        if (other.gameObject.GetComponent<Rock>())
        {
            OnCollision?.Invoke(redCoin, score);
            if (redCoin)
            {
                badParticles.Play();
            }
            else
            {
                goodParticles.Play();
            }
        }
        Destroy(gameObject, timeForDestroy);
        rend.enabled = false;
        col.enabled = false;
    }
}
