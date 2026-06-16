using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannelSO", menuName = "Event/VoidEventChannelSO")]
public class VoidEventChannelSO : ScriptableObject
{
    public event UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if(OnEventRaised != null)
        {
            OnEventRaised?.Invoke();  
        }
    }
}
