using Assets.Interfaces;
using UnityEngine;

public class Hero : MonoBehaviour, IUpdatable
{
    [SerializeField] private HeroType _heroType;
    [SerializeField] private HeroElement _heroElement;
    [SerializeField] private BigNumber _damage;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _heroLvl;

    public HeroType HeroType => _heroType;
    public HeroElement HeroElement => _heroElement;
    public BigNumber Damage => _damage;
    public Weapon Weapon => _weapon;
    public int HeroLvl => _heroLvl;

    private void Start()
    {
        _damage = BigNumber.ValueOf(_weapon.WeaponDamage);
    }

    public void UpdateStats()
    {
        _heroLvl++;
        _damage += _weapon.WeaponDamage * _heroLvl;
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
