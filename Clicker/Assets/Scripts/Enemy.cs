using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private double _health;
    private double _enemyLvl;
    private double _healthScale;

    public double Health
    {
        get { return _health; }
    }

    public double EnemyLvl
    {
        get { return _enemyLvl; }
    }

    public double HealthScale
    {
        get { return _healthScale; }
    }

    void Start()
    {
        _healthScale = _health;
        _enemyLvl = 1;
    }

    void Update()
    {
        
    }

    public void UpdateLvl()
    {
        _enemyLvl += 0.1;
    }

    public double GetDamage(Hero hero)
    {
        return _health -= hero.Damage;
    }

    public void GenerateNewHP()
    {
        _health = Random.Range((int)_healthScale, 2 * (int)_healthScale) * _enemyLvl;
        _healthScale = _health;
    }
}
