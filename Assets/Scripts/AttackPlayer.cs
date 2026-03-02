using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().RecieveDamage();
        }
    }
}
