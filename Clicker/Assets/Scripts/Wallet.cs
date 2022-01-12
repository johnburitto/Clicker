using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private double _cash;

    public static Wallet Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void AddCash(double cashToAdd)
    {
        _cash += cashToAdd;
    }
}
