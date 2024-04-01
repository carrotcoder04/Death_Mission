using UnityEngine.Events;

public static class Actions
{
    public static UnityAction<Bot> KillBot;
    public static UnityAction HPChange;
    public static UnityAction GoIntotTheGate;
    public static UnityAction<int> PickGoldChess;
    public static UnityAction PickLifeChess;
    public static UnityAction<int> PickAmmoChess;
    public static UnityAction<int> ChangeWeapon;
    public static UnityAction<int> OnChangeGold;
    public static UnityAction Fire;
    public static CounterTime counterTime = new CounterTime();
    public static void Start(IAction action,float time)
    {
        counterTime.Start(action,time);
    }
}
