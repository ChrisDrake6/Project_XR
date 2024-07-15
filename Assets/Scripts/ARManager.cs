using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    public static ARManager Instance { get; private set; }

    [SerializeField] TMP_Text scaleFactorLabel;
    [SerializeField] TMP_Text distanceLabel;
    [SerializeField] Slider scaleSlider;
    [SerializeField] Transform imageTarget;
    [SerializeField] string webUrl;

    [SerializeField] GameObject pressIndicator;
    [SerializeField] Image pressIndicatorIconContainer;
    [SerializeField] Image pressIndicatorFillMeter;
    [SerializeField] float holdDuration;

    [SerializeField] GameObject infoScene;
    [SerializeField] GameObject tropicalScene;
    [SerializeField] GameObject videoScene;
    [SerializeField] GameObject divingHelmetScene;
    [SerializeField] GameObject listScene;
    [SerializeField] GameObject webScene;

    [SerializeField] Sprite infoSprite;
    [SerializeField] Sprite tropicalSprite;
    [SerializeField] Sprite videoSprite;
    [SerializeField] Sprite divingHelmetSprite;
    [SerializeField] Sprite listSprite;
    [SerializeField] Sprite webSprite;

    GameObject currentSceneObject;
    ARScenes currentScene;
    float currentScaleFactor;
    float timePressed;

    public ARManager()
    {
        Instance = this;
    }

    void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, imageTarget.position);
        distanceLabel.text = "Distanz: " + string.Format("{0:0.00}", distance / Vuforia.VuforiaConfiguration.Instance.Vuforia.VirtualSceneScaleFactor) + " m";
    }

    public void SetScale(float scaleFactor)
    {
        if (currentSceneObject != null)
        {
            currentSceneObject.transform.localScale = Vector3.one * scaleFactor;
        }

        scaleFactorLabel.text = "Vergrößerung: " + String.Format("{0:0.00}", scaleFactor) + ": 1";
    }

    public void OpenWebsite()
    {
        Application.OpenURL(webUrl);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void OpenScene(ARScenes targetScene)
    {
        if (targetScene != currentScene)
        {
            if (timePressed >= holdDuration)
            {
                pressIndicator.SetActive(false);
                if (currentSceneObject != null)
                {
                    currentSceneObject.SetActive(false);
                }
                scaleSlider.gameObject.SetActive(true);
                scaleFactorLabel.gameObject.SetActive(true);
                SetScale(1);
                scaleSlider.value = currentScaleFactor;
                currentScene = targetScene;
                timePressed = 0;

                switch (currentScene)
                {
                    case ARScenes.Info:
                        currentSceneObject = infoScene;
                        break;
                    case ARScenes.Tropical:
                        currentSceneObject = tropicalScene;
                        break;
                    case ARScenes.Video:
                        currentSceneObject = videoScene;
                        break;
                    case ARScenes.DivingHelmet:
                        currentSceneObject = divingHelmetScene;
                        break;
                    case ARScenes.List:
                        currentSceneObject = listScene;
                        break;
                    case ARScenes.Web:
                        currentSceneObject = webScene;
                        break;
                }

                currentSceneObject.SetActive(true);
            }
            else
            {
                pressIndicator.SetActive(true);
                timePressed += Time.deltaTime;
                pressIndicatorFillMeter.fillAmount = timePressed / holdDuration;

                switch (targetScene)
                {
                    case ARScenes.Info:
                        pressIndicatorIconContainer.sprite = infoSprite;
                        break;
                    case ARScenes.Tropical:
                        pressIndicatorIconContainer.sprite = tropicalSprite;
                        break;
                    case ARScenes.Video:
                        pressIndicatorIconContainer.sprite = videoSprite;
                        break;
                    case ARScenes.DivingHelmet:
                        pressIndicatorIconContainer.sprite = divingHelmetSprite;
                        break;
                    case ARScenes.List:
                        pressIndicatorIconContainer.sprite = listSprite;
                        break;
                    case ARScenes.Web:
                        pressIndicatorIconContainer.sprite = webSprite;
                        break;
                }


            }
        }
    }

    public void CancelSceneOpening()
    {
        pressIndicator.SetActive(false);
        timePressed = 0;
    }
}

public enum ARScenes
{
    NoScene,
    Info,
    Tropical,
    Video,
    DivingHelmet,
    List,
    Web
}