using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance => m_instance;
    private static Board m_instance;

    [SerializeField] private Terrain m_terrain;
    [SerializeField] private List<Unit> m_units;

    private void Awake()
    {
        m_instance = this;
    }

    public void RefreshTerrain()
    {
        m_terrain.Refresh();
    }

    public Unit SelectUnit(TerrainPart _part)
    {
        return m_units.FirstOrDefault(x => x.PosX == _part.PosX && x.PosY == _part.PosY);
    }

    public void HighlightMove(Unit _selectedUnit)
    {
        List<TerrainPart> toHighlight = GetPartsInRadius(_selectedUnit.PosX, _selectedUnit.PosY, _selectedUnit.MoveRadius);
        List<TerrainPart> takenSlots = GetTakenSlots(toHighlight);
        toHighlight.RemoveAll(x => takenSlots.Contains(x));
        foreach (TerrainPart part in toHighlight)
        {
            part.SetHighlightMove(true);
        }
    }

    public List<TerrainPart> GetPartsInRadius(int _posX, int _posY, int _radius)
    {
        TerrainPart current = m_terrain.GetPart(_posX, _posY);

        List<TerrainPart> partsInRadius = new List<TerrainPart>();

        int nextRadius = _radius - 1;

        if (nextRadius != 0)
        {
            foreach (TerrainPart neighbour in m_terrain.GetNeighbours(_posX, _posY))
            {
                partsInRadius.AddRange(GetPartsInRadius(neighbour.PosX, neighbour.PosY, nextRadius));
            }
        }

        partsInRadius.RemoveAll(x => x.PosX == _posX && x.PosY == _posY);
        partsInRadius.Add(current);

        return partsInRadius;
    }

    private List<TerrainPart> GetTakenSlots(List<TerrainPart> _partsToCheck)
    {
        return _partsToCheck.FindAll(x => m_units.Exists((y => y.PosX == x.PosX && y.PosY == x.PosY)));
    }

    public bool IsSlotTaken(TerrainPart _part)
    {
        return m_units.Exists(x => _part.PosX == x.PosX && _part.PosY == x.PosY);
    }

    public bool IsInRadius(int _posX, int _posY, int _radX, int _radY, int _radius)
    {
        List<TerrainPart> inRadius = GetPartsInRadius(_radX, _radY, _radius);
        return inRadius.Exists(x => x.PosX == _posX && x.PosY == _posY);
    }
}
