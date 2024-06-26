using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] bool useAI;

    public bool isShooting = false;

    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        Fire();   
    }

    void Fire()
    {
        if (isShooting && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuous());
        }

        else if (!isShooting && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuous()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();

            if (rb2d != null && !useAI)
            {
                rb2d.velocity = transform.up * projectileSpeed;
            }
            else if (rb2d != null && useAI)
            {
                rb2d.velocity = -transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
