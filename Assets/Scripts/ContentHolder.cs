using UnityEngine;

/// <summary>
/// Shows stored content on press
/// </summary>
public class ContentHolder : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject content;
    [SerializeField] Transform contentParent;

    private void OnMouseUp()
    {
        indicator.SetActive(false);
        bool wasActive = content.activeInHierarchy;
        foreach (Transform child in contentParent)
        {
            child.gameObject.SetActive(false);
        }
        content.SetActive(!wasActive);
    }
}
