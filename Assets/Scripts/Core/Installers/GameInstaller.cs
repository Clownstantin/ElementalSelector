using ElementalSelector.Core.Factory;
using UnityEngine;
using Zenject;

namespace ElementalSelector.Core.Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private GameSettings _gameSettings;
		[SerializeField] private ElementConfig _shapeConfig;

		public override void InstallBindings()
		{
			Container.Bind<EventBus.EventBus>().AsSingle();
			Container.Bind<ElementFactory>().AsSingle();
			Container.Bind<IShapeSpawnSettings>().FromInstance(_gameSettings).AsSingle();
			Container.Bind<ElementConfig>().FromInstance(_shapeConfig).AsSingle();
		}
	}
}
