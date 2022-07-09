using System.Collections.Generic;
using UnityEngine;

public class ClicableZone : MonoBehaviour
{
    [SerializeField] private List<Hero> _heroes;
    [SerializeField] private Enemy _enemy;

    private ElementalCoef _elementalCoef;

    public List<Hero> Heroes => _heroes;

    private void OnEnable()
    {
        _enemy.OnEnemyDead += MakeSpriteGreen;
    }

    private void Start()
    {
        _elementalCoef = ElementalCoef.Init;
        GlobalEventManager.LoadGame.Invoke();
    }

    private void MakeSpriteGreen()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void OnClick()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        _enemy.TryApplyDamageFrom(_heroes, _elementalCoef);
    }

    private void OnDisable()
    {
        GlobalEventManager.SaveGame.Invoke();
        _enemy.OnEnemyDead -= MakeSpriteGreen;
    }
}
