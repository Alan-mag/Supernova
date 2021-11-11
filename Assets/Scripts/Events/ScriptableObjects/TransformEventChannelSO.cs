using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
public class TransformEventChannelSO : DescriptionBaseSO
{
    public UnityAction<Transform> OnEventRaised;

    public void RaiseEvent(Transform value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
