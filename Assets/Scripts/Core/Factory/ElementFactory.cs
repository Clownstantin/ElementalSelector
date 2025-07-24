using ElementalSelector.Enums;
using ElementalSelector.Views;
using UnityEngine;
using Zenject;

namespace ElementalSelector.Core.Factory
{
	public class ElementFactory
	{
		private readonly DiContainer _container;
		private readonly ElementConfig _elementConfig;

		public ElementFactory(DiContainer container, ElementConfig elementConfig)
		{
			_container = container;
			_elementConfig = elementConfig;
		}

		public ElementView Create(ElementType type, Vector3 position, Transform parent, float speed)
		{
			ElementView elementView = _elementConfig.GetPrefab(type);
			if(elementView == null)
			{
				Debug.LogError($"No prefab found for {type}");
				return null;
			}

			elementView = _container.InstantiatePrefabForComponent<ElementView>(elementView, position, Quaternion.identity, parent, new object[] { type, speed });
			return elementView;
		}
	}
}