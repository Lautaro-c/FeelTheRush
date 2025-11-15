using UnityEngine;

public class PlayerSear : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<PlayerController>())
        {
            enemyAnimator.SetTrigger("PlayerSeen");
        }
    }
}
