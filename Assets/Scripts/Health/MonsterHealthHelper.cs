using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthHelper : MonoBehaviour
{
    public void DestroySelf_AnimationEventHelper()
    {
        GetComponentInParent<MonsterHealth>().DestroySelf();
    }

    public void DisableAI_AnimationEventHelper()
    {
        GetComponentInParent<MonsterHealth>().DisableAI();
    }
}
