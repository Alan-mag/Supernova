using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : EventChannelBaseSO
{
    public UnityAction<int> OnIntEventRequest;
    public void RaiseEvent(int value)
    {
        if (OnIntEventRequest != null)
            OnIntEventRequest.Invoke(value);
    }
}
