using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private double _cash;
    public static Wallet Instance;

    public double Cash
    {
        get { return _cash; }
    }

    void Awake()
    {
        Instance = this;
    }

    public void AddCash(double cashToAdd)
    {
        _cash += cashToAdd;
    }
}
