using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------Audio Source--------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("--------Audio Clip--------")]
    [SerializeField] public AudioClip music;
    [SerializeField] public AudioClip alienScream;
    [SerializeField] public AudioClip ufoShoot;
    [SerializeField] public AudioClip explosion;
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip land;
    [SerializeField] public AudioClip alienKick;
    [SerializeField] public AudioClip receiveDamage;
    [SerializeField] public AudioClip aliensDeath;
    [SerializeField] public AudioClip clicButton;
    [SerializeField] public AudioClip clickCancel;
    [SerializeField] public AudioClip Victory;
    [SerializeField] public AudioClip jumpOnEnemies;

    [Header("--------Audio Volume--------")]
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private float generalVolume;

    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        if (clip == alienScream)
        {
            SFXSource.volume = sfxVolume * 0.1f;
        }
        else
        {
            SFXSource.volume = sfxVolume;
        }
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

}
