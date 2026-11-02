using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [SerializeField] private float timeToWait = 5;
    private float waitTime = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Explosion());
    }

    private void doExplosion()
    {
        

        Debug.Log("Exploded!!!");
        Destroy(gameObject);
    }

    IEnumerator Explosion()
    {
        while (waitTime < timeToWait)
        {
            waitTime += Time.deltaTime;
            Debug.Log(waitTime.ToString());
            yield return null;
        }

        doExplosion();
    }
}
