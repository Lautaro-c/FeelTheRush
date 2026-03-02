using UnityEngine;

public class UFOAttack : MonoBehaviour
{
    private UFOAI ufoAi;

    private void Start()
    {
        ufoAi = transform.GetComponentInParent<UFOAI>();
    }

    private void Shoot()
    {
        ufoAi.Shoot();
    }
}
