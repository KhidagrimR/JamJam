using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPenguin : Entity
{
    private PenguinManager penguinManager;

    #region nodes
    [Header("Nodes")]
    // where the king wants to go
    public int currentNodeIndex;
    // all king nodes to move
    public Transform[] patrolNodes;

    public float NodeDistance
    {
        get{
            // magnitude is distance
            return (patrolNodes[currentNodeIndex].position - transform.position).magnitude;
        }
    }

    #endregion
    #region Brood/Hatch
    // is the king on a egg ?
    public bool isBrooding;
    // duration of brooding
    public float broodDuration;
    // current broodduration timer state
    private float broodDurationTimer;
    #endregion


    protected override void Start()
    {
        base.Start();
        penguinManager = GameObject.FindObjectOfType<PenguinManager>();
    }

    protected override void Update()
    {
        base.Update();

        if(NodeDistance < 1.0f && !isBrooding) // small distance between king and egg to hatch
        {
            isBrooding = true;
            broodDurationTimer = broodDuration;
        }
        if(isBrooding)
        {
            broodDurationTimer -= Time.deltaTime;
            if(broodDurationTimer <= 0) // hatch ends
            {
                isBrooding = false;
                MakeEggsHatch();

                //select another destination
                SetANewNode();
            }
        }
        
        if(!isFighting && !isBrooding)
        {
            // on fait patrol le king jusqu au nouveau egg
            Vector2 direction = (patrolNodes[currentNodeIndex].position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    private void MakeEggsHatch()
    {
        Debug.Log("<color=#ADD8E6>" + entityName + "</color> Hatched");
        // get eggs (get component on Node Transform)
        patrolNodes[currentNodeIndex].GetComponent<EntitySpawner>().SpawnEntities();

        //reset hatch spawn time (eggs won t be able to be brood for a small duration
        // TODO
    }

    private void SetANewNode()
    {
        // on crée un tableau de chiffres de 0 a 5
        List<int> availableNodes = new List<int>();
        for(int i = 0, n = patrolNodes.Length; i < n ; i++)
        {
            availableNodes.Add(i);
        }

        // on enléve la destination précédente
        availableNodes.Remove(currentNodeIndex);

        // on choisit une nouvelle destination
        currentNodeIndex = availableNodes[Random.Range(0,availableNodes.Count)];
    }

    protected override void AttackEnemy()
    {
        base.AttackEnemy();
    }

    protected override void TargetEnemy()
    {
        base.TargetEnemy();
    }

    protected override void Death()
    {
        penguinManager.followPenguins.Remove(gameObject);
        base.Death();
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);
    }
}
