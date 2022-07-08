using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;
    public GameObject Enemy;
    public List<GameObject> Heroes;

    private void OnEnable()
    {
        GlobalEventManager.SaveGame.AddListener(SaveGame);
        GlobalEventManager.LoadGame.AddListener(LoadGame);
    }

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        filePath = Path.Combine(Application.persistentDataPath, "save.gamesave");
#else
        filePath = Path.Combine(Application.dataPath, "save.gamesave");
#endif
    }

    public void SaveGame() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveEnemy(Enemy);
        save.SaveWallet(Wallet.Instance.Cash);

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
        Wallet.Instance.AddCash(save.Cash);

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

    public void OnDisable()
    {
        GlobalEventManager.SaveGame.RemoveListener(SaveGame);
        GlobalEventManager.LoadGame.RemoveListener(LoadGame);
    }
}

[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct EnemySaveData
    {
        public int HealthScale, PreviousHealthScale;
        public float HealthNumber, EnemyLvl, PreviousHealthNumber;

        public EnemySaveData(float healthNumber, int healthScale, float previousHealthNumber, 
                             int previousHealthScale, float enemyLvl)
        {
            HealthNumber = healthNumber;
            HealthScale = healthScale;
            PreviousHealthNumber = previousHealthNumber;
            PreviousHealthScale = previousHealthScale;
            EnemyLvl = enemyLvl;
        }
    }

    [System.Serializable]
    public struct WeaponSaveData
    {
        public int WeaponRare, HeroType, WeaponDamageScale;
        public float WeaponDamageNumber, WeaponLvl;

        public WeaponSaveData(int weaponRare, int heroType, float weaponDamageNumber, 
                              int weaponDamageScale, float weaponLvl)
        {
            WeaponRare = weaponRare;
            HeroType = heroType;
            WeaponDamageNumber = weaponDamageNumber;
            WeaponDamageScale = weaponDamageScale;
            WeaponLvl = weaponLvl;
        }
    }

    [System.Serializable]
    public struct HeroSaveData
    {
        public int HeroType, HeroElement, HeroLvl, DamageScale;
        public float DamageNumber;

        public HeroSaveData(int heroType, int heroElement, int heroLvl, float damageNumber, int damageScale)
        {
            HeroType = heroType;
            HeroElement = heroElement;
            HeroLvl = heroLvl;
            DamageNumber = damageNumber;
            DamageScale = damageScale;
        }
    }

    public EnemySaveData Enemy;
    public List<WeaponSaveData> Weapons = new List<WeaponSaveData>();
    public List<HeroSaveData> Heroes = new List<HeroSaveData>();
    public float Cash;

    public void SaveEnemy(GameObject enemy)
    {
        Enemy currentEnemy = enemy.gameObject.GetComponent<Enemy>();

        Enemy = new EnemySaveData(currentEnemy.Health.Number,
                                  currentEnemy.Health.NumberScale,
                                  currentEnemy.PreviousHealth.Number,
                                  currentEnemy.PreviousHealth.NumberScale,
                                  currentEnemy.EnemyLvl);
    }

    public void SaveWeapon(Weapon weapon)
    {
        Weapons.Add(new WeaponSaveData((int)weapon.WeaponRare,
                                       (int)weapon.HeroType,
                                       weapon.WeaponDamage.Number,
                                       weapon.WeaponDamage.NumberScale,
                                       weapon.WeaponLvl));
    }

    public void SaveHero(GameObject hero)
    {
        Hero currentHero = hero.gameObject.GetComponent<Hero>();

        Heroes.Add(new HeroSaveData((int)currentHero.HeroType,
                                    (int)currentHero.HeroElement,
                                    currentHero.HeroLvl,
                                    currentHero.Damage.Number,
                                    currentHero.Damage.NumberScale));
    }

    public void SaveWallet(float cash)
    {
        Cash = cash;
    }
}
