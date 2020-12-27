using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerHealth1 : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] Events playerDead_E;

    private Animator bodyAnim;
    private PlayerController playerController;
    public int currHealth;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        currHealth = Mathf.Clamp(currHealth - damage, 0, maxHealth);

        // Dead
        if (currHealth == 0)
        {
            bodyAnim.SetBool("isDead", true);
            playerController.Disable();
        }
    }


    public void PlayerDead()
    {
        Debug.Log("PlayerDead");

        playerDead_E.OnOccur();
    }
}
