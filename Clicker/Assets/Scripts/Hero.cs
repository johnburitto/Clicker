using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private double _damage;
    private int _heroLvl;
    [SerializeField]
    private HeroType _heroType = HeroType.None;
    [SerializeField]
    private HeroElement _heroElement = HeroElement.None;

    public double Damage {
        get { return _damage; }
        set { _damage = value; } 
    }

    public HeroType HeroType
    {
        get { return _heroType; }
    }

    public HeroElement HeroElement
    {
        get { return _heroElement; }
    }

    void Start()
    {
        _heroLvl = 0;
    }
    
    void Update()
    {
        
    }

    public void UpdateStats()
    {
        _heroLvl++;
        _damage += 2.2 * 0.5 * _heroLvl;
    }

    public static void UpdateAllHeroes(List<Hero> heroes)
    {
        foreach (Hero hero in heroes)
        {
            hero.UpdateStats();
        }
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
