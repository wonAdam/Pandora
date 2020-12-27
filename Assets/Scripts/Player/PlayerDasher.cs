using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerDasher : MonoBehaviour
{
    public enum DashableState
    {
        LEFT, RIGHT, NONE
    }
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDrag;
    [SerializeField] float dashableDuration; // 방향키 더블클릭 감도
    [SerializeField] float dragDuration;
    [SerializeField] float dashCoolTime;

    public DashableState dashableState;
    public bool isDashing = false;
    public bool isDashCoolDown = false;
    private Rigidbody myRB;
    private float secForDashCoolTime = 0f;
    public float secForDashable = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        dashableState = DashableState.NONE;
    }
    private void Update()
    {
        // 대쉬를 할 수 있는 키누름 간격을 셉니다.
        if (dashableState != DashableState.NONE)
            TikTokForDashableDuration();

        // 대쉬를 할 수 있는 쿨타임을 셉니다.
        if (isDashCoolDown)
            TikTokForDashCoolDown();


    }


    #region PUBLIC METHODS
    // Called by PlayerController and Self
    public void SetDashable(DashableState state)
    {
        secForDashable = 0f;
        dashableState = state;
    }

    public void Dash()
    {
        if (isDashCoolDown) return;

        // dash
        if (dashableState == DashableState.LEFT)
            myRB.velocity = new Vector3(-dashSpeed, 0f, 0f);
        else if (dashableState == DashableState.RIGHT)
            myRB.velocity = new Vector3(dashSpeed, 0f, 0f);
        else return;

        // set drag
        StartCoroutine(DashResistor());

        // reset dashable
        SetDashable(DashableState.NONE);

        // CoolTime Start
        isDashCoolDown = true;
    }
    #endregion

    #region PRIVATE METHODS
    // Dash 의 Brake역할을 합니다.
    private IEnumerator DashResistor()
    {
        isDashing = true;
        myRB.drag = dashDrag;
        yield return new WaitForSeconds(dragDuration);
        myRB.drag = 0f;
        isDashing = false;
    }

    private void TikTokForDashCoolDown()
    {
        secForDashCoolTime += Time.deltaTime;

        if (dashCoolTime <= secForDashCoolTime)
        {
            secForDashCoolTime = 0f;
            isDashCoolDown = false;
        }
    }

    private void TikTokForDashableDuration()
    {
        secForDashable += Time.deltaTime;

        if (dashableDuration <= secForDashable)
        {
            SetDashable(DashableState.NONE);
        }
    }
    #endregion

}




