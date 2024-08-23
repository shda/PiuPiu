Соберу всё что понял про ECS

Сущности которые должны создаваться bз объектов юнити должны быть в отдельной субсцене юнити

![alt text](img/image.png)

Игровой объект который должен преобразовывайся в сущность ecs, должен содержать в себе класс Baker
```
public class PlayerAuthoring : MonoBehaviour
{
        [SerializeField] private float speedMoving;
        
        class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterMovingData
                {
                    MovingDirection = new float3(0,0,0),
                    Speed = authoring.speedMoving,
                });
                
                AddComponent(entity , new PlayerData());
                AddComponent(entity , new InputData());
                AddComponent(entity , new PlayerRotateMouseData());
                AddComponent(entity , new PlayerCameraData());
            }
        }
}
```

* Если с сущности есть компонент InputData то будет вызываться система PlayerFireSystem
```
public partial struct PlayerFireSystem : ISystem
{
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //InputData должен быть в одиночном экземпляре и только у одного Entity
            var input = SystemAPI.GetSingleton<InputData>();

            foreach (var bulletSpawner  in SystemAPI.Query<RefRW<BulletSpawnerData>>())
            {
                bulletSpawner.ValueRW.isFireing = input.Space;
            }
        }
}
```

* SystemAPI.Query запрашивает список ссылок на нужные компоненты, по которому можно пройтись в цикле. Можно добавить WithEntityAccess() и в цикле получить доступ к данным сущности.
```SystemAPI.Query<RefRW<BulletSpawnerData>>().WithEntityAccess()```

* Удалять или добавлять компоненты или уничтожать сущности нужно через EntityCommandBuffer
```
[BurstCompile]
public void OnUpdate(ref SystemState state)
{
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
        foreach (var (destroyComponentData, entity) in
                     SystemAPI.Query<RefRO<DestroyComponentData>>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }
            
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
}
```

* Создавать новые сущности из префабов юнити:
```
public class SpawnerEnemyAuthoring : MonoBehaviour
{
        public GameObject Prefab;
        
        class Baker : Baker<SpawnerEnemyAuthoring>
        {
            public override void Bake(SpawnerEnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Spawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
}

public void OnUpdate(ref SystemState state)
{
    -------------------
    var prefab = SystemAPI.GetSingleton<Spawner>().Prefab;
    var instances = state.EntityManager.Instantiate(prefab, 50, Allocator.Temp);
    -------------------
}

         
struct Spawner : IComponentData
{
    public Entity Prefab;
}
``` 

 * Коллизии между объектов можно проверять так:
В проекте есть папка Stateful в ней системы и компоненты которые добавляют StatefulTriggerEvent или StatefulCollisionEvent к сущности у которого произошла коллизия или сработал триггер.
Например, к сущности подключили StatefulCollisionEventBufferAuthoring, когда это сущность пресекла другую у этой сущности в компонент StatefulTriggerEvent в списке добавилась новая сущность. После чего можно обрабатывать эти коллизии

пример снаряда
``` 
public void OnUpdate(ref SystemState state)
{
    -----------------------
	if (entityManager.HasComponent<StatefulTriggerEvent>(bulletEntity))
	{
		var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(bulletEntity);
		for (int i = 0; i < buffer.Length; i++)
		{
			var item = buffer[i];
	
			var hitEntity = item.EntityA;
			if (hitEntity == bulletEntity)
			{
				hitEntity = item.EntityB;
			}
	
			if (SystemAPI.HasComponent<HealthComponentData>(hitEntity))
			{
				var health = SystemAPI.GetComponentRW<HealthComponentData>(hitEntity);
				health.ValueRW.currentHealth -= bulletData.ValueRO.hitDamage;
				
				if (health.ValueRW.currentHealth <= 0)
				{
					ecb.AddComponent(hitEntity, new DestroyComponentData());
				}
				
				//Destroy bullet
				ecb.AddComponent(bulletEntity , new DestroyComponentData());
			}
		}
	}
    ---------------------
}
``` 