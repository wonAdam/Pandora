using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float damageCoolTime;
    public float currDamageCoolTime;
    public int currHealth;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currHealth = maxHealth;
    }

    protected virtual void Update()
    {
        currDamageCoolTime = Mathf.Clamp(currDamageCoolTime - Time.deltaTime, 0f, damageCoolTime);
    }

    public virtual bool TakeDamage(int damage, Transform opponent)
    {
        currHealth = Mathf.Clamp(currHealth - damage, 0, maxHealth);
        return true;
    }

    public virtual void Heal(int amount)
    {
        currHealth = Mathf.Clamp(currHealth + amount, 0, maxHealth);
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }


}
