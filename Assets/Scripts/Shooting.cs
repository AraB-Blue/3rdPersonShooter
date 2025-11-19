using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
    
   public void Shoot ()
    {
        IsOnLoS();
    }
    public bool IsOnLoS(Vector3 enemyPosition, float weaponRange)
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, enemyPosition, out hit, weaponRange))
        {
            Character character = hit.collider.GetComponent<Character>();

            if (character != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
     }
    }
