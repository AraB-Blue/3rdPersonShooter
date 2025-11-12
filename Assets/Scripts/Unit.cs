using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] string characterName;

    public bool hasActed = true;
    bool hasAttacked = false;
    bool hasMoved = false;
    [SerializeField] public bool isFriendly;
    ClickToMove clickToMove;

    private void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        clickToMove.enabled = false;
    }

    public void Run()
    {
        if (hasActed || hasMoved)
        {
            return;
        }

        if (isFriendly)
        {
            clickToMove.enabled = true;
        }
        else
        {
            Debug.Log("se mueve pero en malvado");
        }
            Debug.Log(characterName + " usa la accion correr"); 
    }

    public void Attack()
    {
        if (hasActed || hasAttacked)
        {
            return;
        }

        Debug.Log(characterName + " usa la accion atacar");
        FinishAttack();
    }

    public void PassTurn()
    {
        if (hasActed)
        {
            return;
        }

        Debug.Log(characterName + " pasa su turno");
        FinishAction();
    }


    public void StartTurnForThisUnit()
    {
        hasActed = false;
        hasAttacked = false;
        hasMoved = false;
    }

    public void FinishMovement ()
    {
        clickToMove.enabled = false;
        hasMoved = true;
    }

    public void FinishAttack ()
    {
        hasAttacked = true;
    }
    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
    }
}
