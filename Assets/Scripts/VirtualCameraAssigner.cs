using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraAssigner : MonoBehaviour
{
    private CinemachineVirtualCamera myVC;
    private void Start()
    {
        myVC = GetComponent<CinemachineVirtualCamera>();
    }

    // Event Listener Function - Called By <PlayerSpawned> Event
    public void ReassignTarget()
    {
        myVC.Follow = FindObjectOfType<PlayerController>().transform;
        myVC.LookAt = FindObjectOfType<PlayerController>().transform;
    }
}
