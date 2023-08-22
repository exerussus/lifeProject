
using Leopotam.EcsLite;
using src.Scripts.Data;
using src.Scripts.Systems;
using UnityEngine;

namespace src.Scripts.MonoBehaviours
{
    public class Starter : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        [SerializeField] private GameData gameData;
        
        private void Start() 
        {        
            _world = new EcsWorld();

            PrepareInitSystems();
            PrepareUpdateSystems();
            PrepareFixedUpdateSystems();
        }

        private void Update() 
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, gameData);
            _initSystems.Add(new CreatingPlayerSystem());
            _initSystems.Init();
        }
        
        private void PrepareUpdateSystems()
        {
            _updateSystems = new EcsSystems(_world, gameData);
            _updateSystems.Init();
        }
        
        private void PrepareFixedUpdateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world, gameData);
            _fixedUpdateSystems
                
                .Add(new  MovementSystem());
            
            _fixedUpdateSystems.Init();
        }
        
        private void OnDestroy() 
        {
            _initSystems.Destroy();
        }
    }
}
