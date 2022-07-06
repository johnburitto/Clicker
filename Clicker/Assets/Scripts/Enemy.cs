using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private BigNumber _health = BigNumber.ValueOf(100);
    [SerializeField]
    private double _enemyLvl;
    [SerializeField]
    private BigNumber _previousHealth = BigNumber.ValueOf(100);

    public BigNumber Health
    {
        get { return _health; }
    }

    public double EnemyLvl
    {
        get { return _enemyLvl; }
    }

    public BigNumber PreviousHealth
    {
        get { return _previousHealth; }
    }

    void Start()
    {
        GlobalEventManager.OnEnemyKilled.AddListener(UpdateLvl);
        GlobalEventManager.OnEnemyKilled.AddListener(GenerateNewHP);
    }

    public void UpdateLvl()
    {
        _enemyLvl += 0.1;
    }

    public bool GetDamage(List<Hero> heroes, ElementalCoef elementalCoef)
    {
        GetCommonDamage(heroes);
        GetElementalDamage(heroes, elementalCoef);

        return _health.IsZero;
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
            _health -= ((heroes[i].Damage + heroes[i + 1].Damage) 
                * elementalCoef.ElementalCoefM[(int)heroes[i].HeroElement][(int)heroes[i + 1].HeroElement]);
        }
    }

    public void GenerateNewHP()
    {
        _health = _previousHealth * 1.5;
        _previousHealth = _health;
    }

    public void LoadData(Save.EnemySaveData enemy)
    {
        _health = BigNumber.ValueOf(enemy.HealthNumber, (NumberScale)enemy.HealthScale);
        _previousHealth = BigNumber.ValueOf(enemy.PreviousHealthNumber, (NumberScale)enemy.PreviousHealthScale);
        _enemyLvl = enemy.EnemyLvl;
    }

    public void OnDestroy()
    {
        GlobalEventManager.OnEnemyKilled.RemoveListener(UpdateLvl);
        GlobalEventManager.OnEnemyKilled.RemoveListener(GenerateNewHP);
    }
}
