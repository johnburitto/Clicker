using UnityEngine;

[RequireComponent(typeof(Hero))]
public class HeroUpdater : Updater
{
    private Hero _hero;

    private void Start()
    {
        _hero = GetComponent<Hero>();
    }

    public void OnClick()
    {
        TryUpdate();
    }

    protected override void TryUpdate()
    {
        if (_hero.HeroLvl * UpdateScaler <= Wallet.Instance.Cash)
        {
            _hero.HeroLvl++;
            _hero.Damage += _hero.Weapon.WeaponDamage * _hero.HeroLvl;
        }
    }
}
