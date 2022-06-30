using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponRare _weaponRare = WeaponRare.None;
    [SerializeField]
    private HeroType _heroType = HeroType.None;
    [SerializeField]
    private BigNumber _weaponDamage;
    [SerializeField]
    private double _weaponLvl;

    public WeaponRare WeaponRare
    {
        get { return _weaponRare; }
    }

    public HeroType HeroType
    {
        get { return _heroType; }
    }

    public BigNumber WeaponDamage
    {
        get { return _weaponDamage; }
    }

    public double WeaponLvl
    {
        get { return _weaponLvl; }
    }

    void Start()
    {
        _weaponDamage = BigNumber.ValueOf((double)_weaponRare * 1.25);
        _weaponLvl = 0;
    }

    public void UpdateWeapon()
    {
        _weaponLvl += 0.05;
        _weaponDamage += _weaponDamage * _weaponLvl;
    }

    public void LoadData(Save.WeaponSaveData weapon)
    {
        _weaponRare = (WeaponRare)weapon.WeaponRare;
        _heroType = (HeroType)weapon.HeroType;
        _weaponDamage = BigNumber.ValueOf(weapon.WeaponDamageNumber, (NumberScale)weapon.WeaponDamageScale);
        _weaponLvl = weapon.WeaponLvl;
    }
}

public enum WeaponRare
{
    None,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
