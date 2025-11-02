using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            Debug.Log("Colisiono Nulo");
        }
        if (other != null)
        {
            Debug.Log("Colisiono");
        }
        if (other != null && other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Hay que reiniciar la escena");
            SceneManager.LoadScene(1);
        }
    }
}
