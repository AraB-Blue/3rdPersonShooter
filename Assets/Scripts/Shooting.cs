using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
   [SerializeField] ParticleSystem particleSparks;


   public void Shoot (Vector3 enemyPosition, float weaponRange)
    {
        if (IsOnLoS(enemyPosition, weaponRange))
        {
            particleSparks.Play();
            Debug.Log("Enemigo en linea de tiro");
        }
        else 
        {
            Debug.Log("Enemigo no esta en linea de tiro");
        }
    }
    public bool IsOnLoS(Vector3 enemyPosition, float weaponRange)
    {

        RaycastHit hit;
        Vector3 direction = (enemyPosition - transform.position).normalized;
        Debug.DrawRay(transform.position, direction * weaponRange, Color.red, 1f);
        if (Physics.Raycast(transform.position, enemyPosition, out hit, weaponRange))
        {
            Character character = hit.collider.GetComponent<Character>();

            if (character != null)
            {
                return true;
            }
        }
        return false;
     }
    }
