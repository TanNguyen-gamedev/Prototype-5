using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int _difficultyLevel = 1;
    public Button Button;

    private void Start()
    {
        if(Button == null)
        {
            Button = GetComponent<Button>();    
        }
    }

    public int GetDifficulty()
    {
        return _difficultyLevel;
    }

}
