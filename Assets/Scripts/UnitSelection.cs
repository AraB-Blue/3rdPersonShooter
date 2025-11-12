using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance; //Patron Singleton
    

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100f))
            {
                Unit unit = hit.collider.GetComponent<Unit>();

                if (unit != null && unit.isFriendly && unit.hasActed)
                {
                    Debug.Log("Buena esa, soy una unidad" + unit.name);
                }
                else
                {
                    Debug.Log("Intentalo de nuevo, no soy una unidad");
                }
            }        
        }
    }
}
