using Assets.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IUpdatable
{
    [SerializeField] private BigNumber _health = BigNumber.ValueOf(100f);
    [SerializeField] private float _enemyLvl;
    
    private BigNumber _previousHealth = BigNumber.ValueOf(100f);
    public BigNumber Health => _health;
    public float EnemyLvl => _enemyLvl;
    public BigNumber PreviousHealth => _previousHealth;

    public event UnityAction OnEnemyDead;

    private void OnEnable()
    {
        OnEnemyDead += UpdateStatsAndGenerateNewHP;
    }

    private void OnDisable()
    {
        OnEnemyDead -= UpdateStatsAndGenerateNewHP;
    }

    public void TryApplyDamageFrom(List<Hero> heroes, ElementalCoef elementalCoef)
    {
        ApplyCommonDamage(heroes);
        ApplyElementalDamage(heroes, elementalCoef);

        if (_health.IsZero)
        {
            OnEnemyDead?.Invoke();
        }
    }

    private void ApplyCommonDamage(List<Hero> heroes)
    {
        foreach (Hero hero in heroes)
        {
            _health -= hero.Damage;
        }
    }

    private void ApplyElementalDamage(List<Hero> heroes, ElementalCoef elementalCoef)
    {
        for (int i = 0; i < heroes.Count - 1; i++)
        {
            _health -= ((heroes[i].Damage + heroes[i + 1].Damage) 
                * elementalCoef[(int)heroes[i].HeroElement, (int)heroes[i + 1].HeroElement]);
        }
    }

    public void UpdateStatsAndGenerateNewHP()
    {
        UpdateStats();
        _health = _previousHealth * 1.5f;
        _previousHealth = _health;
    }

    public void UpdateStats()
    {
        _enemyLvl += 0.1f;
    }

    public void LoadData(Save.EnemySaveData enemy)
    {
        _health = BigNumber.ValueOf(enemy.HealthNumber, (NumberScale)enemy.HealthScale);
        _previousHealth = BigNumber.ValueOf(enemy.PreviousHealthNumber, (NumberScale)enemy.PreviousHealthScale);
        _enemyLvl = enemy.EnemyLvl;
    }
}
