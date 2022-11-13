
using System;
using UnityEngine;
using Windows.Kinect;

namespace NotDarkKinect
{
    public class DetectJoint : MonoBehaviour
    {
        public JointType TrackedJoint;
        private Body[] bodies;
        public float multiplier = 1f;
        BodySourceManager manager => Registry.bodySourceManager;
        public event Action PositionWasChanged= delegate { };
        private Vector3 OldPos = Vector3.zero;

        void Start()
        {
            if (manager == null)
            {
                Debug.LogError("Asign Game Object with Body Source Manager");
            }
        }

        private void MoveJoint(CameraSpacePoint kinectJointPoint)
        {
            transform.position = new Vector3
                ((kinectJointPoint.X + manager.JointsOffset.x) * multiplier * manager.JointsPosMultiplier,
                (kinectJointPoint.Y + manager.JointsOffset.y) * multiplier * manager.JointsPosMultiplier,
                gameObject.transform.position.z);
        }
        void Update()
        {
            bodies = manager.GetData();
            if (bodies != null)
            {
                foreach (var body in bodies)
                {
                    if (body == null)
                    {
                        continue;
                    }
                    if (body.IsTracked)
                    {
                        MoveJoint(body.Joints[TrackedJoint].Position);
                    }
                }
            }
            if (OldPos != transform.position)
            {
                OldPos = transform.position;
                PositionWasChanged();
            }
            else
            {
                OldPos = transform.position;
            }
        }
    }
}
