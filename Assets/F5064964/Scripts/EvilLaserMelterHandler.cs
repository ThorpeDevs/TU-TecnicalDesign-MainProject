using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EvilLaserMelterHandler : MonoBehaviour
{
    [SerializeField] private float timeToWait = 5;
    private float _waitTime = 0;

    private void DoBeam()
    {
        Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out RaycastHit hit);
        
        Destroy(gameObject);
    }

    private void Start()
    {
        DoBeam();
    }
}
