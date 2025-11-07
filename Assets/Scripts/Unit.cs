using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool hasActed;
    void Start()
    {
        hasActed = true;
    }

    
    void Update()
    {
        
    }

    public void StartTurnForThisUnit()
    {
        hasActed = false;
    }

    public void FinishAction()
    {
        hasActed = true;
    }
}
