using UnityEngine;

public interface ITriggerObject
{
    public void OnHit(PlayerController playerController);

    public void Reset();
}
