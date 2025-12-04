using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
   [SerializeField] ParticleSystem particleSparks;
    private Unit unit;
   

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

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

       if (unit != null)
        {
            unit.FinishAttack();
 
        }

    }
    public bool IsOnLoS(Vector3 enemyPosition, float weaponRange)
    {

        RaycastHit hit;

        Vector3 direction = (enemyPosition - transform.position).normalized;
        
        Debug.DrawRay(transform.position, direction * weaponRange, Color.red, 1f);
        
        if (Physics.Raycast(transform.position, direction, out hit, weaponRange))
        {
            Debug.Log("Preparado para disparar");
            
            Character character = hit.collider.GetComponent<Character>();
            
            if (character != null)
            {
                return true;
            }
        }
        return false;
     }

    private void GenerateHitParticles(Vector3 hitPoint)
    {
        // Prefab particulas
        GameObject particlesPrefab = Resources.Load<GameObject>("Sparks");

        if (particlesPrefab != null)
        {
            GameObject particles = Instantiate(particlesPrefab, hitPoint, Quaternion.identity);
            ParticleSystem ps = particles.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                // Destruye particulas
                Destroy(particles, ps.main.duration + ps.main.startLifetime.constantMax);
            }
        }
        else
        {
            Debug.LogWarning("No se encontro el prefab 'Sparks' en Resources");
        }
    }
}

  
