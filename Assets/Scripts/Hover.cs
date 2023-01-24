using UnityEngine.EventSystems;
using UnityEngine;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 highlightedScale;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        highlightedScale = new Vector3(originalScale.x + 0.01f, originalScale.y + 0.01f, originalScale.z + 0.01f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = highlightedScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}