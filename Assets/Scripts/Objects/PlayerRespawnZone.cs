using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnZone : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Events playerRespawned_E;
    [SerializeField] Transform templeSpawnPos;
    [SerializeField] Transform forestSpawnPos;


    // Event Listener Function - Called By <PlayerDaed> Event
    public void Respawn(Transform pos)
    {
        // Destroy Dead Player
        Debug.Log("Respawn");
        GameObject deadPlayer = FindObjectOfType<PlayerHealth>().gameObject;
        Destroy(deadPlayer);

        // Spawn New Player
        Instantiate(player, pos.position, Quaternion.LookRotation(Vector3.right, Vector3.up));
        playerRespawned_E.OnOccur();
    }
}
