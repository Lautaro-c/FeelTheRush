using UnityEngine;

public class CallEvents : MonoBehaviour
{
    [SerializeField] Actor subjectToObserve;

    private void OnDie()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        if (subjectToObserve != null) 
        {
            subjectToObserve.OnDie += OnDie;
        }
    }

    private void OnDestroy()
    {
        if ( subjectToObserve != null)
        {
            if (subjectToObserve != null )
            {
                subjectToObserve.OnDie -= OnDie;
            }
        }
    }
}