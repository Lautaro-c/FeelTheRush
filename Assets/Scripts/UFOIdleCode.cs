using UnityEngine;

public class UFOIdleCode : MonoBehaviour
{
    
    private UFOAI ufoAi;
    private float idleSpeed;
    private float deviation;
    private bool ascending;
    private void Start()
    {
        ufoAi = this.GetComponent<UFOAI>();
        deviation = 0;
        idleSpeed = 2f;
        ascending = true;
    }
    
    private void Update()
    {
        if (ufoAi.idle)
        {
            if (ascending)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y + idleSpeed * Time.deltaTime, transform.position.z);
                deviation += idleSpeed * Time.deltaTime;
                if(deviation >= 0.5f)
                {
                    ascending = false;
                }
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y - idleSpeed * Time.deltaTime, transform.position.z);
                deviation -= idleSpeed * Time.deltaTime;
                if (deviation <= -0.5f)
                {
                    ascending = true;
                }
            }
        }
    }
}
