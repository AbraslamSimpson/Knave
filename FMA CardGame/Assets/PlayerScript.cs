using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int HP = 100;
    public string hpString;
    public Text hpValueText;
    public bool isDefending;
    public int attackDamage;

    public int defendStacks;

    public GameManager GM;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hpString = HP.ToString();
        hpValueText.text = hpString;

        
    }

    public void Defending()
    {
        anim.SetBool("isDefending", true);
        StartCoroutine(waitRoutine());
    }

    public void takeDamage(int damage)
    {
        HP = HP - damage;
        if (isDefending == false)
        {
            anim.SetBool("isDamaged", true);
        }
        else if (isDefending == true)
        {
            anim.SetBool("isDefending", true);
        }

        StartCoroutine(waitRoutine());
    }

    public IEnumerator waitRoutine()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("isDamaged", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDefending", false);
    }
}
