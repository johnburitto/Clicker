using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GlobalEventManager
{
    public static UnityEvent OnEnemyKilled = new UnityEvent();
    public static UnityEvent SaveGame = new UnityEvent();
    public static UnityEvent LoadGame = new UnityEvent();
}
