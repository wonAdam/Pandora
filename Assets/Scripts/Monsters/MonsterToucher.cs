using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MonsterToucher : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] int touchDamage;
    [SerializeField] float pushMagnitude;
    private void OnTriggerStay(Collider other)
    {
        if(1 << other.gameObject.layer == (int)playerMask)
        {
            other.transform.GetComponentInParent<Health>().TakeDamage(touchDamage, transform);
        }
    }
}
