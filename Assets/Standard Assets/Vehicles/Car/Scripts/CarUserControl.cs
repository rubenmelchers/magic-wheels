using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {

            int horizontal = 0;
            int vertical = 0;

            #if UNITY_STANDALON || UNITY_WEBPLAYER

            horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
            vertical = (int) (Input.GetAxisRaw ("Vertical"));

            if(horizontal != 0) {
                vertical = 0;
            }

            #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

            if(Input.touchCount > 0) {
                Touch myTouch = Input.touches[0];

                if(myTouch.phase == TouchPhase.Began) {
                    touchOrigin = myTouch.position;
                } else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
                    Vector2 touchEnd = myTouch.position;

                    float x = touchEnd.x - touchOrigin.x;

                    float y = touchEnd.y - touchOrigin.y;

                    touchOrigin.x = -1;

                    if (Mathf.Abs(x) > Mathf.Abs(y)) {
                        horizontal = x > 0 ? 1 : -1;
                    } else {
                        vertical = y > 0 ? 1 : -1;
                    }
                }
            }

            #endif

            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
