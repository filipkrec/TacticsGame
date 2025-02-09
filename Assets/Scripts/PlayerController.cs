using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<Unit> m_units;
    [SerializeField] private ActionController m_actionController;

    private TerrainPart m_targetPart;
    public List<Unit> Units => m_units;

    private void Start()
    {
        m_actionController.EndTurnAction += Board.Instance.RefreshTerrain;
    }

    public bool TrySetUnitTurn(Unit _unit)
    {
        if (m_units.Contains(_unit))
        {
            m_actionController.SetUnit(_unit);
            return true;
        }
        return false;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (m_targetPart != null) m_targetPart.SetTarget(false);

            m_targetPart = hit.transform.GetComponent<TerrainPart>();

            m_targetPart.SetTarget(true);
        }

        if (Input.GetMouseButtonDown(1) && m_targetPart != null)
        {
            m_actionController.TargetTerrain(m_targetPart);
        }
    }
}
