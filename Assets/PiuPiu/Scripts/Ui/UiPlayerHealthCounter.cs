using JetBrains.Annotations;
using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Player;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace PiuPiu.Scripts.Ui
{
    public class UiPlayerHealthCounter : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private Image hpImage;
        
        private EntityQuery _playerLiveCounterQuery;

        private void LateUpdate()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_playerLiveCounterQuery) &&
                !_playerLiveCounterQuery.IsEmpty)
            {
                var entity = _playerLiveCounterQuery.GetSingletonEntity();

                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var data = entityManager.GetComponentData<HealthData>(entity);
                if (hpImage != null) 
                    hpImage.fillAmount = (float)data.health / data.maxHealth;
            }
        }
        
        void Start()
        {
            _playerLiveCounterQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(
                new EntityQueryBuilder(Allocator.Temp)
                    .WithAll<HealthData>().WithAny<PlayerData>());
        }
        
        void OnDestroy()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_playerLiveCounterQuery))
                _playerLiveCounterQuery.Dispose();
        }
    }
}
