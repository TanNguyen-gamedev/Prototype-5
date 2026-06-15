using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scroreText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private IntEventChannelSO _onScoreChange;
    [SerializeField] private BoolEventChannelSO _onGameOver;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _onScoreChange.OnEventRaised += OnScoreChange;
        _onGameOver.OnEventRaise += OnGameOver;
        
    }

    private void OnDisable()
    {
        _onScoreChange.OnEventRaised -= OnScoreChange;
        _onGameOver.OnEventRaise -= OnGameOver;
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

}
