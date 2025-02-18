using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitScriptableObject m_config;

    public int PosX;
    public int PosY;

    public string Name => m_config.Name;
    public int MoveRadius => m_config.Speed;
    public List<AttackScriptableObject> Attacks => m_config.Attacks;

    private int m_currentHP;
    private int m_currentMP;

    private void Awake()
    {
        m_currentHP = m_config.MaxHP;
        m_currentMP = m_config.MaxMP;
    }

    public void Move(TerrainPart _part)
    {
        transform.position = _part.transform.position;
        PosX = _part.PosX;
        PosY = _part.PosY;
    }

    public void Damage(int _damage)
    {
        m_currentHP -= _damage;
        if(m_currentHP <= 0)
        {
            Die();
        }
    }

    public void InitiateMovement()
    {
        Board.Instance.HighlightMove(this);
    }

    public void InitiateAttack(AttackScriptableObject _attack)
    {
        Board.Instance.HighlightAttackRange(this, _attack);
    }

    public bool TryMove(TerrainPart _target)
    {
        if (!Board.Instance.IsInRadius(_target.PosX, _target.PosY, PosX, PosY, MoveRadius)
            || Board.Instance.IsSlotTaken(_target))
        {
            return false;
        }
        else
        {
            Move(_target);
            return true;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
