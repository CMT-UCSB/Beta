using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldController : MonoBehaviour
{
    private int level;

    private int speed;
    private int intimidation;
    private float attackDamage;
    private float defenseBuff;
    private float maxHealth;

    public float currHealth;

    private Animator animator;

    public AudioSource moldAudio;
    public AudioClip moldAttack;
    public AudioClip moldHurt;
    public AudioClip moldDie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetLevel(int lvl)
    {
        level = lvl;

        if(level == 1)
        {
            speed = 3;
            intimidation = 1;
            attackDamage = 1.0f;
            defenseBuff = 80.0f;
            maxHealth = 5.0f;
        }

        else if(level == 2)
        {
            speed = 3;
            intimidation = 1;
            attackDamage = 1.5f;
            defenseBuff = 80.0f;
            maxHealth = 5.0f;
        }

        else if(level == 3)
        {
            speed = 4;
            intimidation = 1;
            attackDamage = 2.0f;
            defenseBuff = 85.0f;
            maxHealth = 6.0f;
        }

        else if(level == 4)
        {
            speed = 5;
            intimidation = 2;
            attackDamage = 2.5f;
            defenseBuff = 90.0f;
            maxHealth = 6.5f;
        }

        currHealth = maxHealth;
    }

    public int GetLevel()
    {
        return level;
    }

    public bool AttemptAttack(int playerSpeed, int playerIntimidation, float playerDefense)
    {
        int chance = 100;

        if(playerSpeed > speed)
        {
            chance -= Mathf.FloorToInt(10 / level);
        }

        if(playerIntimidation > intimidation)
        {
            chance -= 5 * (playerIntimidation - intimidation);
        }

        if(playerDefense != 0.0f)
        {
            chance = Mathf.FloorToInt(chance * (1.0f / playerDefense)); 
        }

        int dice = Random.Range(1, 100);

        if(dice <= chance)
        {
            animator.SetBool("isAttacking", true);
            moldAudio.clip = moldAttack;
            moldAudio.Play();
            moldAudio.loop = true; 
            Invoke("StopAttacking", 2);
            return true;
        }

        return false; 
    }

    void StopAttacking()
    {
        animator.SetBool("isAttacking", false);
        moldAudio.Pause();
        moldAudio.loop = false;
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;

        if(currHealth <= 0.0f)
        {
            animator.SetInteger("damageLevel", 4);
            moldAudio.clip = moldDie;
            moldAudio.Play();
            return;
        }

        else if((currHealth / maxHealth) <= (1.0f/4.0f))
        {
            animator.SetInteger("damageLevel", 3);
        }

        else if((currHealth / maxHealth) <= (2.0f/4.0f))
        {
            animator.SetInteger("damageLevel", 2);
        }

        else if((currHealth / maxHealth) <= (3.0f/4.0f))
        {
            animator.SetInteger("damageLevel", 1);
        }

        else
        {
            animator.SetInteger("damageLevel", 0);
        }

        moldAudio.clip = moldHurt;
        moldAudio.Play();
    }

    public float ReturnDamage()
    {
        return attackDamage;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
