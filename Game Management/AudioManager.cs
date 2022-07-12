using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{

    public static AudioManager AMInstance;
    public static string ActiveTrackName;

    public bool MusicOn = true;
    public AudioClip ActiveTrack;

    [SerializeField]
    public AudioClip clip012, clip3, clip4, clip5, clip6;

    [SerializeField] private AudioSource MusicPlayer;
    private int ActiveScene = -1, PrevScene = -2;

    private void Awake()
    {
        if (AMInstance == null)
        {
            AMInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        else Destroy(gameObject);
    }

    private void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
    }

    public void MusicToggle()
    {
        if (!MusicOn) MusicPlayer.Play();
        MusicOn = !MusicOn;
    }

    private void Update()
    {
        if (!MusicOn)
        {
            MusicPlayer.Pause();
            return;
        }

        ActiveScene = SceneManager.GetActiveScene().buildIndex;
        if (PrevScene == ActiveScene) return;

        switch (ActiveScene)
        {
            case (0):
            case (1):
            case (2):
                AlterMusic(clip012);
                break;

            case (3):
                AlterMusic(clip3);
                break;

            case (4):
                AlterMusic(clip4);
                break;
            case (5):
                AlterMusic(clip5);
                break;
            case (6):
                AlterMusic(clip6);
                break;
        }

        PrevScene = ActiveScene;
    }

    public void AlterMusic(AudioClip Clip)
    {
        MusicPlayer.Pause();
        MusicPlayer.clip = Clip;
        MusicPlayer.Play();
    }


}
