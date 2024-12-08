using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GroupSwitcher : MonoBehaviour
{
    private CanvasGroup _group;

    public bool Interactable => _group.interactable;
    
    public void Initialize()
    {
        _group = GetComponent<CanvasGroup>();
    }
    
    public void Show()
    {
        _group.alpha = (float)ValueConstants.One;
        _group.interactable = true;
        _group.blocksRaycasts = true;
    }

    public void Hide()
    {
        _group.alpha = (float)ValueConstants.Zero;
        _group.interactable = false;
        _group.blocksRaycasts = false;
    }
}