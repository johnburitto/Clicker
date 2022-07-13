using UnityEngine;

public abstract class Updater : MonoBehaviour
{
    [SerializeField] protected int UpdateScaler;

    protected abstract void TryUpdate();
}
