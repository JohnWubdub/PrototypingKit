// TweenSpringBounce.cs
// Last edited 11:11 AM 04/17/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit
{
    /// <summary>
    ///     This is a tween that will create spring-like movement based on Hooke's law and
    ///     Euler integration. It's intended for use with non-physics objects, like sprites,
    ///     that you don't want to use the Unity rigidbody + collider + spring joint system on.
    /// </summary>
    public class TweenSpringBounce : MonoBehaviour
    {
        /// <summary>
        ///     If set to <c>TRUE</c> the targetPosition will act as a floor for the tween (the attached GameObject)
        ///     will bounce off the targetPosition and lose velocity equivalent to <c>restitution * velocity</c>.
        /// </summary>
        public bool targetPositionIsFloor;

        [SerializeField] private Vector3 targetPosition, velocity;
        [SerializeField] private float dampingRatio;
        [SerializeField] private float mass;
        [SerializeField] private float k;
        [SerializeField] private float restitution;
        [SerializeField] private bool clampZ;
        [SerializeField] private Vector3 impulse;

        public float SpringStrenth
        {
            set { k = value; }
        }

        public Vector3 TargetPosition
        {
            set { targetPosition = value; }
        }

        private void Start()
        {
            impulse = Vector3.zero;
        }

        private void FixedUpdate()
        {
            /* We're using simple Euler integration to get velocity from accumulated forces created by
         * the spring, with damping applied to the velocity
         * 
         * NOTE: Since we're moving the position of the transform you won't want to use this
         * tween on an object with a rigidbody.
         */
            Vector3 acceleration, normal, positionNextFrame;
            Vector3 force = acceleration = Vector3.zero;
            force += impulse;
            impulse = Vector3.zero;
            positionNextFrame = transform.localPosition;
            normal = transform.localPosition - targetPosition;

            force += -k * normal;
            acceleration = (force / mass) * Time.deltaTime;
            velocity += acceleration * Time.deltaTime;
            velocity *= dampingRatio;
            positionNextFrame += velocity * Time.deltaTime;

            if (targetPositionIsFloor)
            {
                if (positionNextFrame.y < 0)
                {
                    velocity *= restitution;
                    velocity *= -1;
                }
                positionNextFrame.Set(Mathf.Abs(positionNextFrame.x), Mathf.Abs(positionNextFrame.y), Mathf.Abs(positionNextFrame.z));
            }

            // check for Z-position clamping to prevent movement between Z-layers
            float z = clampZ ? transform.localPosition.z : positionNextFrame.z;
            positionNextFrame.z = z;
            transform.localPosition = positionNextFrame;
        }

        /// <summary>
        ///     Sets the tween values, otherwise sets to default.
        /// </summary>
        public void SetupTween(float _mass = 1.0f, float _k = 1.0f, float _dampingRatio = 1.0f, float _restitution = 1.0f,
                               bool _targetPositionIsFloor = false)
        {
            mass = _mass;
            k = _k;
            dampingRatio = _dampingRatio;
            restitution = _restitution;
            targetPositionIsFloor = _targetPositionIsFloor;
        }

        public void AddImpulse(Vector3 impulseForce)
        {
            impulse += impulseForce;
        }
    }
}