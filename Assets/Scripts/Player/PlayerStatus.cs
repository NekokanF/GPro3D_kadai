using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public float BaseSpeed;
    public float CurrentSpeed;

    public int BaseHP;
    public int CurrentHP;
}
