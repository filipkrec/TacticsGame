using UnityEngine;

public class TerrainPart : MonoBehaviour
{
    public int PosX;
    public int PosY;

    [SerializeField] private Material MaterialMove;
    [SerializeField] private Material MaterialAttack;
    [SerializeField] private Material MaterialTarget;
    [SerializeField] private Material MaterialEmpty;
    [SerializeField] private Material MaterialBasicOutline;
    [SerializeField] private MeshRenderer Renderer;

    public void SetHighlightMove(bool _isOn)
    {
        if (_isOn)
        {
            SetMaterial(2, MaterialMove);
        }
        else
        {
            SetMaterial(2, MaterialEmpty);
        }
    }

    public void SetHighlightAttack(bool _isOn)
    {
        if (_isOn)
        {
            SetMaterial(2, MaterialAttack);
        }
        else
        {
            SetMaterial(2, MaterialEmpty);
        }
    }

    public void SetTarget(bool _isOn)
    {
        if (_isOn)
        {
            SetMaterial(1, MaterialTarget);
        }
        else
        {
            SetMaterial(1, MaterialBasicOutline);
        }
    }

    private void SetMaterial(int _index, Material _material)
    {
        Material[] materials = Renderer.materials;
        materials[_index] = _material;
        Renderer.materials = materials;
    }
}
