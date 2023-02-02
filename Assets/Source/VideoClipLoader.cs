using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoClipLoader : MonoBehaviour
{
	// Originally hosted here: https://github.com/Ilandria/Viewports/raw/main/Assets/StreamingAssets/Wrongwarp.mp4
	private VideoPlayer videoPlayer = null;

	public void Start()
	{
		videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.Prepare();
	}
}
