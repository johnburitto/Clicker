using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponRare _weaponRare = WeaponRare.None;
    [SerializeField] private HeroType _heroType = HeroType.None;
    [SerializeField] private BigNumber _weaponDamage;
    [SerializeField] private float _weaponLvl;

    public WeaponRare WeaponRare => _weaponRare;
    public HeroType HeroType => _heroType;
    public BigNumber WeaponDamage 
    {
        get
        {
            return _weaponDamage;
        }
        set
        {
            _weaponDamage = value;
        }
    }
    public float WeaponLvl
    {
        get
        {
            return _weaponLvl;
        }
        set
        {
            _weaponLvl = value;
        }
    }

    private void Start()
    {
        _weaponDamage = BigNumber.ValueOf((float)_weaponRare * 1.25f);
        _weaponLvl = 0;
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
