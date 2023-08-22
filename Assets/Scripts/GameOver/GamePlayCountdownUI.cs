using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCountdownUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Image timerImage;

    // Unity Methods
    private void Update() 
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalised();
    }
}
