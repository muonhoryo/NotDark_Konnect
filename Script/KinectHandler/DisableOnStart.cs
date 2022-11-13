
using UnityEngine;

namespace NotDarkKinect
{
    public class DisableOnStart : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }
    }
}
