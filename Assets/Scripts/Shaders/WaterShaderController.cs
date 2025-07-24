using UnityEngine;

[ExecuteAlways]
public class WaterShaderController : MonoBehaviour
{
	[SerializeField] private Material _targetMat;

	private Vector2 _resolution;

	void Update()
	{
		if(_targetMat == null) return;

		// Use Screen.width/height for runtime, 1024 for editor preview
		_resolution.x = Application.isPlaying ? Screen.width : 1024;
		_resolution.y = Application.isPlaying ? Screen.height : 768;

		_targetMat.SetVector("_iResolution", _resolution);
		_targetMat.SetFloat("_iTime", Time.time);

		if(_targetMat.IsKeywordEnabled("SHOW_TILING") !=
			(_targetMat.GetInt("_ShowTiling") == 1))
		{
			if(_targetMat.GetInt("_ShowTiling") == 1)
				_targetMat.EnableKeyword("SHOW_TILING");
			else
				_targetMat.DisableKeyword("SHOW_TILING");
		}
	}
}