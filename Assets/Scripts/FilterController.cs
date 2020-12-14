using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterController : MonoBehaviour
{
    public GameManager gm;

    private float health;
    private float maxHealth = 18.0f;

    private Animator animator;

    public AudioSource fitlerAudio;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;

    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = GetComponent<Animator>(); 
        health = 18.0f;
    }

    private float Tear()
    {
        animator.SetInteger("Attack", 1);
        fitlerAudio.clip = attack1;
        fitlerAudio.Play();
        Invoke("StopAttacking", 2f);
        return Random.Range(0.5f, 2.0f);
    }

    private int Tangle()
    {
        //Targets whomever is more afraid between Sock and Hair Ties
        //Sock = more afraid = 1
        //Hair Ties = more afraid = 2
        //Equally afraid (aka same intimidation) = 0
        //If 0, attack has a 1/3 to hit Sock, 1/3 Hair Ties, and 1/3 failure 

        if(gm.playerBattle.GetComponent<PlayerBattle>().GetIntimidation() <= 3)
        {
            animator.SetInteger("Attack", 2);
            fitlerAudio.clip = attack2;
            fitlerAudio.Play();
            Invoke("StopAttacking", 2f);
            return 1; 
        }

        return 0;
    }

    private int Dislodge(int num)
    {
        if(num == 2)
        {
            return 0;
        }

        else if(num == 1)
        {
            animator.SetInteger("Attack", 3);
            fitlerAudio.clip = attack3;
            fitlerAudio.Play();
            Invoke("StopAttacking", 2f);
            return 1;
        }

        else
        {
            animator.SetInteger("Attack", 3);
            fitlerAudio.clip = attack3;
            fitlerAudio.Play();
            Invoke("StopAttacking", 2f);
            return Random.Range(1, 2);
        }
    }

    //[numAttack, damage, target]
    //target -> 0.0 = none, 1.0 = Sock, 2.0 Hair Ties, 3.0f = All
    //damage = attack damage OR level of minions
    public float[] Attack(int minions)
    {
        int chanceAttack = Random.Range(1, 100);
        float[] toReturn = new float[3]; 

        if(chanceAttack >= 67)
        {
            toReturn[0] = 0.0f;
            toReturn[1] = 0.0f;
            toReturn[2] = 0.0f;

            return toReturn;
            //return "FITLER \"decides\" not to attack";
        }

        else if(chanceAttack >= 50)
        {
            float damage = Tear();

            toReturn[0] = 1.0f;
            toReturn[1] = damage;
            toReturn[2] = 3.0f;

            return toReturn;
            //return "FITLER uses TEAR";
        }

        else if(chanceAttack >= 34)
        {
            int target = Tangle();

            toReturn[0] = 2.0f;
            toReturn[1] = 0.0f;
            toReturn[2] = (float)target;

            return toReturn;
            //return "FITLER uses TANGLE";
        }

        else
        {
            int numSummon = Dislodge(minions);

            if(numSummon == 0)
            {
                toReturn[0] = 3.0f;
                toReturn[1] = 0.0f;
            }

            else if(numSummon == 1)
            {
                toReturn[0] = 4.0f;
                int level = Random.Range(1, 2);
                toReturn[1] = (float)level;
            }

            else if(numSummon == 2)
            {
                toReturn[0] = 5.0f;
                int level = Random.Range(1, 2);
                toReturn[1] = (float)level;
            }

            toReturn[2] = 0.0f;

            return toReturn;
            //return "FITLER uses DISLODGE...much to your distaste";
        }
    }

    private void StopAttacking()
    {
        animator.SetInteger("Attack", 0);
    }

    public string TakeDamage(bool piercing, float damage)
    {
        if(piercing)
        {
            health -= damage;
            Debug.Log(health);
            return "Fitler took minor damage";
        }

        return "Naive fool";
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
