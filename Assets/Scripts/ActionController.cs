using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_name;
    [SerializeField] private Button m_moveButton;
    [SerializeField] private Button m_attacksToggle;
    [SerializeField] private GameObject m_attacksParent;
    [SerializeField] private AttackButtonView m_attackButtonPrefab;

    private List<AttackButtonView> m_initializedButtons = new List<AttackButtonView>();

    private Unit m_currentUnit;

    public Action EndTurnAction;
    private Action<TerrainPart> CurrentSelectAction;
    private Action<TerrainPart> CurrentHoverAction;

    private void Awake()
    {
        m_moveButton.onClick.AddListener(InitiateMove);
        m_attacksToggle.onClick.AddListener(ToggleAttacks);
    }

    public void SetUnit(Unit _unit)
    {
        m_currentUnit = _unit;
        m_name.text = m_currentUnit.Name;
    }

    private void InitiateMove()
    {
        Board.Instance.RefreshTerrain();
        HideAttacks();
        m_currentUnit.InitiateMovement();
        CurrentSelectAction = Move;
        CurrentHoverAction = null;
    }

    private void InitiateAttack(AttackScriptableObject _attack)
    {
        Board.Instance.RefreshTerrain();
        m_currentUnit.InitiateAttack(_attack);
        CurrentSelectAction = Attack;
        CurrentHoverAction = (x) => ShowAttackArea(_attack,x);
    }

    private void ShowAttackArea(AttackScriptableObject _attack, TerrainPart _terrainPart)
    {
        Board.Instance.HighlightAttackArea(_terrainPart.PosX, _terrainPart.PosY, _attack);
    }

    public void HoverTerrain(TerrainPart _targetPart)
    {
        CurrentHoverAction?.Invoke(_targetPart);
    }

    private void Move(TerrainPart _part)
    {
        if (m_currentUnit.TryMove(_part))
        {
            EndTurnAction?.Invoke();
            CurrentHoverAction = null;
            CurrentSelectAction = null;
        }
    }

    private void Attack(TerrainPart _part)
    {
        if (true) //TODO
        {
            EndTurnAction?.Invoke();
            CurrentHoverAction = null;
            CurrentSelectAction = null;
        }
    }

    public void TargetTerrain(TerrainPart _target)
    {
        CurrentSelectAction?.Invoke(_target);
    }

    private void ToggleAttacks()
    {
        if(m_attacksParent.activeSelf)
        {
            HideAttacks();
        }
        else
        {
            ShowAttacks();
        }
    }

    private void ShowAttacks()
    {
        m_attacksParent.SetActive(true);
        foreach(AttackScriptableObject attack in m_currentUnit.Attacks)
        {
            AttackButtonView newButton = Instantiate(m_attackButtonPrefab, m_attacksParent.transform);
            newButton.Button.onClick.AddListener(() => InitiateAttack(attack));
            newButton.Name.text = attack.Name;
            newButton.MP.text = attack.MP.ToString();
            m_initializedButtons.Add(newButton);
        }
    }

    private void HideAttacks()
    {
        m_attacksParent.SetActive(false);
        foreach(AttackButtonView button in m_initializedButtons)
        {
            Destroy(button.gameObject);
        }
        m_initializedButtons.Clear();
    }
}
