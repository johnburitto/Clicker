using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicableZone : MonoBehaviour
{
    private List<Hero> _heroes;
    private Enemy _enemy;
    private ElementalCoef _elementalCoef;

    public List<Hero> Heroes
    {
        set { _heroes = value; }
    }

    public Enemy Enemy
    {
        set { _enemy = value; }
    }

    void Start()
    {
        _elementalCoef = new ElementalCoef();
    }

    void Update()
    {

    }

    public void OnClick()
    {
        if (_enemy.GetDamage(_heroes, _elementalCoef) <= 0)
        {
            _enemy.UpdateLvl();
            Hero.UpdateAllHeroes(_heroes);
            _enemy.GenerateNewHP();
        }
    }
}
