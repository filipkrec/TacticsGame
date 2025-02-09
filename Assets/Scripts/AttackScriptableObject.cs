using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/Attack")]
public class AttackScriptableObject : ScriptableObject
{
    public int Range;
    public int Damage;
    public int MP;
    public int Radius;
}
