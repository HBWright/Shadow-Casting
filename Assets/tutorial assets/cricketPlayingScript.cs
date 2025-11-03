using UnityEngine;

public class CricketsAmbient : MonoBehaviour
{
    public AudioClip cricketSound;
    public float volume = 1f;

    private static AudioSource cricketSource;

    void Start()
    {
        // entering the first scene ? no crickets yet ? start them
        if (cricketSource == null)
        {
            cricketSource = gameObject.AddComponent<AudioSource>();
            cricketSource.clip = cricketSound;
            cricketSource.loop = true;
            cricketSource.volume = volume;

            DontDestroyOnLoad(cricketSource.gameObject);
            cricketSource.Play();
        }
        // entering the next scene ? crickets already exist ? kill them
        else
        {
            Destroy(cricketSource.gameObject);
            cricketSource = null;
        }
    }
}
