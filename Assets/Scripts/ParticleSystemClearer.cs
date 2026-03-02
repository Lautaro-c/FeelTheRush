using UnityEngine;

public class ParticleSystemClearer : MonoBehaviour
{
    public void ClearAllParticles()
    {
        ParticleSystem[] allParticles = Object.FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None);
        foreach (var ps in allParticles)
        {
            Debug.Log("Se borro una particula");
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
