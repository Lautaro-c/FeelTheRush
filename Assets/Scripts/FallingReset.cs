using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingReset : MonoBehaviour
{
    [SerializeField] private List<Actor> actorsToRestart = new List<Actor>();
    [SerializeField] private Vector3 SpawnPlayerPos;
    [SerializeField] private Quaternion SpawnPlayerRotation;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.GetComponent<PlayerController>())
        {
            Restart(other);
        }
    }

    public void Restart(Collider other)
    {
        ScoreManager.Instance.enemiesKilled = 0;
        for (int i = 0; i < actorsToRestart.Count; i++)
        {
            actorsToRestart[i].gameObject.SetActive(true);
            actorsToRestart[i].RestartEnemy();
        }
        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            other.transform.position = SpawnPlayerPos;
            other.transform.rotation = SpawnPlayerRotation;
            other.GetComponent<PlayerController>().RestartSpeed();
            cc.enabled = true;
        }
    }
}
