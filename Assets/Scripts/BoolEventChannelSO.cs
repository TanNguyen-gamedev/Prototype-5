using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "Event/BoolEventChannelSO")]
public class BoolEventChannelSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool value)
    {
        OnEventRaised?.Invoke(value);
    }
}
