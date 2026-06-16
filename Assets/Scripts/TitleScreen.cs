using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button _easyButton;
    [SerializeField] private Button _mediumButton;
    [SerializeField] private Button _hardButton;
    [SerializeField] private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_easyButton != null)
        {
            _easyButton.onClick.AddListener(() => SetDifficulty(1));
        }
        if(_mediumButton != null)
        {
            _mediumButton.onClick.AddListener(() => SetDifficulty(2));
        }
        if(_hardButton != null)
        {
            _hardButton.onClick.AddListener(() => SetDifficulty(3));   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDifficulty(int difficulty)
    {
        if(_gameManager != null)
        {
            _gameManager.StartGame(difficulty);
        }
    }

}
