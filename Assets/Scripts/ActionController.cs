using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_name;
    [SerializeField] Button m_moveButton;

    private Unit m_currentUnit;

    public Action EndTurnAction;
    private Action<TerrainPart> CurrentAction;

    private void Awake()
    {
        m_moveButton.onClick.AddListener(InitiateMove);
    }

    public void SetUnit(Unit _unit)
    {
        m_currentUnit = _unit;
        m_name.text = m_currentUnit.Name;
    }

    private void InitiateMove()
    {
        m_currentUnit.InitiateMovement();
        CurrentAction = Move;
    }

    private void Move(TerrainPart _part)
    {
        if(m_currentUnit.TryMove(_part))
        {
            EndTurnAction?.Invoke();
        }
    }

    public void TargetTerrain(TerrainPart _target)
    {
        CurrentAction?.Invoke(_target);
    }
}
