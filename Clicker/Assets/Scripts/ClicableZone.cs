using System.Collections.Generic;
using UnityEngine;

public class ClicableZone : MonoBehaviour
{
    [SerializeField]
    private List<Hero> _heroes;
    [SerializeField]
    private Enemy _enemy;
    private ElementalCoef _elementalCoef;

    public List<Hero> Heroes
    {
        get { return _heroes; }
        set { _heroes = value; }
    }

    public Enemy Enemy
    {
        set { _enemy = value; }
    }

    void Start()
    {
        _elementalCoef = ElementalCoef.Init;
        GlobalEventManager.LoadGame.Invoke();
    }

    public void OnClick()
    {
        if (_enemy.GetDamage(_heroes, _elementalCoef) <= 0)
        {
            Hero.UpdateAllHeroes(_heroes);
            Wallet.Instance.AddCash(_enemy.HealthScale * 1.5);
            GlobalEventManager.OnEnemyKilled.Invoke();
            GlobalEventManager.SaveGame.Invoke();
        }
    }

    public void OnDestroy()
    {
        GlobalEventManager.SaveGame.Invoke();   
    }
}
