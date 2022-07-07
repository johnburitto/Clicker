using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponRare _weaponRare = WeaponRare.None;
    [SerializeField] private HeroType _heroType = HeroType.None;
    [SerializeField] private BigNumber _weaponDamage;
    [SerializeField] private float _weaponLvl;

    public WeaponRare WeaponRare => _weaponRare;

    public HeroType HeroType => _heroType;

    public BigNumber WeaponDamage => _weaponDamage;

    public float WeaponLvl => _weaponLvl;

    private void Start()
    {
        _weaponDamage = BigNumber.ValueOf((float)_weaponRare * 1.25f);
        _weaponLvl = 0;
    }

    public void UpdateWeapon()
    {
        _weaponLvl += 0.05f;
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
