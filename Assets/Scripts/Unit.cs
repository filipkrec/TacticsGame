using UnityEngine;

public class Unit : MonoBehaviour
{
    public int PosX;
    public int PosY;
    public int MoveRadius;

    public void Move(TerrainPart _part)
    {
        transform.position = _part.transform.position;
        PosX = _part.PosX;
        PosY = _part.PosY;
    }
}
