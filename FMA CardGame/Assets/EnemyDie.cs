using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled; 
    public void die()
    {
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject);

        Invoke("Respawn", 5);
    }

    void Respawn()
    {
        //Respawn Enemy
    }
}
