using System.Collections.Generic;
using UnityEngine;

public class ClicableZone : MonoBehaviour
{
    [SerializeField] private List<Hero> _heroes;
    [SerializeField] private Enemy _enemy;

    private ElementalCoef _elementalCoef;

    public List<Hero> Heroes => _heroes;

    private void Start()
    {
        _elementalCoef = ElementalCoef.Init;
        GlobalEventManager.LoadGame.Invoke();
        Debug.Log(BigNumber.ValueOf(1.55555f, NumberScale.Millions).StringInDecimalFormat);
    }

    public void OnClick()
    {
        if (_enemy.GetDamage(_heroes, _elementalCoef))
        {
            Wallet.Instance.AddCash(100);
            GlobalEventManager.OnEnemyKilled.Invoke(); 
            GlobalEventManager.SaveGame.Invoke();
        }

        Debug.Log("Enemy health => " + _enemy.Health);
    }

    private void OnDestroy()
    {
        GlobalEventManager.SaveGame.Invoke();   
    }
}
