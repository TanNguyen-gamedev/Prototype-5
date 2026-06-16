using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private DifficultyButton _easyButton;
    [SerializeField] private DifficultyButton _mediumButton;
    [SerializeField] private DifficultyButton _hardButton;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioSource _musicAudio;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if(_easyButton != null)
        {
            _easyButton.GetComponent<Button>().onClick.AddListener(OnDifficultyClicked);
        }
        if(_mediumButton != null)
        {
            _mediumButton.GetComponent<Button>().onClick.AddListener(OnDifficultyClicked);
        }
        if(_hardButton != null)
        {
            _hardButton.GetComponent<Button>().onClick.AddListener(OnDifficultyClicked);
        }
        if(_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    private void OnDisable()
    {
        if(_easyButton != null)
        {
            _easyButton.GetComponent<Button>().onClick.RemoveListener(OnDifficultyClicked);
        }
        if(_mediumButton != null)
        {
            _mediumButton.GetComponent<Button>().onClick.RemoveListener(OnDifficultyClicked);
        }
        if(_hardButton != null)
        {
            _hardButton.GetComponent<Button>().onClick.RemoveListener(OnDifficultyClicked);
        }
        if(_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        }
    }

    private void OnDifficultyClicked()
    {
        GameObject clickedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(clickedObject != null && clickedObject.TryGetComponent(out DifficultyButton difficulty))
        {
            SetDifficulty(difficulty.GetDifficulty());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeVolume(float volume)
    {
        _musicAudio.volume = volume;
    }

    private void SetDifficulty(int difficulty)
    {
        if(_gameManager != null)
        {
            _gameManager.StartGame(difficulty);
        }
    }

}
