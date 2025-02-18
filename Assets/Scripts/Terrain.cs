using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public List<TerrainPart> m_terrainParts;

    public int m_sizeX;
    public int m_sizeY;

    public void Instantiate(int _sizeX, int _sizeY)
    {
        m_sizeX = _sizeX;
        m_sizeY = _sizeY;

        m_terrainParts = new List<TerrainPart>();
        for (int x = 0; x < m_sizeX; x++)
        {
            for (int y = 0; y < m_sizeY; y++)
            {
                m_terrainParts.Add(null);
            }
        }
    }

    public void SetPart(int _x, int _y, TerrainPart _part)
    {
        m_terrainParts[(_x * m_sizeY) + _y] = _part;
        _part.PosX = _x;
        _part.PosY = _y;
    }

    public TerrainPart GetPart(int _x, int _y)
    {
        return m_terrainParts[(_x * m_sizeY) + _y];
    }

    public List<TerrainPart> GetNeighbours(int _x, int _y)
    {
        List<TerrainPart> neighbours = new List<TerrainPart>();

        Vector2Int up = new Vector2Int(_x, _y + 1);
        Vector2Int down = new Vector2Int(_x, _y - 1);
        Vector2Int right = new Vector2Int(_x + 1, _y);
        Vector2Int left = new Vector2Int(_x - 1, _y);

        if (up.y < m_sizeY) neighbours.Add(GetPart(up.x, up.y));
        if (down.y >= 0) neighbours.Add(GetPart(down.x, down.y));
        if (left.x >= 0) neighbours.Add(GetPart(left.x, left.y));
        if (right.x < m_sizeX) neighbours.Add(GetPart(right.x, right.y));

        return neighbours;
    }

    public void Refresh()
    {
        foreach (TerrainPart part in m_terrainParts)
        {
            part.SetHighlightMove(false);
            part.SetHighlightAttackArea(false);
            part.SetHighlightAttackRange(false);
        }
    }
}
