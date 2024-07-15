using UnityEngine;
using UnityEngine.Video;

public class VideoContainer : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;

    VideoPlayer player;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void OnMouseUp()
    {
        if (player.isPlaying)
        {
            player.Pause();
            PlayButton.SetActive(true);
        }
        else
        {
            player.Play();
            PlayButton.SetActive(false);
        }
    }
}
