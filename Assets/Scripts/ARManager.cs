using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    public static ARManager Instance { get; private set; }

    [Header ("MainUIProperties")]
    [SerializeField] TMP_Text scaleFactorLabel;
    [SerializeField] TMP_Text distanceLabel;
    [SerializeField] Slider scaleSlider;
    [SerializeField] Transform imageTarget;

    [Header ("PressindicatorProperties")]
    [SerializeField] GameObject pressIndicator;
    [SerializeField] Image pressIndicatorIconContainer;
    [SerializeField] Image pressIndicatorFillMeter;
    [SerializeField] float holdDuration;

    [Header ("PressIndicatorIcons")]
    [SerializeField] Sprite infoSprite;
    [SerializeField] Sprite tropicalSprite;
    [SerializeField] Sprite videoSprite;
    [SerializeField] Sprite divingHelmetSprite;
    [SerializeField] Sprite listSprite;
    [SerializeField] Sprite webSprite;

    [Header ("AvailableScenes")]
    [SerializeField] GameObject infoScene;
    [SerializeField] GameObject tropicalScene;
    [SerializeField] GameObject videoScene;
    [SerializeField] GameObject divingHelmetScene;
    [SerializeField] GameObject listScene;
    [SerializeField] GameObject webScene;

    [Header ("Miscellaneous")]
    [SerializeField] string webUrl;

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
        // Show distance to ImageTarget
        float distance = Vector3.Distance(Camera.main.transform.position, imageTarget.position);
        distanceLabel.text = "Distanz: " + string.Format("{0:0.00}", distance / Vuforia.VuforiaConfiguration.Instance.Vuforia.VirtualSceneScaleFactor) + " m";
    }

    /// <summary>
    /// Scale scene with the slider
    /// </summary>
    /// <param name="scaleFactor"></param>
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

    /// <summary>
    /// While pressing a button, this function gets called OnUpdate:
    /// - Show press indication
    /// - Check if the button got pressed for given time, then open corresponding scene
    /// </summary>
    /// <param name="targetScene"></param>
    public void OpenScene(ARScenes targetScene)
    {
        // If target scene is already open, nothing needs to be done
        if (targetScene != currentScene)
        {
            // Check if button has been pressed long enough
            if (timePressed >= holdDuration)
            {
                // Hide press indicator
                pressIndicator.SetActive(false);

                // Close former scene
                if (currentSceneObject != null)
                {
                    currentSceneObject.SetActive(false);
                }

                // Enable scaling, if not yet available
                scaleSlider.gameObject.SetActive(true);
                scaleFactorLabel.gameObject.SetActive(true);
                SetScale(1);
                scaleSlider.value = currentScaleFactor;
                timePressed = 0;

                // Open target scene and save as current
                currentScene = targetScene;

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
                // Show press indicator and fill the meter
                pressIndicator.SetActive(true);
                timePressed += Time.deltaTime;
                pressIndicatorFillMeter.fillAmount = timePressed / holdDuration;

                // Show correct Icon
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