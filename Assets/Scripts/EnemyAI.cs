using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Shooting))]
public class EnemyAI : MonoBehaviour
{
    private Unit unit;
    private Shooting shooting;
    [SerializeField] private float visionRange = 30f;
    [SerializeField] private float attackRange;
    public float weaponRange;
    private bool isActing = false;
    NavMeshAgent agent;
    Animator animator;
    public TMP_Text EvilAttack;

    void Start()
    {
        UpdateAttackRange();
    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        shooting = GetComponent<Shooting>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void UpdateAttackRange()
    {
        // Obtener el rango del arma equipada
        EnemyCharacter enemyChar = GetComponent<EnemyCharacter>();
        if (enemyChar != null)
        {
            Weapon equippedWeapon = enemyChar.GetEquippedWeapon();
            if (equippedWeapon != null)
            {
                attackRange = equippedWeapon.GetWeaponRange();
            }
            else
            {
                attackRange = 5f; // Rango por defecto
            }
        }
        else
        {
            attackRange = 5f;
        }
    }

    void Update()
    {
        if (unit.isFriendly) return;

        // No actuar si el enemigo está muerto
        Character character = GetComponent<Character>();
        if (character != null && !character.IsAlive())
        {
            if (agent != null)
                agent.enabled = false;
            return;
        }

        if (TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (!isActing)
        {
            StartCoroutine(DoenemyTurn());
        }
    }

    IEnumerator DoenemyTurn()
    {
        isActing = true;

        // Si está muerto, pasar turno inmediatamente
        Character character = GetComponent<Character>();
        if (character != null && !character.IsAlive())
        {
            Debug.Log(unit.characterName + " está muerto, pasa turno");
            gameObject.SetActive(false);  // Ocultar el enemigo muerto
            unit.FinishAction();
            isActing = false;
            yield break;
        }

        Unit target = FindClosestPlayerUnit();

        if (target == null)
        {
            Debug.Log(unit.characterName + " no encuentra objetivos validos");
            unit.FinishAction();
            isActing = false;
            yield break;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

       
        // Intentar atacar inmediatamente
        if (distanceToTarget <= attackRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
            unit.FinishAction();
        }
        else
        {
            // Moverse hacia el objetivo
            yield return MoveTowardTarget(target.transform.position);

            // Intentar atacar otra vez
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= attackRange && hasLineOfSight(target))
            {
                yield return AttackTarget(target);
            }
        }

        // terminar el turno
        //unit.FinishAction();
        isActing = false;
    }


    private IEnumerator MoveTowardTarget(Vector3 targetPosition)
    {
        Debug.Log(unit.characterName + " se mueve buscando a su objetivo:");

        
        agent.isStopped = false;
        agent.destination = targetPosition;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetFloat("forwardMovement", agent.velocity.magnitude);
            yield return null;
        }

        agent.isStopped = true;
        animator.SetFloat("forwardMovement", 0f);
        unit.FinishMovement(); // para de moverse
    }


    private IEnumerator AttackTarget(Unit target)
    {
        Debug.Log(unit.characterName + " ataca a " + target.characterName);

        Vector3 lookDir = target.transform.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        //yield return new WaitForSeconds(1.6f);

        // Obtener el arma del enemigo
        EnemyCharacter enemyChar = GetComponent<EnemyCharacter>();
        float damageDealt = 10f;
        float penetration = 0f;
        string weaponUsed = "puño";

        if (enemyChar != null)
        {
            Weapon equippedWeapon = enemyChar.GetEquippedWeapon();
            if (equippedWeapon != null)
            {
                damageDealt = equippedWeapon.GetWeaponDamage();
                penetration = equippedWeapon.GetWeaponPenetration();
                weaponUsed = equippedWeapon.GetWeaponName();
            }
        }

        shooting.Shoot(target.transform.position, attackRange);

        // Aplicar daño al jugador
        Character targetCharacter = target.GetComponent<Character>();
        if (targetCharacter != null)
        {
            targetCharacter.TakeDamage(damageDealt, penetration);

            // Verificar si el jugador murió
            if (!targetCharacter.IsAlive())
            {
                Debug.Log(target.characterName + " ha muerto por ataque de " + unit.characterName);
                target.enabled = false;
                target.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log(unit.characterName + " causa " + damageDealt + " de daño a " + target.characterName + " con " + weaponUsed);
            }
        }

        yield return new WaitForSeconds(0.1f);

        StartCoroutine (AttackinEvil(EvilAttack, "el enemigo ha atacado"));

        unit.FinishAttack();  // se ha pegado a tortas
             
    }


    private bool hasLineOfSight(Unit target)
    {
        return shooting.IsOnLoS(target.transform.position, weaponRange);
    }


    private Unit FindClosestPlayerUnit()
    {


        Unit closest = null;
        float closestDist = Mathf.Infinity;

        foreach (Unit playerUnit in TurnManager.Instance.playerUnits)
        {
            if (playerUnit == null) continue;

            // Ignorar unidades muertas
            Character playerCharacter = playerUnit.GetComponent<Character>();
            if (playerCharacter != null && !playerCharacter.IsAlive())
                continue;

            float dist = Vector3.Distance(transform.position, playerUnit.transform.position);
            if (dist < closestDist && dist <= visionRange)
            {
                closestDist = dist;
                closest = playerUnit;
            }
        }

        return closest;

    }

    IEnumerator AttackinEvil (TMP_Text textoUI, string mensaje)
    {
        textoUI.text = mensaje;
        textoUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);

        textoUI.gameObject.SetActive(false);
    }
}
