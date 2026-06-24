using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scroreText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _liveText;
    [SerializeField] private IntEventChannelSO _onScoreChange;
    [SerializeField] private IntEventChannelSO _onLiveChange;
    [SerializeField] private BoolEventChannelSO _onGameOver;
    [SerializeField] private VoidEventChannelSO _onGameRestart;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _onScoreChange.OnEventRaised += OnScoreChange;
        _onGameOver.OnEventRaised += OnGameOver;
        _onLiveChange.OnEventRaised += OnLiveChange;
        if(_restartButton != null)
        {
            _restartButton.onClick.AddListener(OnRestartGame);
        } 
    }

    private void OnDisable()
    {
        _onScoreChange.OnEventRaised -= OnScoreChange;
        _onGameOver.OnEventRaised -= OnGameOver;
        _onLiveChange.OnEventRaised -= OnLiveChange;
        if(_restartButton != null)
        {
            _restartButton.onClick.RemoveListener(OnRestartGame);
        }
    }

    private void OnRestartGame()
    {
        _onGameRestart.RaiseEvent();
    }

    private void OnScoreChange(int totalScore)
    {
        _scroreText.text = "Score:" + totalScore;
    }

    private void OnGameOver(bool isGameOver)
    {
        _gameOverText.text = "GAME OVER";
        _gameOverText.gameObject.SetActive(isGameOver);
        _restartButton.gameObject.SetActive(true);
    }

    private void OnLiveChange(int lives)
    {
        _liveText.text = "Lives: " + lives;
    }

}
