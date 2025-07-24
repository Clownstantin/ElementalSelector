using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Configs/GameSettings")]
public class GameSettings : ScriptableObject, IShapeSpawnSettings
{
	[field: SerializeField] public Vector2Int ElementsCount { get; private set; }
	[field: SerializeField] public Vector2 ElementsSpeed { get; private set; }
	[field: SerializeField] public Vector2 ElementsDelay { get; private set; }
	[field: SerializeField] public float LineSpacing { get; private set; }
	[field: SerializeField] public int PlayerHealth { get; private set; }
}
