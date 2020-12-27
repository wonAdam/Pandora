using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{

    [SerializeField] private float jumpVel;
    [SerializeField] private float NIBEL_jumpVel;
    [SerializeField] private float jumpableVel;
    [SerializeField] private float NIBEL_jumpableVel;
    [SerializeField] private float groundDetectLength;
    [SerializeField] private float higherAdditiveGravity;
    [SerializeField] private float NIBEL_higherAdditiveGravity;

    [SerializeField] private float lowerAdditiveGravity;
    [SerializeField] private float NIBEL_lowerAdditiveGravity;
    [SerializeField] private float NIBEL_maxDescendingVel;

    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Vector3 feetColliderScale;
    [SerializeField] private Key JumpKey;


    [HideInInspector] public bool isOnGround = false;
    [HideInInspector] public bool isJumpable = false;
    private PlayerArcher playerArcher;
    private PlayerStance playerStance;
    private Animator bodyAnim;
    private Rigidbody myRB;
    private float baseJumpVel;
    private float baseJumpableVel;
    private float baseHigherAdditiveGravity;
    private float baseLowerAdditiveGravity;




    private void Start()
    {
        playerArcher = GetComponent<PlayerArcher>();
        playerStance = GetComponent<PlayerStance>();
        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
        myRB = GetComponent<Rigidbody>();

        baseJumpVel = jumpVel;
        baseJumpableVel = jumpableVel;
        baseHigherAdditiveGravity = higherAdditiveGravity;
        baseLowerAdditiveGravity = lowerAdditiveGravity;
    }


    private void Update()
    {


        // Check if Grounded
        CheckNSetIfIsOnGround_OverlapBox();

        // Check Player Stance
        ChangeJumpPowerNGravityBasedOnStance();

        if (!isOnGround)
        {
            // For Good Jump Feeling
            // Gravity Addition
            if (!Input.GetKey(JumpKey.key))
            {
                GetGravityHarder(higherAdditiveGravity);
            }
            else
            {
                GetGravityHarder(lowerAdditiveGravity);
                if (playerStance.state == PlayerStance.PlayerStanceState.NIBEL)
                {
                    myRB.velocity = new Vector3(
                        myRB.velocity.x,
                        Mathf.Clamp(myRB.velocity.y, -NIBEL_maxDescendingVel, Mathf.Infinity),
                        myRB.velocity.z);

                    if (Mathf.Abs(myRB.velocity.y - (-NIBEL_maxDescendingVel)) < Mathf.Epsilon)
                    {
                        bodyAnim.SetBool("isHoldingUmbrella", true);
                        return;
                    }
                }
            }

            // InTheAir Animation On
            bodyAnim.SetBool("isOnGround", false);

        }
        else
        {
            // InTheAir Animation Off
            bodyAnim.SetBool("isOnGround", true);
        }
        bodyAnim.SetBool("isHoldingUmbrella", false);

        
    }

    #region PUBLIC METHDOS
    // Normal Jump
    public void Jump()
    {
        myRB.velocity = new Vector3(myRB.velocity.x, myRB.velocity.y + jumpVel, myRB.velocity.z);
    }

    // 2nd Jump
    public void UseJumpable()
    {
        if (bodyAnim.GetBool("isAiming"))
        {
            bodyAnim.SetBool("isAiming", false);
        }

        isJumpable = false;
        myRB.velocity = new Vector3(myRB.velocity.x, jumpableVel, myRB.velocity.z);

        bodyAnim.SetBool("isHoldingUmbrella", false);
        bodyAnim.SetTrigger("SecondJump");
    }

    public void ChangeJumpPowerNGravityBasedOnStance()
    {
        if (playerStance.state == PlayerStance.PlayerStanceState.NIBEL)
        {
            jumpVel = NIBEL_jumpVel;
            jumpableVel = NIBEL_jumpableVel;
            higherAdditiveGravity = NIBEL_higherAdditiveGravity;
            lowerAdditiveGravity = NIBEL_lowerAdditiveGravity;
        }
        else
        {
            jumpVel = baseJumpVel;
            jumpableVel = baseJumpableVel;
            higherAdditiveGravity = baseHigherAdditiveGravity;
            lowerAdditiveGravity = baseLowerAdditiveGravity;

        }
    }
    #endregion

    #region PRIVATE METHDOS
    private void GetGravityHarder(float amount)
    {
        //Debug.Log("GetGravityHarder" + amount);
        myRB.AddForce(new Vector3(0f, -amount * Time.deltaTime, 0f), ForceMode.Impulse);
    }

    private void CheckNSetIfIsOnGround_Raycast()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundDetectLength, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDetectLength, floorMask))
        {
            isOnGround = true;
            isJumpable = false;
        }
        else
        {
            isOnGround = false;
        }
    }

    private void CheckNSetIfIsOnGround_OverlapBox()
    {
        //Debug.DrawLine(transform.position + new Vector3(0f, groundDetectLength, 0f), transform.position + new Vector3(0f, 1f, 0f), Color.red);
        if (Physics.OverlapBox(transform.position - new Vector3(0f, groundDetectLength, 0f), feetColliderScale / 2f, Quaternion.identity, (int)floorMask).Length > 0)
        {
            isOnGround = true;
            isJumpable = false;
        }
        else
        {
            isOnGround = false;
        }

    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position - new Vector3(0f, groundDetectLength, 0f), feetColliderScale);
    }

    #endregion

}




