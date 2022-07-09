using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent SaveGame = new UnityEvent();
    public static UnityEvent LoadGame = new UnityEvent();
}
