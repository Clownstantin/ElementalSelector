using UnityEngine;

public interface IShapeSpawnSettings
{
	float LineSpacing { get; }
	Vector2Int ElementsCount { get; }
	Vector2 ElementsSpeed { get; }
	Vector2 ElementsDelay { get; }
}