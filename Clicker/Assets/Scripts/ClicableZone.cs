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

    private void Start()
    {
        _elementalCoef = ElementalCoef.Init;
        GlobalEventManager.LoadGame.Invoke();
    }

    public void OnClick()
    {
        if (_enemy.GetDamage(_heroes, _elementalCoef))
        {
            Hero.UpdateAllHeroes(_heroes);
            Wallet.Instance.AddCash(100);
            GlobalEventManager.OnEnemyKilled.Invoke(); 
            GlobalEventManager.SaveGame.Invoke();
        }

        Debug.Log("Enemy health => " + _enemy.Health);
    }

    public void OnDestroy()
    {
        GlobalEventManager.SaveGame.Invoke();   
    }
}
