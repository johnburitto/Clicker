using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponRare _weaponRare = WeaponRare.None;
    [SerializeField]
    private HeroType _heroType = HeroType.None;
    [SerializeField]
    private double _weaponDamage;
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

    public double WeaponDamage
    {
        get { return _weaponDamage; }
    }

    public double WeaponLvl
    {
        get { return _weaponLvl; }
    }

    void Start()
    {
        _weaponDamage = (int)_weaponRare * 1.25;
        _weaponLvl = 0;
    }

    void Update()
    {
        
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
        _weaponDamage = weapon.WeaponDamage;
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
