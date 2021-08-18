using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite muteSprite;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Button muteButton;
    private bool isMute;

    public void ToggleMute()
    {
        if(isMute)
        {
            muteButton.image.sprite = muteSprite;
            audioSource.mute = true;
            isMute = false;
        }
        else if(!isMute)
        {
            muteButton.image.sprite = onSprite;
            audioSource.mute = false;
            isMute = true;
        }
    }
}
