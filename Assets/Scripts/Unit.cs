using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] string characterName;

    public bool hasActed = true;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Run()
    {
        if (hasActed)
        {
            return;
        }

        Debug.Log(characterName + " usa la accion correr");
        FinishAction();
    }

    public void Attack()
    {
        if (hasActed)
        {
            return;
        }

        Debug.Log(characterName + " usa la accion atacar");
        FinishAction();
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
    }

    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
    }
}
