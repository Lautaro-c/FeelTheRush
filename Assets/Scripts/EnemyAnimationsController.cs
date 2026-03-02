using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
    [SerializeField] private GameObject attackPlayer;

    public void ActivateAttack()
    {
        attackPlayer.SetActive(true);
    }

    public void DeactivateAttack()
    {
        attackPlayer.SetActive(false);
    }
}
