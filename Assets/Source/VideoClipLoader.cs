using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoClipLoader : MonoBehaviour
{
	// Originally hosted here: https://dl.dropboxusercontent.com/s/8uvbpwnrck1qhqq/Wrongwarp.mp4
	[SerializeField]
	private string videoUrl = string.Empty;

	private VideoPlayer videoPlayer = null;

	public void Start()
	{
		videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.url = videoUrl;
		videoPlayer.Prepare();
	}
}
