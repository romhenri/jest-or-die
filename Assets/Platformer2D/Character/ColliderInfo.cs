using UnityEngine;

namespace Platformer2D.Character
{
    public interface IColliderInfo
    {
        Vector2 Size { get; set; }
        Vector2 Offset { get; set; }

        Collider2D Collider { get; }
    }

    public class ColliderInfoFactory
    {
        public static IColliderInfo NewColliderInfo(Collider2D collider)
        {
            if (collider.GetType() == typeof(CapsuleCollider2D))
            {
                return new CapsuleColliderInfo((CapsuleCollider2D) collider);
            }
            if (collider.GetType() == typeof(BoxCollider2D))
            {
                return new BoxColliderInfo((BoxCollider2D)collider);
            }

            throw new System.Exception("No ColliderInfo implementation for type: " + (collider != null ? collider.name : "NULL"));
        }
    }

    public class CapsuleColliderInfo : IColliderInfo
    {
        public CapsuleColliderInfo(CapsuleCollider2D inCapsuleCollider)
        {
            capsuleCollider = inCapsuleCollider;
        }

        private CapsuleCollider2D capsuleCollider;
        public Vector2 Size 
        { 
            get => capsuleCollider.size; 
            set => capsuleCollider.size = value; 
        }

        public Vector2 Offset 
        { 
            get => capsuleCollider.offset; 
            set => capsuleCollider.offset = value; 
        }

        public Collider2D Collider => capsuleCollider;
    }

    public class BoxColliderInfo : IColliderInfo
    {
        public BoxColliderInfo(BoxCollider2D inBoxCollider)
        {
            boxCollider = inBoxCollider;
        }

        private BoxCollider2D boxCollider;
        public Vector2 Size
        {
            get => boxCollider.size;
            set => boxCollider.size = value;
        }

        public Vector2 Offset 
        { 
            get => boxCollider.offset; 
            set => boxCollider.offset = value; 
        }

        public Collider2D Collider => boxCollider;
    }
}