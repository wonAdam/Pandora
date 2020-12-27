using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraChangeZone : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] CinemachineVirtualCamera toCinemachine;
    [SerializeField] CinemachineVirtualCamera fromCinemachine;
    private void OnTriggerEnter(Collider other)
    {
        if (Math.Pow(2, other.gameObject.layer) == playerLayer.value)
        {
            CinemachineVirtualCamera[] vcs = FindObjectsOfType<CinemachineVirtualCamera>();
            foreach(var c in vcs) c.Priority = 10;
            toCinemachine.Priority = 20;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Math.Pow(2, other.gameObject.layer) == playerLayer.value)
        {
            CinemachineVirtualCamera[] vcs = FindObjectsOfType<CinemachineVirtualCamera>();
            foreach(var c in vcs) c.Priority = 10;
            fromCinemachine.Priority = 20;
        }
    }
}
