using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyMainScript : MonoBehaviour
{
    public int HP = 100;
    public GameObject Enemy;
    public Animator anim;
    public Text EnemyHPValue;
    public string HPString;
    public bool enemyIsDefending;
    public GameObject eCursor;

    public PlayerScript pScript;
    public GameManager gm;
    public runSequence rSeq;
    

    public bool isMinion;

    public int enemyAttackValue;

    public GameObject[] handHiders;
    
    void Start()
    {
       eCursor.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        HPString = HP.ToString();
        EnemyHPValue.text = HPString;

        //if (HP <= 0)
        //{
        //    StartCoroutine(dieRoutine());
        //    EnemyHPValue.text = "0";
        //}

    }

    private void OnMouseOver()
    {
        if(gm.isChoosing == true)
        {
          {eCursor.SetActive(true);}
        }
    }

    private void OnMouseExit()
    {
        eCursor.SetActive(false);
    }

    

    public void enemyAttack()
    {
        anim.SetBool("isAttacking", true);

        if (pScript.isDefending == false)
        {
            pScript.takeDamage(enemyAttackValue);
        }
        else if (pScript.isDefending == true)
        {
            pScript.Defending();
            pScript.takeDamage(enemyAttackValue - pScript.defendStacks);
        }
        waitRoutine();
    }

    public void enemyDefend()
    {
        enemyIsDefending = true;
    }

    public void takeDamage(int damage)
    {
        HP = HP - damage;
        anim.SetBool("isDamaged", true);
        StartCoroutine(waitRoutine());
        if (HP <= 0)
        {
            StartCoroutine(dieRoutine());
            EnemyHPValue.text = "0";
        }

    }

    public IEnumerator waitRoutine()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("isDamaged", false);
        anim.SetBool("isAttacking", false);
    }

    

    IEnumerator dieRoutine()
    {
        yield return new WaitForSeconds(2);
        
        //gm.enemiesLeft--;
        
        
        
    }
}
