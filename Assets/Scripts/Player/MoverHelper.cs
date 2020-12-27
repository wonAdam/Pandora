using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHelper : MonoBehaviour
{
    private PlayerMover playerMover;
    private PlayerJumper playerJumper;
    private Rigidbody playerRB;


    void Start()
    {
        playerMover = GetComponentInParent<PlayerMover>();
        playerJumper = GetComponentInParent<PlayerJumper>();
        playerRB = GetComponentInParent<Rigidbody>();
    }

    public void PlayWalkingStepSoundOnce_AnimationEventHelper()
    {
        if (playerJumper.isOnGround && Mathf.Abs(playerRB.velocity.x) > 0.01f)
            playerMover.PlayWalkingStepSoundOnce();
    }
}
