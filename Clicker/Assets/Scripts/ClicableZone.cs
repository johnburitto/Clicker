using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicableZone : MonoBehaviour
{
    
    [SerializeField]
    private Hero _hero;
    [SerializeField]
    private Enemy _enemy;

    void Start()
    {
    
    }

    void Update()
    {

    }

    public void OnClick()
    {
        if (_enemy.GetDamage(_hero) <= 0)
        {
            _enemy.UpdateLvl();
            _hero.UpdateDamage();
            _enemy.GenerateNewHP();
        }
    }
}
