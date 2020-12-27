using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHelper : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }
    public void PlayerDead_AnimationEventHelper()
    {
        Debug.Log("PlayerDead_AnimationEventHelper");

        playerHealth.PlayerDead();
    }
}
