using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    public event Action OnDie;
    private Animator enemyAnimator;
    private Transform cameraTransform;
    private float force = 50f;
    private Rigidbody enemyRb;
    private CapsuleCollider enemyCc;

    void Awake()
    {
        currentHealth = maxHealth;
        OnDie += PlayDeathAnimation;
        OnDie += SendEnemyFlying;
    }

    private void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        cameraTransform = GameObject.Find("Main Camera").transform;
        enemyRb = this.GetComponent<Rigidbody>();
        enemyCc = this.GetComponent<CapsuleCollider>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        { 
            Death(); 
        }
    }

    void Death()
    {
        ScoreManager.Instance?.RegisterKill();
        OnDie.Invoke();
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        DestroyEnemy();
    }

    private void PlayDeathAnimation()
    {
        enemyAnimator.SetTrigger("EnemyDied");
    }

    private void SendEnemyFlying()
    {
        if (enemyRb != null && cameraTransform != null && enemyCc != null)
        {
            enemyCc.isTrigger = true;
            enemyRb.useGravity = true;
            Vector3 shootDirection = cameraTransform.forward;
            enemyRb.AddForce(shootDirection.normalized * force, ForceMode.Impulse);
        }
    }

    private void DestroyEnemy()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<PlayerController>())
        {
            enemyAnimator.SetTrigger("PlayerSeen");
        }
    }

}