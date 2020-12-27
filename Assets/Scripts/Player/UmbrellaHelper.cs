using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHelper : MonoBehaviour
{
    private PlayerUmbrella playerUmbrella;
    private void Start()
    {
        playerUmbrella = GetComponentInParent<PlayerUmbrella>();
    }

    //public void PlayUmbrellaUnfold_AnimationEventHelper()
    //{
    //    playerUmbrella.PlayUmbrellaUnfoldSoundOnce();
    //}
}
