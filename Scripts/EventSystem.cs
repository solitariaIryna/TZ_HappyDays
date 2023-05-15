using System;


public static class EventSystem 
{
    public static Action<AnimationPlayer> OnChangeAnimation;
    public static Action<SpawnerAmmo> OnDetectAmmo;
    public static Action OnAddedAmmo;

    public static Action OnPutOnBox;

    public static Action OnPutOnMachine;

    public static Action OnCreatedRocket;

    public static Action<SpawnerRocket> OnDetectRocket;
    public static Action OnAddedRocket;

    public static Action OnRemoveRocket;
}
