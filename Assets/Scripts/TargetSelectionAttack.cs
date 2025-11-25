using UnityEngine;

public class TargetSelectionAttack : MonoBehaviour
{
    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject characterShooting;

    public void ShootTarget1()
    {
        //characterShooting.Shoot();
        Debug.Log ("Shooting Target 1");
    }

    public void ShootTarget2()
    {
        Debug.Log ("Shooting Target 2");
        //characterShooting.Shoot();
    }
}
