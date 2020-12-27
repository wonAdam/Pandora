using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionThrone : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask destoySelfMask;
    public int damage;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Mathf.Log(playerMask, 2f))
        {
            other.transform.GetComponentInParent<Health>().TakeDamage(damage, transform);
        }

        if(((int)Mathf.Pow(2, other.gameObject.layer) & destoySelfMask) > 0)
        {
            Destroy(gameObject);
        }
        
    }
}
