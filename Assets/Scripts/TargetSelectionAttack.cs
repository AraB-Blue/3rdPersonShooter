using UnityEngine;

public class TargetSelectionAttack : MonoBehaviour
{
    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject characterShooting;
    [SerializeField] Weapon weapon; 

    Shooting shooter;

    private void Awake()
    {
        shooter = characterShooting.GetComponent<Shooting>();
    }

    public void ShootTarget1()
    {
        if (shooter == null) return;

        Vector3 targetPos = target_1.transform.position;
        float range = weapon.GetWeaponRange();

        shooter.Shoot(targetPos, range);
    }

    public void ShootTarget2()
    {
        if (shooter == null) return;

        Vector3 targetPos = target_2.transform.position;
        float range = weapon.GetWeaponRange();

        shooter.Shoot(targetPos, range);
    }
}


