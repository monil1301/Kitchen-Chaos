using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game states
    private enum State
    {
        WaitingToStart, CountdownToStart, GamePlaying, GameOver
    }

    // Private fields
    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 30f;
    private bool isGamePaused = false;

    // Public fields
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    // Unity Methods
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteraction;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                // This will show the countdown timer before starting the game
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, System.EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                // Handle the game playing timer here
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, System.EventArgs.Empty);
                }
                break;
            case State.GameOver:
                OnStateChanged?.Invoke(this, System.EventArgs.Empty);
                break;
        }
    }

    // Private Methods
    private void GameInput_OnPauseAction(object sender, System.EventArgs e)
    {
        ToggleGamePause();
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, System.EventArgs.Empty);
        }
    }

    // Public Methods
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalised()
    {
        return gamePlayingTimer / gamePlayingTimerMax; // calculate the time remaining for game play
    }

    public void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            // It is used to make time.deltaTime stop. And we are using time.deltaTime everywhere, so this will pause everything
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, System.EventArgs.Empty);
        }
        else
        {
            // Make time.deltaTime work and that will resume everything in the game
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
