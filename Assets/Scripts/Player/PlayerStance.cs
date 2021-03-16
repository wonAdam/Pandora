using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStance : MonoBehaviour
{
    public enum PlayerStanceState
    {
        MIA, RIGEN, NIBEL, PANDORA
    }
    public PlayerStanceState state;
    public PlayerStanceState nextReservedState; // 전환중에 또 전환할 경우를 위해

    [SerializeField] Material hairMat;
    [SerializeField] Material clothMat;
    [SerializeField] Material accessoriesMat;
    [SerializeField] Color[] hairColor;
    [SerializeField] Color[] clothColor;
    [SerializeField] Color[] accessoriesColor;
    [SerializeField] Events playerStanceChanged_Event;
    [SerializeField] public Transform stanceSphere; // StateMachineBehaviour가 접근함

    private PlayerArcher playerArcher;
    private PlayerUmbrella playerUmbrella;
    private Animator bodyAnim;
    private Animator playerAnim;
    private bool isInitialized = false;

    private void Start()
    {
        playerArcher = GetComponent<PlayerArcher>();
        playerUmbrella = GetComponent<PlayerUmbrella>();
        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
        playerAnim = GetComponent<Animator>();
        nextReservedState = state;
    }

    private void Update()
    {
        if (!isInitialized)
        {
            SetToState(state);
            isInitialized = true;
        }
    }

    // Called by PlayerController
    public void ChangeStateRight()
    {
        TriggerStateChangeAnim((PlayerStanceState)((int)(nextReservedState + 1) % 3));
    }

    // Called by PlayerController
    public void ChangeStateLeft()
    {
        if (nextReservedState - 1 < 0)
        {
            TriggerStateChangeAnim(PlayerStanceState.NIBEL);
        }
        else
        {
            TriggerStateChangeAnim(nextReservedState - 1);
        }

    }

    private void TriggerStateChangeAnim(PlayerStanceState nextState)
    {
        nextReservedState = nextState;
        switch (nextState)
        {
            case PlayerStanceState.MIA:
                playerAnim.SetTrigger("ToMia");
                break;

            case PlayerStanceState.RIGEN:
                playerAnim.SetTrigger("ToRigen");
                break;

            case PlayerStanceState.NIBEL:
                playerAnim.SetTrigger("ToNibel");
                break;

            default:
                break;
        }

    }

    // Animation Event
    public void DestroyVFX()
    {
        Destroy(stanceSphere.GetComponentInChildren<ParticleSystem>()?.gameObject);
    }

    public void SetToState(PlayerStanceState nextState)
    {
        state = nextState;
        bodyAnim.SetTrigger("AimingReset");
        playerStanceChanged_Event.OnOccur();
        switch (state)
        {
            case PlayerStanceState.MIA:
                SetAsMIA();
                break;

            case PlayerStanceState.RIGEN:
                SetAsRIGEN();
                break;

            case PlayerStanceState.NIBEL:
                SetAsNIVEL();
                break;

            case PlayerStanceState.PANDORA:
                SetAsPANDORA();
                break;

            default:
                break;
        }
    }

    // Animation Event
    public void SetAsMIA()
    {
        int stanceIdx = (int)PlayerStanceState.MIA;
        hairMat.color = hairColor[stanceIdx];
        clothMat.color = clothColor[stanceIdx];
        accessoriesMat.color = accessoriesColor[stanceIdx];

        playerArcher.IsEnable = false;
        playerUmbrella.IsEnable = false;
    }
    // Animation Event
    public void SetAsRIGEN()
    {
        int stanceIdx = (int)PlayerStanceState.RIGEN;
        hairMat.color = hairColor[stanceIdx];
        clothMat.color = clothColor[stanceIdx];
        accessoriesMat.color = accessoriesColor[stanceIdx];

        playerArcher.IsEnable = true;
        playerUmbrella.IsEnable = false;
    }

    // Animation Event
    public void SetAsNIVEL()
    {
        int stanceIdx = (int)PlayerStanceState.NIBEL;
        hairMat.color = hairColor[stanceIdx];
        clothMat.color = clothColor[stanceIdx];
        accessoriesMat.color = accessoriesColor[stanceIdx];

        playerArcher.IsEnable = false;
        playerUmbrella.IsEnable = true;
    }

    // ??? not in use yet
    private void SetAsPANDORA()
    {
        int stanceIdx = (int)PlayerStanceState.PANDORA;
        hairMat.color = hairColor[stanceIdx];
        clothMat.color = clothColor[stanceIdx];
        accessoriesMat.color = accessoriesColor[stanceIdx];

        playerArcher.IsEnable = false;
        playerUmbrella.IsEnable = false;
    }
}


