using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage), typeof(VideoPlayer), typeof(AudioSource))]
public class VideoPlayerOnUGUI : MonoBehaviour
{
    RawImage image;
    VideoPlayer player;
    void Awake()
    {
        image = GetComponent<RawImage>();
        player = GetComponent<VideoPlayer>();
        var source = GetComponent<AudioSource>();
        player.EnableAudioTrack(0, true);
        player.SetTargetAudioSource(0, source);
    }
    void Update()
    {
        if (player.isPrepared)
        {
            image.texture = player.texture;
        }
    }
}