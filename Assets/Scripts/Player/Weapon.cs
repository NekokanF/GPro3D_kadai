using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public float FireBaseRate;
    public float FireCurrentRate;

    public float BaseReloadTime;
    public float CurrentReloadTime;

    public int BaseMagazineAmount;
    public int CurrentMagazineAmount;

    public int BaseBulletAmount;
    public int CurrentBulletAmount;

    public int Damage;
}
