using UnityEngine;
using UnityEngine.Events;
public class CallEvents : MonoBehaviour
{
    [SerializeField] Actor subjectToObserve;

    private void OnDie()
    {
        Debug.Log("murio");
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





    /*

    private void Start()
    {
        OnWin = new UnityEvent();
        OnWin.AddListener();
    }
    */

}
