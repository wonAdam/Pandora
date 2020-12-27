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
    private PlayerDasher playerDasher;
    private PlayerStance playerStance;
    private PlayerArcher playerArcher;
    private PlayerPusher playerPusher;

    void Start()
    {
        playerJumper = GetComponent<PlayerJumper>();
        playerMover = GetComponent<PlayerMover>();
        playerDasher = GetComponent<PlayerDasher>();
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
        {
            JumpAfterConditionCheck();
        }

        //// Right Dash
        //if (Input.GetKeyDown(Right.key))
        //{
        //    RightDashAfterConditionCheck();
        //}

        //// Left Dash
        //if (Input.GetKeyDown(Left.key))
        //{
        //    LeftDashAfterConditionCheck();
        //}

        // Move
        if (Input.GetKey(Right.key))
        {
            playerPusher.isMoving = true;
            RightMoveAfterConditionCheck();
        }
        else if (Input.GetKey(Left.key))
        {
            playerPusher.isMoving = true;
            LeftMoveAfterConditionCheck();
        }
        else
        {
            playerPusher.isMoving = false;
        }

        // Shoot Arrow
        if (Input.GetMouseButtonDown(0))
        {
            AimArrowAfterConditionCheck();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ShootAfterConditionCheck();
        }

        // Stance Switch
        if (Input.GetKeyDown(StanceRight.key))
        {
            ChangeStateRightAfterConditionCheck();
        }
        if (Input.GetKeyDown(StanceLeft.key))
        {
            ChangeStateLeftAfterConditionCheck();
        }

    }


    private void JumpAfterConditionCheck()
    {
        if (playerJumper.isOnGround)
        {
            playerJumper.Jump();
        }
        else if (playerJumper.isJumpable)
        {
            playerJumper.UseJumpable();
        }
    }

    //private void RightDashAfterConditionCheck()
    //{
    //    // Right Dashable이라면
    //    if (playerDasher.dashableState == PlayerDasher.DashableState.RIGHT)
    //    {
    //        playerDasher.Dash(); // 대쉬 Exec
    //    }

    //    else
    //    {
    //        playerDasher.SetDashable(PlayerDasher.DashableState.RIGHT); // 대쉬 Set
    //    }
    //}

    //private void LeftDashAfterConditionCheck()
    //{
    //    // Left Dashable이라면
    //    if (playerDasher.dashableState == PlayerDasher.DashableState.LEFT)
    //    {
    //        playerDasher.Dash(); // 대쉬 Exec 
    //    }

    //    else
    //    {
    //        playerDasher.SetDashable(PlayerDasher.DashableState.LEFT); // 대쉬 Set
    //    }
    //}

    private void LeftMoveAfterConditionCheck()
    {
        if (!playerDasher)
        {
            playerMover.MoveLeft(); // 이동
            return;
        }
        if (!playerDasher.isDashing)
        {
            playerMover.MoveLeft(); // 이동
        }
    }

    private void RightMoveAfterConditionCheck()
    {
        if(!playerDasher)
        {
            playerMover.MoveRight(); // 이동
            return;
        }
        if (!playerDasher.isDashing)
        {
            playerMover.MoveRight(); // 이동
        }
    }

    private void AimArrowAfterConditionCheck()
    {
        if (playerArcher.IsEnable)
        {
            playerArcher.Aim();
        }
    }

    private void ShootAfterConditionCheck()
    {
        if (playerArcher.IsEnable)
        {
            playerArcher.Shoot();
        }
    }

    private void ChangeStateLeftAfterConditionCheck()
    {
        playerStance.ChangeStateLeft();
    }

    private void ChangeStateRightAfterConditionCheck()
    {
        playerStance.ChangeStateRight();
    }

    public void Disable()
    {
        this.enabled = false;
    }
}




