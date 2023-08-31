using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Private fields
    private AudioSource audioSource;
    private float volume;

    // Public fields
    public static MusicManager Instance { get; private set; }

    // Unity Methods
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        // Get save music volume and use it in the game
        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefsKeys.MUSIC_VOLUME, 0.3f);
        audioSource.volume = volume;
    }

    // Public Methods
    public void ChangeVolume()
    {
        // Increase volume by 10%
        volume += .1f;

        // As we are only increasing the volume, after full volume we start from beginning again
        if (volume > 1f)
            volume = 0f;

        audioSource.volume = volume;

        // Save the music volume
        PlayerPrefs.SetFloat(Constants.PlayerPrefsKeys.MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}