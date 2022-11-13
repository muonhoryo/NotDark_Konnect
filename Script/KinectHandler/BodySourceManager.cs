
using UnityEngine;
using Windows.Kinect;

namespace NotDarkKinect
{
    public class BodySourceManager : MonoBehaviour
    {
        private KinectSensor _Sensor;
        private BodyFrameReader _Reader;
        private Body[] _Data = null;
        public float JointsPosMultiplier=1f;
        public Vector2 JointsOffset;

        public Body[] GetData()
        {
            return _Data;
        }

        private void Awake()
        {
            Registry.bodySourceManager= this;
        }
        void Start()
        {
            _Sensor = KinectSensor.GetDefault();

            if (_Sensor != null)
            {
                _Reader = _Sensor.BodyFrameSource.OpenReader();

                if (!_Sensor.IsOpen)
                {
                    _Sensor.Open();
                }
            }
        }

        void Update()
        {
            if (_Reader != null)
            {
                BodyFrame frame = _Reader.AcquireLatestFrame();
                if (frame != null)
                {
                    if (_Data == null)
                    {
                        _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                    }

                    frame.GetAndRefreshBodyData(_Data);

                    frame.Dispose();
                }
            }
        }

        void OnApplicationQuit()
        {
            if (_Reader != null)
            {
                _Reader.Dispose();
            }

            if (_Sensor != null)
            {
                if (_Sensor.IsOpen)
                {
                    _Sensor.Close();
                }
            }
        }
    }
}
