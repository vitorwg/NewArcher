using UnityEngine;


[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject
{
    public int health;
    public int damage;
    public int speed;
    public int countdownAttack;
    public int dropRateArrow;
}
