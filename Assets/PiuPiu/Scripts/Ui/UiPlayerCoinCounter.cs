using JetBrains.Annotations;
using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Player;
using TMPro;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ui
{
    public class UiPlayerCoinCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinCountText;
        
        private EntityQuery _playerCountCoinEntityQuery;

        private void LateUpdate()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_playerCountCoinEntityQuery) &&
                !_playerCountCoinEntityQuery.IsEmpty)
            {
                var entity = _playerCountCoinEntityQuery.GetSingletonEntity();

                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var data = entityManager.GetComponentData<CoinCountData>(entity);

                coinCountText.text = $"{data.coinCount}";
            }
        }
        
        void Start()
        {
            _playerCountCoinEntityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(
                new EntityQueryBuilder(Allocator.Temp)
                    .WithAll<CoinCountData>().WithAny<PlayerData>());
        }
        
        void OnDestroy()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_playerCountCoinEntityQuery))
                _playerCountCoinEntityQuery.Dispose();
        }
    }
}