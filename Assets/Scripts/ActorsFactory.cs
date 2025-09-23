using System.Collections.Generic;
using UnityEngine;

public class ActorsFactory : MonoBehaviour
{
    [SerializeField] private Actor[] actors;
    private Dictionary<string, Actor> actorsDictionary;

    private void Start()
    {
        actorsDictionary = new Dictionary<string, Actor>();
        foreach (var actor in actors)
        {
            Debug.Log(actor.name);
            actorsDictionary.Add(actor.name, actor); 
        }
    }

    public Actor Create(string name)
    {
        if(!actorsDictionary.TryGetValue(name, out Actor actor))
        {
            return null;
        }
        return Instantiate(actor);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Create("Enemy");
        }
    }
}