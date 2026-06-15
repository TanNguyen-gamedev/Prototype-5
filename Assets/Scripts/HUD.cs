using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scroreText;
    [SerializeField] private IntEventChannelSO _onScoreChange;

    private void OnEnable()
    {
        _onScoreChange.OnEventRaised += OnScoreChange;
    }

    private void OnDisable()
    {
        _onScoreChange.OnEventRaised -= OnScoreChange;
    }

    private void OnScoreChange(int value)
    {
        _scroreText.text = "Score:" + value;
    }


}
