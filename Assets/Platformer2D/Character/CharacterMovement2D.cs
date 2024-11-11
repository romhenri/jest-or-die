using UnityEngine;

namespace Platformer2D.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CharacterMovement2D : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float groundMaxSpeed = 7.0f;
        [SerializeField] float groundAcceleration = 100.0f;

        [Header("Jump")]
        [SerializeField] float maxJumpHeight = 4.0f;
        [SerializeField] float jumpPeakTime = 0.4f;
        [SerializeField] float jumpAbortDecceleration = 100.0f;

        [Header("Crouch")]
        [Range(0.1f, 1.0f)]
        [SerializeField] float crouchCapsuleHeightPercent = 0.5f;

        [Range(0.0f, 1.0f)]
        [SerializeField] float crouchGroundSpeedPercent = 0.01f;

        [Space]

        [Header("Collision")]
        [SerializeField] LayerMask groundedLayerMask = default;
        [SerializeField] float groundedRaycastDistance = 0.1f;

        protected Vector2 currentVelocity;
        Rigidbody2D rigidbody2d;
        ContactFilter2D contactFilter;

        public IColliderInfo ColliderInfo { get; private set; }
        bool isGrounded;
        bool isCrouching;
        bool wantsToUnCrouch;
        bool wasGroundedLastFrame;

        public bool IsGrounded { get { return isGrounded == wasGroundedLastFrame && isGrounded; } }

        public bool IsCrouching { get { return isCrouching; } }
        public bool IsJumping { get { return currentVelocity.y > 0; } }

        public float MaxGroundSpeed 
        { 
            get { return groundMaxSpeed * (IsCrouching ? crouchGroundSpeedPercent : 1.0f); }
            set { groundMaxSpeed = value; }
        }

        public float GroundRaycastDistance { get { return groundedRaycastDistance; } }

        public Vector2 CurrentVelocity { get { return currentVelocity; } }

        public Rigidbody2D RigidBody { get { return rigidbody2d; } }

        float Gravity { get { return maxJumpHeight * 2 / (jumpPeakTime * jumpPeakTime); } }
        public float JumpSpeed { get { return Gravity * jumpPeakTime; } }

        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2D thisCollider = GetComponent<Collider2D>();

            ColliderInfo = ColliderInfoFactory.NewColliderInfo(thisCollider);

            rigidbody2d.gravityScale = 0;
            rigidbody2d.freezeRotation = true;
            
            contactFilter.layerMask = groundedLayerMask;
            contactFilter.useLayerMask = true;
            contactFilter.useTriggers = false;

            Physics2D.queriesStartInColliders = false;
        }

        void CheckUnCrouch()
        {
            if (wantsToUnCrouch && CheckCapsuleCollisionsTop() == false)
            {
                UnCrouch();
            }
        }

        protected virtual void FixedUpdate()
        {
            ApplyGravity();

            Vector2 previousPosition = rigidbody2d.position;
            Vector2 currentPosition = previousPosition + currentVelocity * Time.fixedDeltaTime;
            rigidbody2d.MovePosition(currentPosition);

            if (currentVelocity.y <= 0)
            {
                CheckCapsuleCollisionsBottom();
            }

            if (currentVelocity.y > 0)
            {
                CheckCapsuleCollisionsTop();
            }

            if (currentVelocity.x != 0)
            {
                CheckCapsuleCollisionsWalls();
            }

            CheckUnCrouch();
        }


        void ApplyGravity()
        {
            float maxFallSpeed = -16.0f;
            currentVelocity.y = Mathf.Max(currentVelocity.y - Gravity * Time.fixedDeltaTime, maxFallSpeed);
        }

        protected bool CanJump()
        {
            return IsGrounded && !IsJumping && !IsCrouching;
        }

        public void ProcessMovementInput(Vector2 movementInput)
        {
            float desiredHorizontalSpeed = movementInput.x * MaxGroundSpeed;
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredHorizontalSpeed, groundAcceleration * Time.deltaTime);
        }

        public void StopImmediately()
        {
            currentVelocity = Vector2.zero;
        }
 
        public void Jump()
        {
            if (CanJump())
            {
                currentVelocity.y = JumpSpeed;
            }
        }
        public void UpdateJumpAbort()
        {
            if (IsJumping)
            {
                currentVelocity.y -= jumpAbortDecceleration * Time.deltaTime;
            }
        }

        private void SetCapsuleHeight(float newHeight)
        {
            float capsuleCurrentHeight = ColliderInfo.Size.y;
            ColliderInfo.Size = new Vector2(ColliderInfo.Size.x, newHeight);
            ColliderInfo.Offset += new Vector2(0.0f, (newHeight - capsuleCurrentHeight)*0.5f);
        }

        public void Crouch()
        {
            wantsToUnCrouch = false;

            if (CanCrouch() == false)
            {
                return;
            }

            isCrouching = true;

            SetCapsuleHeight(ColliderInfo.Size.y * crouchCapsuleHeightPercent);
        }

        public void UnCrouch()
        {
            wantsToUnCrouch = true;
            if (CanUncrouch() == false)
            {
                return;
            }

            wantsToUnCrouch = false;
            isCrouching = false;
            SetCapsuleHeight(ColliderInfo.Size.y / crouchCapsuleHeightPercent);
        }

        private static readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
        private static readonly Vector2[] raycastPositions = new Vector2[3];
        private const int raycastCount = 3;

        bool CheckCapsuleCollisions(Vector2 basePosition, Vector2 direction, out int hitCount, bool earlyReturn = false)
        {
            float offset = ColliderInfo.Size.x * 0.5f;

            raycastPositions[0] = basePosition + Vector2.left * offset;
            raycastPositions[1] = basePosition;
            raycastPositions[2] = basePosition + Vector2.right * offset;

            float raycastDistance = offset + groundedRaycastDistance * 2f;
            hitCount = 0;

            for (int i = 0; i < raycastCount; i++)
            {
                Debug.DrawLine(raycastPositions[i], raycastPositions[i] + direction * raycastDistance, Color.red);

                if (Physics2D.RaycastNonAlloc(raycastPositions[i], direction, hitBuffer, raycastDistance, contactFilter.layerMask) > 0)
                {
                    hitCount++;
                    if (earlyReturn)
                        return true;
                }
            }

            return hitCount > 0;
        }

        bool CheckCapsuleCollisionsTop()
        {
            int hitCount;
            return CheckCapsuleCollisions(GetColliderTop(), Vector2.up, out hitCount, earlyReturn: true);
        }

        void CheckCapsuleCollisionsBottom()
        {
            int hitCount;
            bool hasCollision = CheckCapsuleCollisions(GetColliderBottom(), Vector2.down, out hitCount);

            wasGroundedLastFrame = isGrounded;
            isGrounded = hitCount > 0;

            if (isGrounded && !IsJumping)
            {
                currentVelocity.y = 0;
            }
        }

        bool CheckCapsuleCollisionsWalls()
        {
            int leftHitCount;
            int rightHitCount;

            bool isLeftWallHit = CheckCapsuleCollisions(GetColliderLeft(), Vector2.left, out leftHitCount);

            bool isRightWallHit = CheckCapsuleCollisions(GetColliderRight(), Vector2.right, out rightHitCount);

            return isLeftWallHit || isRightWallHit;
        }

        Vector2 GetColliderLeft()
        {
            return new Vector2(GetColliderCenter().x - ColliderInfo.Size.x * 0.5f, GetColliderCenter().y);
        }

        Vector2 GetColliderRight()
        {
            return new Vector2(GetColliderCenter().x + ColliderInfo.Size.x * 0.5f, GetColliderCenter().y);
        }

        Vector2 GetColliderCenter()
        {
            return (Vector2)transform.position + ColliderInfo.Offset;
        }


        bool CanCrouch()
        {
            return IsCrouching == false;
        }

        bool CanUncrouch()
        {
            return IsCrouching && CheckCapsuleCollisionsTop() == false;
        }

        public int RaycastAgainstGround(Vector2 raycastOrigin, Vector2 raycastDirection, float raycastDistance, RaycastHit2D[] hitBuffer)
        {
            Debug.DrawLine(raycastOrigin, raycastOrigin + raycastDirection * raycastDistance);
            return Physics2D.Raycast(raycastOrigin, raycastDirection, contactFilter, hitBuffer, raycastDistance);
        }

        public int RaycastAgainstGround(Vector2 raycastOrigin, Vector2 raycastDirection, float raycastDistance)
        {
            RaycastHit2D[] hitResults = new RaycastHit2D[1];
            return RaycastAgainstGround(raycastOrigin, raycastDirection, raycastDistance, hitResults);
        }

        public Vector2 GetColliderBottom()
        {
            return rigidbody2d.position + ColliderInfo.Offset + Vector2.down * (ColliderInfo.Size.y * 0.5f - ColliderInfo.Size.x * 0.5f);
        }

        public Vector2 GetColliderTop()
        {
            return rigidbody2d.position + ColliderInfo.Offset + Vector2.up * (ColliderInfo.Size.y * 0.5f - ColliderInfo.Size.x * 0.5f);
        }
    }
}