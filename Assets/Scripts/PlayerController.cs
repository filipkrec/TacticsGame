using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Board m_board;

    private TerrainPart m_targetPart;
    private Unit m_selectedUnit;
    private List<TerrainPart> m_availableParts;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) 
        {
            if(m_targetPart != null) m_targetPart.SetTarget(false);

            m_targetPart = hit.transform.GetComponent<TerrainPart>();

            m_targetPart.SetTarget(true);
        }

        if(Input.GetMouseButtonDown(0) && m_selectedUnit == null)
        {
            m_selectedUnit = m_board.SelectUnit(m_targetPart);
            if(m_selectedUnit != null) m_availableParts = m_board.InitiateMove(m_selectedUnit);
        }
        else if (Input.GetMouseButtonDown(0) && m_availableParts != null && m_availableParts.Contains(m_targetPart))
        {
            m_selectedUnit.Move(m_targetPart);
            m_selectedUnit = null;
            m_availableParts = null;

            m_board.RefreshTerrain();
        }
    }
}
