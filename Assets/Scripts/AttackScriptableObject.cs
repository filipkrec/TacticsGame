using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/Attack")]
public class AttackScriptableObject : ScriptableObject
{
    public string Name;
    public int Range;
    public int DamageMin;
    public int DamageMax;
    public int MP;
    public int Radius;
}
