using UnityEngine;

[CreateAssetMenu(fileName = "TerrainGenerator", menuName = "Scriptables/TerrainGenerator")]
public class TerrainGenerator : ScriptableObject
{
    public Terrain TerrainParentPrefab;
    public TerrainPart TerrainPrefab;
    public float Offset;
    public int SizeX;
    public int SizeY;

    public static GameObject InstantiatedTerrain;

    [ContextMenu("Generate!")]
    public void GenerateTerrain()
    {
        DestroyImmediate(InstantiatedTerrain);

        Terrain parent = Instantiate(TerrainParentPrefab);

        parent.Instantiate(SizeX, SizeY);

        for (int x = 0; x < SizeX; ++x)
        {
            for (int y = 0; y < SizeY; ++y)
            {
                TerrainPart terrain = Instantiate(TerrainPrefab);
                terrain.transform.position = new Vector3(Offset * x, 0f, Offset * y);
                terrain.transform.SetParent(parent.transform);
                parent.SetPart(x, y, terrain);
            }
        }
    }
}
