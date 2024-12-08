using QuickOutline;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ObjectHighlighter : MonoBehaviour
{
    private float _deselectValue;
    private float _selectValue;

    private Outline Outline;
        
    public void Initialize(float deselectValue = 0f, float selectValue = 4f)
    {
        Outline = GetComponent<Outline>();
        _deselectValue = deselectValue;
        _selectValue = selectValue;
        Outline.Initialize();
    }

    public void Select()
    {
        Outline.OutlineWidth = _selectValue;
    }

    public void Deselect()
    {
        Outline.OutlineWidth = _deselectValue;
    }
}