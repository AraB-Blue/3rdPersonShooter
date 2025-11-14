using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Unit selectedUnit;
    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (!TurnManager.Instance.isPlayerTurn)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Unit unit = hit.collider.GetComponent<Unit>();

                if (unit != null && unit.isFriendly && !unit.hasActed)
                {
                    SelectUnit(unit);
                    Debug.Log("Has seleccionado a " +  unit.name);
                }
                else
                {
                    ClearSelection();
                    Debug.Log("No soy una unidad");
                }
            }
        }
    }

    private void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public void ClearSelection ()
    {
        if(selectedUnit !=null)
        {
            selectedUnit = null;
        }
    }
}
