using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : MonoBehaviour
{

    [SerializeField] Key Jump;
    [SerializeField] Key Right;
    [SerializeField] Key Left;
    [SerializeField] Key Up;
    [SerializeField] Key Down;
    [SerializeField] Key StanceRight;
    [SerializeField] Key StanceLeft;

    private PlayerJumper playerJumper;
    private PlayerMover playerMover;
    private PlayerStance playerStance;
    private PlayerArcher playerArcher;
    private PlayerPusher playerPusher;

    void Start()
    {
        playerJumper = GetComponent<PlayerJumper>();
        playerMover = GetComponent<PlayerMover>();
        playerStance = GetComponent<PlayerStance>();
        playerArcher = GetComponent<PlayerArcher>();
        playerPusher = GetComponent<PlayerPusher>();
    }

    void Update()
    {
        /*
        PlayerController는 
        입력을 받고 조건에 따라 Player스크립트들의 함수를 호출합니다.
        */

        // Jump
        if (Input.GetKeyDown(Jump.key))
            if (playerJumper.isOnGround)
                playerJumper.Jump();
            else if(playerJumper.isJumpable)
                playerJumper.UseJumpable();

        // Move
        if (Input.GetKey(Right.key))
        {
            playerPusher.isMoving = true;
            playerMover.MoveRight(); // 이동
        }
        else if (Input.GetKey(Left.key))
        {
            playerPusher.isMoving = true;
            playerMover.MoveLeft();
        }
        else
            playerPusher.isMoving = false;

        // Shoot Arrow
        if (Input.GetMouseButtonDown(0))
            if (playerArcher.IsEnable)
                playerArcher.Aim();

        if (Input.GetMouseButtonUp(0))
            if (playerArcher.IsEnable)
                playerArcher.Shoot();

        // Stance Switch
        if (Input.GetKeyDown(StanceRight.key))
            playerStance.ChangeStateRight();

        if (Input.GetKeyDown(StanceLeft.key))
            playerStance.ChangeStateLeft();

    }

    public void Disable()
    {
        this.enabled = false;
    }
}




