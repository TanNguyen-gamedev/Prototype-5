using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private VoidEventChannelSO _onRestartGame;
    [SerializeField] private VoidEventChannelSO _onResumeGame;
    [SerializeField] private BoolEventChannelSO _onPauseGame;
    [SerializeField] private VoidEventChannelSO _onQuitGame;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private AudioSource _musicAudio;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private GameObject _panel;
    private void OnEnable()
    {
        _onPauseGame.OnEventRaised += OnGamePause;
        if(_restartButton != null)
        {
            _restartButton.onClick.AddListener(OnRestartGame);
        }
        if(_resumeButton != null)
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
        }
        if(_quitButton != null)
        {
            _quitButton.onClick.AddListener(OnQuitGame);
        }
        if(_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        }
    }
    private void OnDisable()
    {
        _onPauseGame.OnEventRaised -= OnGamePause;
        if(_restartButton != null)
        {
            _restartButton.onClick.RemoveListener(OnRestartGame);
        }
        if(_resumeButton != null)
        {
            _resumeButton.onClick.RemoveListener(OnResumeClick);
        }
        if(_quitButton != null)
        {
            _quitButton.onClick.RemoveListener(OnQuitGame);
        }
        if(_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
        }
    }

    private void OnVolumeChange(float volume)
    {
        _musicAudio.volume = volume;
    }

    private void OnGamePause(bool isGamePause)
    {
        _panel.SetActive(isGamePause);
    }

    private void OnResumeClick()
    {
        _onResumeGame.RaiseEvent();

        _panel.SetActive(false);
    }

    private void OnQuitGame()
    {
        _onQuitGame.RaiseEvent();
    }

   
    private void OnRestartGame()
    {
        _onRestartGame.RaiseEvent();
    }
}
