
using UnityEngine;

namespace NotDarkKinect
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public sealed class BonePhysics : MonoBehaviour
    {
        [SerializeField]
        private DetectJoint StartJoint;
        [SerializeField]
        private DetectJoint EndJoint;
        private float ColliderVerticalSize;
        private CapsuleCollider2D Collider;
        private void Start()
        {
            Collider = GetComponent<CapsuleCollider2D>();
            ColliderVerticalSize = Collider.size.y;
            StartJoint.PositionWasChanged += JointPositionWasChanged;
            EndJoint.PositionWasChanged += JointPositionWasChanged;
            JointPositionWasChanged();
        }
        private void JointPositionWasChanged()
        {
            Vector2 startNewPos = StartJoint.transform.position;
            Vector2 endNewPos = EndJoint.transform.position;
            transform.position = (endNewPos + startNewPos)/2;
            transform.rotation = Quaternion.Euler
                (0,
                0,
                Vector2.SignedAngle(startNewPos, endNewPos));
            Collider.size = new Vector2
                (Collider.size.x,
                ColliderVerticalSize*Vector2.Distance(startNewPos, endNewPos));
        }
    }
}
