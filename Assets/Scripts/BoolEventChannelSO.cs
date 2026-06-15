using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "Event/BoolEventChannelSO")]
public class BoolEventChannelSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaise;

    public void RaiseEvent(bool value)
    {
        OnEventRaise?.Invoke(value);
    }
}
