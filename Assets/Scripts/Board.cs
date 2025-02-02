using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Terrain m_terrain;
    [SerializeField] private List<Unit> m_units;

    public void RefreshTerrain()
    {
        m_terrain.Refresh();
    }

    public Unit SelectUnit(TerrainPart _part)
    {
        return m_units.FirstOrDefault(x => x.PosX == _part.PosX && x.PosY == _part.PosY);
    }

    public List<TerrainPart> InitiateMove(Unit _selectedUnit)
    {
        List<TerrainPart> toReturn = GetPartsInRadius(_selectedUnit.PosX, _selectedUnit.PosY, _selectedUnit.MoveRadius);
        foreach (TerrainPart part in toReturn)
        {
            part.SetHighlightMove(true);
        }

        return toReturn;
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
}
