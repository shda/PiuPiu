using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Player;
using TMPro;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace PiuPiu.Scripts.Ui
{
    public class UiEntityCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        
        private EntityQuery _entityQuery;
        
        private void LateUpdate()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_entityQuery) &&
                !_entityQuery.IsEmpty)
            {
                var count = _entityQuery.CalculateEntityCount();
                
                countText.text = $"Entity {count}";
            }
        }
        
        void Start()
        {
            _entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(
                new EntityQueryBuilder(Allocator.Temp)
                    .WithAll<LocalToWorld>());
        }
        
        void OnDestroy()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_entityQuery))
                _entityQuery.Dispose();
        }
    }
}