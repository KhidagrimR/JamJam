using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    public Transform king;

    [Header("Decision for Enemy Behaviour")]
    [Range(0,1)] public float probabilityToAttackPlayer = 0.5f;

    public Transform AssignEnemyTarget()
    {
        Transform target;
        float rnd = Random.Range(0,1);
        if(rnd < probabilityToAttackPlayer)
        {
            target = player;
        }
        else
        {
            target = king;
        }

        return target;
    }
}
