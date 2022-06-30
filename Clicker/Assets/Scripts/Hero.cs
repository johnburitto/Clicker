using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private HeroType _heroType = HeroType.None;
    [SerializeField]
    private HeroElement _heroElement = HeroElement.None;
    [SerializeField]
    private BigNumber _damage;
    [SerializeField]
    private Weapon _weapon;
    private int _heroLvl;

    public HeroType HeroType
    {
        get { return _heroType; }
    }

    public HeroElement HeroElement
    {
        get { return _heroElement; }
    }

    public BigNumber Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public Weapon Weapon
    {
        get { return _weapon; }
        set { _weapon = value; }
    }

    public int HeroLvl
    {
        get { return _heroLvl; }
    }

    void Start()
    {
        _heroLvl = 0;
        _damage = BigNumber.ValueOf(_weapon.WeaponDamage);
    }

    public void UpdateStats()
    {
        _heroLvl++;
        _damage += _weapon.WeaponDamage * _heroLvl;
    }

    public static void UpdateAllHeroes(List<Hero> heroes)
    {
        foreach (Hero hero in heroes)
        {
            hero.Weapon.UpdateWeapon();
            hero.UpdateStats();
        }
    }

    public void LoadData(Save.HeroSaveData hero)
    {
        _heroType = (HeroType)hero.HeroType;
        _heroElement = (HeroElement)hero.HeroElement;
        _heroLvl = hero.HeroLvl;
        _damage = BigNumber.ValueOf(hero.DamageNumber, (NumberScale)hero.DamageScale);
    }
}

public enum HeroType
{
    None,
    Archer,
    Warior,
    Mage,
    Knight
}

public enum HeroElement
{
    Fire,
    Earth,
    Water,
    Wind,
    None
}
