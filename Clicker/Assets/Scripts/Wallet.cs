using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private float _cash;

    public static Wallet Instance;

    public float Cash => _cash;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCash(float cashToAdd)
    {
        _cash += cashToAdd;
    }

    public void SpendCash(float cashToSpend)
    {
        _cash -= cashToSpend;
    }
}
