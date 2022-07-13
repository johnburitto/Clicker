using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponUpdater : Updater
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
    }

    public void OnClick()
    {
        TryUpdate();
    }

    protected override void TryUpdate()
    {
        if (_weapon.WeaponLvl * UpdateScaler <= Wallet.Instance.Cash)
        {
            _weapon.WeaponLvl += 0.05f;
            _weapon.WeaponDamage += _weapon.WeaponDamage * _weapon.WeaponLvl;
        }
    }
}
