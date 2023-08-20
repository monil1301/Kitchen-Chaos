using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    // Unity Methods
    private void Start() 
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailure += DeliveryManager_OnRecipeFailure;
    }

    // Private Methods
    private void PlaySound (AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[UnityEngine.Random. Range (0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) 
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs eventArgs)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }
    
    private void DeliveryManager_OnRecipeFailure(object sender, EventArgs eventArgs)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }   
}
