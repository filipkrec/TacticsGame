using UnityEngine;

public class TerrainPart : MonoBehaviour
{
    public int PosX;
    public int PosY;

    public bool CanMove;
    public bool CanAttack;

    [SerializeField] private Material MaterialMove;
    [SerializeField] private Material MaterialAttackRange;
    [SerializeField] private Material MaterialAttackArea;
    [SerializeField] private Material MaterialTarget;
    [SerializeField] private Material MaterialEmpty;
    [SerializeField] private Material MaterialBasicOutline;
    [SerializeField] private MeshRenderer Renderer;

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

    public void SetHighlightMove(bool _isOn)
    {
        CanMove = _isOn;

        if (_isOn)
        {
            SetMaterial(2, MaterialMove);
        }
        else
        {
            SetMaterial(2, MaterialEmpty);
        }
    }

    public void SetHighlightAttackRange(bool _isOn)
    {
        CanAttack = _isOn;

        if (_isOn)
        {
            SetMaterial(3, MaterialAttackRange);
        }
        else
        {
            SetMaterial(3, MaterialEmpty);
        }
    }

    public void SetHighlightAttackArea(bool _isOn)
    {
        if (_isOn)
        {
            SetMaterial(4, MaterialAttackArea);
        }
        else
        {
            SetMaterial(4, MaterialEmpty);
        }
    }

    private void SetMaterial(int _index, Material _material)
    {
        Material[] materials = Renderer.materials;
        materials[_index] = _material;
        Renderer.materials = materials;
    }
}
