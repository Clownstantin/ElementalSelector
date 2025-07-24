using System.Collections;
using ElementalSelector.Core.EventBus;
using ElementalSelector.Core.Factory;
using ElementalSelector.Enums;
using UnityEngine;
using Zenject;

public class ElementSpawner : MonoBehaviour
{
	private IShapeSpawnSettings _settings;
	private ElementFactory _elementFactory;
	private EventBus _eventBus;

	[Inject]
	public void Construct(IShapeSpawnSettings settings, ElementFactory shapeFactory, EventBus eventBus)
	{
		_settings = settings;
		_elementFactory = shapeFactory;
		_eventBus = eventBus;
	}

	private void Start()
	{
		StartCoroutine(SpawnRoutine());
		_eventBus.Subscribe<LoseEvent>(OnLose);
	}

	private IEnumerator SpawnRoutine()
	{
		float lineSpacing = _settings.LineSpacing;
		float y = lineSpacing;

		while(true)
			for(int i = 0; i < 3; i++)
			{
				var type = (ElementType)Random.Range(0, 2);
				float speed = Random.Range(_settings.ElementsSpeed.x, _settings.ElementsSpeed.y);
				Vector3 position = new(transform.position.x, y);
				_elementFactory.Create(type, position, transform, speed);

				_eventBus.Publish(new ElementSpawnedEvent(type, position));

				float delay = Random.Range(_settings.ElementsDelay.x, _settings.ElementsDelay.y);
				yield return new WaitForSeconds(delay);
				y = y < 0 ? lineSpacing : y - lineSpacing;
			}
	}

	private void OnLose(LoseEvent _) => StopCoroutine(SpawnRoutine());
}