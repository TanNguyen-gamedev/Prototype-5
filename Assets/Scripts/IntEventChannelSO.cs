using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannelSO", menuName = "Event/IntEventChannelSO")]
public class IntEventChannelSO : ScriptableObject
{
    public event UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        if(OnEventRaised != null)
        {
            OnEventRaised?.Invoke(value);  
        }
    }
}
