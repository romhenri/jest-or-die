using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnchor : MonoBehaviour
{
    [System.Serializable]
    public class MovementInfo
    {
        public Vector3 targetPosition;
        public float speed;
        public MovementType movementType;
        public bool isRelative;
        public bool isLooping;
    }

    public enum MovementType
    {
        Linear,
        Sinusoidal
    }

    public List<MovementInfo> movements;
    private int currentMovementIndex = 0;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingTowardsTarget = true;
    private float time;

    void Start()
    {
        if (movements.Count > 0)
        {
            startPosition = transform.position;
            SetTargetPosition();
            time = 0f;
        }
    }

    void Update()
    {
        if (movements.Count == 0) return;

        MovementInfo currentMovement = movements[currentMovementIndex];

        if (currentMovement.speed <= 0)
        {
            Debug.LogWarning("Velocidade nao pode ser zero ou negativa. Verifique a configuracao no Inspector.");
            return;
        }

        switch (currentMovement.movementType)
        {
            case MovementType.Linear:
                MoveLinear(currentMovement);
                break;

            case MovementType.Sinusoidal:
                MoveSinusoidal(currentMovement);
                break;
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (currentMovement.isLooping)
            {
                movingTowardsTarget = !movingTowardsTarget;
                startPosition = transform.position;
                SetTargetPosition();
                time = 0f;
            }
            else
            {
                currentMovementIndex = (currentMovementIndex + 1) % movements.Count;
                startPosition = transform.position;
                movingTowardsTarget = true;
                SetTargetPosition();
                time = 0f;
            }
        }
    }

    void MoveLinear(MovementInfo movement)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movement.speed * Time.deltaTime);
    }

    void MoveSinusoidal(MovementInfo movement)
    {
        time += Time.deltaTime;
        Vector3 direction = (targetPosition - startPosition).normalized;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float oscillation = Mathf.Sin(time * movement.speed) * 0.5f;

        transform.position = Vector3.MoveTowards(startPosition + direction * distance * (time / distance),
            targetPosition, movement.speed * Time.deltaTime) + new Vector3(0, oscillation, 0);
    }
    void SetTargetPosition()
    {
        MovementInfo currentMovement = movements[currentMovementIndex];

        if (currentMovement.isRelative)
        {
            if (movingTowardsTarget)
            {
                targetPosition = startPosition + currentMovement.targetPosition;
            }
            else
            {
                targetPosition = startPosition - currentMovement.targetPosition;
            }
        }
        else
        {
            targetPosition = movingTowardsTarget ? currentMovement.targetPosition : startPosition;
        }
    }
}
