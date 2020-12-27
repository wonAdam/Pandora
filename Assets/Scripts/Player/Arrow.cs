using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] GameObject collisionVFX;
    public int damage;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (((int)Math.Pow(2, collision.collider.gameObject.layer) & (int)collisionLayer) > 0)
        {
            GameObject effect = Instantiate(collisionVFX, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            
            // break process
            if (collision.transform.GetComponent<IBreakable>() != null)
            {
                collision.transform.GetComponent<IBreakable>().Break();
            }

            // damage process
            if(collision.transform.GetComponent<Health>() != null)
            {
                collision.transform.GetComponent<Health>().TakeDamage(damage, transform);
            }


            Destroy(effect, 1f);
        }
        Destroy(gameObject);
        
    }
    
}
