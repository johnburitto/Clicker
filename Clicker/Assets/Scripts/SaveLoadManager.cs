using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;
    public GameObject Enemy;
    public List<GameObject> Heroes;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        filePath = Path.Combine(Application.persistentDataPath, "save.gamesave");
#else
        filePath = Path.Combine(Application.dataPath, "save.gamesave");
#endif
        GlobalEventManager.SaveGame.AddListener(SaveGame);
        GlobalEventManager.LoadGame.AddListener(LoadGame);
    }

    public void SaveGame() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveEnemy(Enemy);
        
        foreach (var hero in Heroes)
        {
            save.SaveWeapon(hero.gameObject.GetComponent<Hero>().Weapon);
        }

        foreach(var hero in Heroes)
        {
            save.SaveHero(hero);
        }

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        Enemy.gameObject.GetComponent<Enemy>().LoadData(save.Enemy);

        for (int i = 0; i < save.Weapons.Count; i++)
        {
            Heroes[i].gameObject.GetComponent<Hero>().Weapon.LoadData(save.Weapons[i]);
        }

        for (int i = 0; i < save.Heroes.Count; i++)
        {
            Heroes[i].GetComponent<Hero>().LoadData(save.Heroes[i]);
        }

        fs.Close();
    }
}

[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct EnemySaveData
    {
        public double Health, EnemyLvl, HealthScale;

        public EnemySaveData(double health, double enemyLvl, double healthScale)
        {
            Health = health;
            EnemyLvl = enemyLvl;
            HealthScale = healthScale;
        }
    }

    [System.Serializable]
    public struct WeaponSaveData
    {
        public int WeaponRare, HeroType;
        public double WeaponDamage, WeaponLvl;

        public WeaponSaveData(int weaponRare, int heroType, double weaponDamage, double weaponLvl)
        {
            WeaponRare = weaponRare;
            HeroType = heroType;
            WeaponDamage = weaponDamage;
            WeaponLvl = weaponLvl;
        }
    }

    [System.Serializable]
    public struct HeroSaveData
    {
        public int HeroType, HeroElement, HeroLvl;
        public double Damage;

        public HeroSaveData(int heroType, int heroElement, int heroLvl, double damage)
        {
            HeroType = heroType;
            HeroElement = heroElement;
            HeroLvl = heroLvl;
            Damage = damage;
        }
    }

    public EnemySaveData Enemy;
    public List<WeaponSaveData> Weapons = new List<WeaponSaveData>();
    public List<HeroSaveData> Heroes = new List<HeroSaveData>();

    public void SaveEnemy(GameObject enemy)
    {
        Enemy = new EnemySaveData(enemy.gameObject.GetComponent<Enemy>().Health,
                                  enemy.gameObject.GetComponent<Enemy>().EnemyLvl,
                                  enemy.gameObject.GetComponent<Enemy>().HealthScale);
    }

    public void SaveWeapon(Weapon weapon)
    {
        Weapons.Add(new WeaponSaveData((int)weapon.WeaponRare,
                                       (int)weapon.HeroType,
                                       weapon.WeaponDamage,
                                       weapon.WeaponLvl));
    }

    public void SaveHero(GameObject hero)
    {
        Heroes.Add(new HeroSaveData((int)hero.gameObject.GetComponent<Hero>().HeroType,
                                    (int)hero.gameObject.GetComponent<Hero>().HeroElement,
                                    hero.gameObject.GetComponent<Hero>().HeroLvl,
                                    hero.gameObject.GetComponent<Hero>().Damage));
    }
}
