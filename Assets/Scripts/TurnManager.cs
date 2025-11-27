using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance; //todos pueden acceder
    public bool isPlayerTurn = true;

    public List<Unit> enemyUnits = new List<Unit>();
    public List<Unit> playerUnits = new List<Unit>();

    public TMP_Text turnoAliado, turnoEnemigo;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartPlayerTurn();
    }

    
    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
        ResetUnits(playerUnits);
        UnitSelection.Instance.enabled = true;
        StartCoroutine(MostrarTurno(turnoAliado, "Turno de los aliados"));

        Debug.Log("Turno del jugador");
    }

    private void StartEnemyTurn()
    {
        isPlayerTurn = false;
        ResetUnits(enemyUnits);
        StartCoroutine(MostrarTurno(turnoEnemigo, "turno de los enemigos"));

        Debug.Log("Turno del enemigo");
    }

    IEnumerator MostrarTurno (TMP_Text textoUI, string mensaje)
    {
        textoUI.text = mensaje;
        textoUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        textoUI.gameObject.SetActive(false);
    }
    
    private void ResetUnits (List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            unit.hasActed = false;
        
        }
    }

    bool AllUnitsActed (List <Unit> units)
    {
        foreach (var u in units)
        {
            if (!u.hasActed)
            {
                return false;
            }
        }
        return true;       
    }

    public void CheckEndTurn()
    {
        if (isPlayerTurn)
        {
            if (AllUnitsActed(playerUnits))
                StartEnemyTurn();
        }

        else 
        {
            if (AllUnitsActed(enemyUnits))
                StartPlayerTurn();
        }
    }

    void Update()
    {
        
    }
}
