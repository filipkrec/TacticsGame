using System.Collections.Generic;
using UnityEngine;

public class UnitQueue : MonoBehaviour
{
    [SerializeField] private List<Unit> m_units;
    [SerializeField] private List<PlayerController> m_playerControllers;
    private int m_currentUnit;

    private void Awake()
    {
        m_currentUnit = -1;
    }

    private void Start()
    {
        NextTurn();
    }

    private void NextTurn()
    {
        m_currentUnit++;
        m_currentUnit %= m_units.Count;
        foreach (PlayerController playerController in m_playerControllers)
        {
            if (playerController.TrySetUnitTurn(m_units[m_currentUnit]))
            {
                break;
            }
        }
    }
}
