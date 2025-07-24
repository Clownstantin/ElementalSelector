using ElementalSelector.Enums;
using ElementalSelector.Views;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementConfig", menuName = "Configs/ElementConfig")]
public class ElementConfig : ScriptableObject
{
	[field: SerializeField] public ElementPrefabEntry[] Elements { get; private set; }

	public ElementView GetPrefab(ElementType type)
	{
		foreach(var entry in Elements)
			if(entry.type == type)
				return entry.prefab;

		return null;
	}

	[System.Serializable]
	public struct ElementPrefabEntry
	{
		public ElementType type;
		public ElementView prefab;
	}
}