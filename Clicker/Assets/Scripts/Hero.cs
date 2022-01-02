using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private double _damage;

    public double Damage {
        get { return _damage; }
        set { _damage = value; } 
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void UpdateDamage()
    {
        _damage += 2.2;
    }
}
