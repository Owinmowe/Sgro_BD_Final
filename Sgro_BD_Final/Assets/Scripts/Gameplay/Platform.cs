using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float minTimeBeforeFlicker = 5f;
    [SerializeField] float maxTimeBeforeFlicker = 10f;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(FlickerCoroutine());
    }

    IEnumerator FlickerCoroutine()
    {
        while (true)
        {
            float randomTime = Random.Range(minTimeBeforeFlicker, maxTimeBeforeFlicker);
            yield return new WaitForSeconds(randomTime);
            anim.SetTrigger("Flicker");
        }
    }
}
