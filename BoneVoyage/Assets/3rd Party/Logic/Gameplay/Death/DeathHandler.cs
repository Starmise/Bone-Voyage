using Infrastructure.Menus;
using CollisionSystem;
using StateMachines;
using Character;
using Ecs;

using UnityEngine;

namespace DeathSystem
{
    public class DeathHandler : IFixedTickable
    {
        private Filter<EnterCollisionEvent> _events = new();
        private Filter<CharacterMarker> _characters = new();
        private IStateMachine _stateMachine;

        public DeathHandler(IStateMachine stateMachine)
            => _stateMachine = stateMachine;

        public void FixedTick(float deltaTime)
        {
            foreach (var @event in _events.Components)
                Handle(@event);
        }

        private bool IsCharacter(GameObject gameObject)
        {
            foreach (var character in _characters.Components)
                if (gameObject == character.gameObject)
                    return true;

            return false;
        }

        private void Handle(EnterCollisionEvent @event)
        {
            Debug.Log("Colisión detectada con: " + @event.CollisionInfo.gameObject.name);

            if (!World.TryGetEntity(@event.CollisionInfo.gameObject, out var entity))
            {
                Debug.Log("No se encontró la entidad en el mundo.");
                return;
            }

            if (!entity.Contains<ObstacleMarker>())
            {
                if (@event.CollisionInfo.gameObject.CompareTag("Obstacle"))
                {
                    Debug.LogWarning("El obstáculo tiene la tag 'Obstacle' pero no tiene el componente ObstacleMarker. Se agregará ahora.");
                    entity.Add(new ObstacleMarker()); // Agregar componente manualmente
                }
                else
                {
                    Debug.Log("El objeto no es un obstáculo.");
                    return;
                }
            }

            if (!@event.Sender.Contains<DeathMarker>())
            {
                Debug.Log("El objeto que colisiona no tiene DeathMarker.");
                return;
            }

            if (IsCharacter(@event.Sender.GameObject))
            {
                CharacterDeath();
            }
            else
            {
                DefaultDeath(@event.Sender);
            }
        }


        private void CharacterDeath()
            => _stateMachine.SwitchTo<DefeatState>();

        private void DefaultDeath(Entity entity)
            => entity.Destroy();
    }
}
