using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherHelper : MonoBehaviour
{
    private PlayerArcher playerArcher;

    private void OnEnable()
    {
        playerArcher = GetComponentInParent<PlayerArcher>();
    }

    public void ShootArrow_AnimationEventHelper()
    {
        playerArcher.ShootArrow_AnimationEvent();
    }

    public void PlayDrawSoundOnce_AnimationEventHelper()
    {
        playerArcher.PlayDrawSoundOnce();
    }

    public void PlayLooseSoundOnce_AnimationEventHelper()
    {
        playerArcher.PlayLooseSoundOnce();
    }
}
