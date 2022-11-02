using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundBase : MonoBehaviour
{
    public AudioSource audioSource;
    
    public AudioClip clickSound;

    private void Awake()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
        
        SubscribeClickSound();
    }

    private void SoundClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
    
    private void SubscribeClickSound()
    {
        var buttonList = FindObjectsOfType<Button>().ToList();
        foreach (var button in buttonList)
        {
            button.onClick.AddListener(SoundClick);
        }
    }

    
}
