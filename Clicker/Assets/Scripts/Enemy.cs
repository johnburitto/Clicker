using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private double _health;
    [SerializeField]
    private double _enemyLvl;
    [SerializeField]
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
        GlobalEventManager.OnEnemyKilled.AddListener(UpdateLvl);
        GlobalEventManager.OnEnemyKilled.AddListener(GenerateNewHP);
    }

    public void UpdateLvl()
    {
        _enemyLvl += 0.1;
    }

    public double GetDamage(List<Hero> heroes, ElementalCoef elementalCoef)
    {
        GetCommonDamage(heroes);
        GetElementalDamage(heroes, elementalCoef);

        return _health;
    }

    private void GetCommonDamage(List<Hero> heroes)
    {
        foreach (Hero hero in heroes)
        {
            _health -= hero.Damage;
        }
    }

    private void GetElementalDamage(List<Hero> heroes, ElementalCoef elementalCoef)
    {
        for (int i = 0; i < heroes.Count - 1; i++)
        {
            _health -= (heroes[i].Damage + heroes[i + 1].Damage) 
                * elementalCoef.ElementalCoefM[(int)heroes[i].HeroElement][(int)heroes[i + 1].HeroElement];
        }
    }

    public void GenerateNewHP()
    {
        _health = Random.Range((int)_healthScale, 2 * (int)_healthScale) * _enemyLvl;
        _healthScale = _health;
    }

    public void LoadData(Save.EnemySaveData enemy)
    {
        _health = enemy.Health;
        _enemyLvl = enemy.EnemyLvl;
        _healthScale = enemy.HealthScale;
    }

    public void OnDestroy()
    {
        GlobalEventManager.OnEnemyKilled.RemoveListener(UpdateLvl);
        GlobalEventManager.OnEnemyKilled.RemoveListener(GenerateNewHP);
    }
}
