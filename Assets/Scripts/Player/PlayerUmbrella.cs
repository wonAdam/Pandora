using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUmbrella : MonoBehaviour
{
    [SerializeField] GameObject umbrella_fold;
    [SerializeField] GameObject umbrella_unfold;
    [SerializeField] AudioClip umbrellaUnfoldSound;

    private bool isEnable;
    public bool IsEnable
    {
        get { return isEnable; }
        set
        {
            isEnable = value;
            UmbrellaEquip(isEnable);
        }
    }

    private void UmbrellaEquip(bool equip)
    {
        umbrella_fold.SetActive(equip);
        umbrella_unfold.SetActive(false);
    }

    public void UmbrellaFolding(bool fold)
    {
        umbrella_fold.SetActive(fold);
        umbrella_unfold.SetActive(!fold);
    }

    public void PlayUmbrellaUnfoldSoundOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(umbrellaUnfoldSound);
    }
}
