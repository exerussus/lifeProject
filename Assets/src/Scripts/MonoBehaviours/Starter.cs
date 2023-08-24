
using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
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
        
#if UNITY_EDITOR
        IEcsSystems _editorSystems;
#endif

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
#if UNITY_EDITOR
            // Выполняем обновление состояния отладочных систем. 
            _editorSystems?.Run ();
#endif
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, gameData);
#if UNITY_EDITOR
            _editorSystems = new EcsSystems (_initSystems.GetWorld ());
            _editorSystems
                .Add (new EcsWorldDebugSystem ())
                .Init ();
#endif
            _initSystems.Add(new CreatingPlayerSystem());
#if UNITY_EDITOR
            _initSystems.Add(new EcsWorldDebugSystem());
#endif
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
                .Add(new MovementSystem())
                .Add(new HerbivoreHungerBehaviourSystem())
                .Add(new DeterminantSystem())
                .Add(new VisionSystem());
            _fixedUpdateSystems.Init();
        }
        
        private void OnDestroy() 
        {
#if UNITY_EDITOR
            if (_editorSystems != null) {
                _editorSystems.Destroy ();
                _editorSystems = null;
            }
#endif
            _initSystems.Destroy();
        }
    }
}
