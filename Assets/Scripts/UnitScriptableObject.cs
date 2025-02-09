using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "Scriptable Objects/Unit")]
public class UnitScriptableObject : ScriptableObject
{
    public string Name;
    public int MaxHP;
    public int Speed;
    public int MaxMP;
    public List<AttackScriptableObject> Attacks;
}
