using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public enum PlayerMoveDirection
    {
        LEFT, RIGHT
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float NIBEL_moveSpeed;
    [SerializeField] float aimingMoveSpeed;
    [SerializeField] float inTheAirFactor;
    [SerializeField] float acceleration;
    [SerializeField] float turnSpeed;
    [SerializeField] AudioClip walkSound;
    [HideInInspector] public PlayerMoveDirection Direction;
    private PlayerDasher playerDasher;
    private PlayerJumper playerJumper;
    private PlayerArcher playerArcher;
    private PlayerPusher playerPusher;
    private PlayerStance playerStance;
    private float baseMoveSpeed;
    private Rigidbody myRB;
    private Transform bodyTR;
    private Animator bodyAnim;
    private Quaternion leftQuaternion;
    private Quaternion rightQuaternion;

    private void Start()
    {
        Direction = PlayerMoveDirection.RIGHT;

        playerDasher = GetComponent<PlayerDasher>();
        playerJumper = GetComponent<PlayerJumper>();
        playerArcher = GetComponent<PlayerArcher>();
        playerPusher = GetComponent<PlayerPusher>();
        playerStance = GetComponent<PlayerStance>();
        myRB = GetComponent<Rigidbody>();
        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
        bodyTR = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0].transform; ;

        leftQuaternion = Quaternion.LookRotation(Vector3.left, Vector3.up);
        rightQuaternion = Quaternion.LookRotation(Vector3.right, Vector3.up);

        baseMoveSpeed = moveSpeed;
    }
    private void Update()
    {
        /*
        PlayerMover는 Direction과 Aiming Angle, Velocity에 따른 애니메이션과 회전을 담당합니다.
        */

        ChangeMovementSpeedBasedOnStance();

        // Shooting 중에는 Direction을 바꾸지않습니다.
        if (bodyAnim.GetBool("isShooting")) return;

        // Aim 중이면
        if (bodyAnim.GetBool("isAiming"))
        {
            // 카메라에서 마우스포인터, 캐릭터 두개의 Vector간의 각도를 구합니다.
            float angleForCharacterDirectionWhileAiming = CalculateCharacterDirection();
            // 그 각도로 키보드 Right/Left Key로 설정됐던 Direction을  Override합니다.
            OverrideCharacterDirectionBasedOn(angleForCharacterDirectionWhileAiming);

            // 캐릭터 상체의 각도를 설정합니다.
            // Aim때 설정된 각도는 Shooting중에는 재설정하지않습니다.
            if (!bodyAnim.GetBool("isShooting"))
            {
                // 마우스포인터의 위치에 따른 상체 각도를 받아옵니다. (Range : -90f, 90f)
                float angleForCharacterAimingAngle = playerArcher.CalculateCharacterAimingAngle(Direction);
                // Animation Aiming Angle을 설정합니다. (Range : -90f, 90f) -> (Range : 0f, 1f)
                bodyAnim.SetFloat("Angle", (angleForCharacterAimingAngle + 90f) / 180f);
            }

            // Set Animation Move Speed For Aiming
            bodyAnim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x) / aimingMoveSpeed);
        }
        // Aim중이 아니라면
        else
        {
            // Aim이 아니라면 뒷걸음은 없습니다.
            bodyAnim.SetBool("MovingBackward", false);

            // Set Animation Move Speed
            bodyAnim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x) / moveSpeed);
            
        }

        // 설정된 Direction에 따라 캐릭터를 회전합니다.
        RotateCharacterBasedOnDirection();
    }


    #region PUBLIC METHODS
    // Called by PlayerController
    public void MoveLeft()
    {
        if (bodyAnim.GetBool("isAiming"))
        {
            float xSpeed;
            if (playerJumper.isOnGround)
                xSpeed = myRB.velocity.x - acceleration * Time.deltaTime;
            else
                xSpeed = myRB.velocity.x - acceleration * Time.deltaTime / inTheAirFactor;

            float clampSpeed = Mathf.Clamp(xSpeed, -aimingMoveSpeed, aimingMoveSpeed);

            // move
            myRB.velocity = new Vector3(clampSpeed, myRB.velocity.y, myRB.velocity.z);

            ChangeDirection(PlayerMoveDirection.LEFT);
        }
        else
        {
            float xSpeed;
            if (playerJumper.isOnGround)
                xSpeed = myRB.velocity.x - acceleration * Time.deltaTime;
            else
                xSpeed = myRB.velocity.x - acceleration * Time.deltaTime / inTheAirFactor;

            float clampSpeed = Mathf.Clamp(xSpeed, -moveSpeed, moveSpeed);

            // move
            myRB.velocity = new Vector3(clampSpeed, myRB.velocity.y, myRB.velocity.z);

            ChangeDirection(PlayerMoveDirection.LEFT);
        }
    }


    public void ChangeMovementSpeedBasedOnStance()
    {
        if (playerStance.state == PlayerStance.PlayerStanceState.NIBEL)
        {
            moveSpeed = NIBEL_moveSpeed;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }


    // Called by PlayerController
    public void MoveRight()
    {
        if (bodyAnim.GetBool("isAiming"))
        {
            float xSpeed;
            if (playerJumper.isOnGround)
                xSpeed = myRB.velocity.x + acceleration * Time.deltaTime;
            else
                xSpeed = myRB.velocity.x + acceleration * Time.deltaTime / inTheAirFactor;

            float clampSpeed = Mathf.Clamp(xSpeed, -aimingMoveSpeed, aimingMoveSpeed);

            // move
            myRB.velocity = new Vector3(clampSpeed, myRB.velocity.y, myRB.velocity.z);

            ChangeDirection(PlayerMoveDirection.RIGHT);
        }
        else
        {
            float xSpeed;
            if (playerJumper.isOnGround)
                xSpeed = myRB.velocity.x + acceleration * Time.deltaTime;
            else
                xSpeed = myRB.velocity.x + acceleration * Time.deltaTime / inTheAirFactor;

            float clampSpeed = Mathf.Clamp(xSpeed, -moveSpeed, moveSpeed);


            // move
            myRB.velocity = new Vector3(clampSpeed, myRB.velocity.y, myRB.velocity.z);

            ChangeDirection(PlayerMoveDirection.RIGHT);
        }


    }

    public void PlayWalkingStepSoundOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(walkSound);
    }

    #endregion

    #region PRIVATE METHODS

    // 카메라와 마우스 포인터
    // 카메라와 캐릭터
    // 두개의 Vector간의 각도를 구합니다. (에임중일때 호출)
    private float CalculateCharacterDirection()
    {
        // Calculation For Character Direction 
        Ray rayOnlyX = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, 0f, 0f));
        Vector3 cameraToCharacter = transform.position - Camera.main.transform.position;

        //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + rayOnlyX.direction * 10f, Color.yellow); // Camera -> MousePoint
        //Debug.DrawLine(Camera.main.transform.position, transform.position, Color.blue); // Camera -> Character
        //Debug.Log("Direction Angle: " + angleForCharacterDirection); // Angle

        return Vector3.SignedAngle(rayOnlyX.direction, cameraToCharacter, Vector3.up);

    }



    private void OverrideCharacterDirectionBasedOn(float angleForCharacterDirectionWhileAiming)
    {
        if (angleForCharacterDirectionWhileAiming < 0)
        {
            if (Direction == PlayerMoveDirection.LEFT)
            {
                bodyAnim.SetBool("MovingBackward", true);
            }
            else
            {
                bodyAnim.SetBool("MovingBackward", false);
            }

            ChangeDirection(PlayerMoveDirection.RIGHT);
        }
        else
        {
            if (Direction == PlayerMoveDirection.RIGHT)
            {
                bodyAnim.SetBool("MovingBackward", true);
            }
            else
            {
                bodyAnim.SetBool("MovingBackward", false);
            }

            ChangeDirection(PlayerMoveDirection.LEFT);
        }
    }

    private void RotateCharacterBasedOnDirection()
    {
        if (Direction == PlayerMoveDirection.RIGHT)
        {
            bodyTR.rotation = Quaternion.Lerp(bodyTR.rotation, rightQuaternion, Time.deltaTime * turnSpeed);
        }
        else
        {
            bodyTR.rotation = Quaternion.Lerp(bodyTR.rotation, leftQuaternion, Time.deltaTime * turnSpeed);
        }
    }


    private void ChangeDirection(PlayerMoveDirection dir)
    {
        Direction = dir;
    }

    #endregion

}



