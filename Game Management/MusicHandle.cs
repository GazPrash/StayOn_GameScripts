using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandle : MonoBehaviour
{
    [SerializeField] Sprite MusicOnSprite, MusicOffSprite;
    private Image MusicTogglePreview;

    void Start()
    {
        Debug.Log("started");
        MusicTogglePreview = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(MToggle);
        CheckMusic();
    }

    void CheckMusic()
    {
        MusicTogglePreview.sprite = AudioManager.AMInstance.MusicOn ? MusicOnSprite : MusicOffSprite;
    }

    void MToggle()
    {
        AudioManager.AMInstance.MusicToggle();
        CheckMusic();
    }
 
}
