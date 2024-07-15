using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ARScenes targetScene;

    bool pressing = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressing = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        pressing = false;
        ARManager.Instance.CancelSceneOpening();
    }

    void Update()
    {
        if (pressing)
        {
            ARManager.Instance.OpenScene(targetScene);
        }
    }

}

