using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Shooting))]
public class EnemyAI : MonoBehaviour
{
    private Unit unit;
    private Shooting shooting;
    [SerializeField] private float visionRange = 5f;
    private float attackRange;

    void Start()
    {
        
    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (unit.isFriendly) return;

        if(TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (!unit.hasActed)
        {
            StartCoroutine(DoenemyTurn());
        }
    }

    IEnumerator DoenemyTurn()
    {
        Unit target = FindClosestPlayerUnit(); //Encontrar aliado cercano

        if (target ==null)
        {
            Debug.Log(unit.characterName + "no encuentra objetivos validos");
            unit.FinishAction();
            yield break;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
        }
    }

    private IEnumerator AttackTarget(Unit target)
    {
        throw new NotImplementedException();
    }

    private bool hasLineOfSight(Unit target)
    {
        throw new NotImplementedException();
    }


    private Unit FindClosestPlayerUnit()
    {
        
        
        Unit closest = null;
        float closestDist = Mathf.Infinity; 
        
        foreach (Unit playerUnit in TurnManager.Instance.playerUnits)
        {
          float dist = Vector3.Distance(transform.position, playerUnit.transform.position);
          if (dist< closestDist && dist <=visionRange)
          {
            closestDist = dist;
            closest = playerUnit;
          }
        }

        return closest;
       
    }
}
