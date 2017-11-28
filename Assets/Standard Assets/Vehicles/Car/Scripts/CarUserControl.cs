using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        private Vector3 touchOrigin = -Vector3.one;

        public bool isDownLeft = false;
        public bool isDownRight = false;
        public bool isDownBrake = false;


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = 5f;
            float verticalInput = Input.GetAxis("Vertical");

            #if UNITY_STANDALON || UNITY_WEBPLAYER

            // horizontal = Input.GetAxis ("Horizontal");
            // vertical = (float) (CrossPlatformInputManager.GetAxis ("Vertical"));
            // vertical = 5f;

            // if(horizontal != 0) {
            //     vertical = 0;
            // }

            #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

            if(Input.touchCount > 0) {
                Touch myTouch = Input.touches[0];

                if (myTouch.phase == TouchPhase.Began) {
                    touchOrigin = myTouch.position;

                } else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
                    Vector3 touchEnd = myTouch.position;

                    float x = touchEnd.x - touchOrigin.x;

                    // float y = touchEnd.y - touchOrigin.y;

                    // touchOrigin.x = -1;

                    // if (Mathf.Abs(x) > Mathf.Abs(y)) {
                    //     horizontal = x > 0 ? 1 : -1;
                    // } else {
                    //     vertical = y > 0 ? 1 : -1;
                    // }

                }
            }

            #endif

            // if(horizontal != 0 || vertical != 0) {
                // AttemptMove<Wall> (horizontal vertical);
                #if !MOBILE_INPUT
                float handbrake = Input.GetAxis("Jump");
                if(verticalInput != 0) {
                    m_Car.Move(horizontal, verticalInput, verticalInput, handbrake);
                } else {
                    m_Car.Move(horizontal, vertical, vertical, handbrake);
                }

                #else
                if(verticalInput != 0) { //if the brake button is pressed
                    m_Car.Move(horizontal, verticalInput, verticalInput, 0f);
                } else {
                    m_Car.Move(horizontal, vertical, vertical, 0f);


                    float acceleroInput = Input.acceleration.x;

                    float low = 0.1f;
                    float high = 10f;

                    if(acceleroInput <= low && acceleroInput >= -low) {

                        m_Car.Move(horizontal, vertical, vertical, 0f);

                    } else if (acceleroInput >= low) {
                            m_Car.Move(0.5f, vertical, vertical, 0f);

                        if (acceleroInput >= high) {
                            m_Car.Move(5f, vertical, vertical, 0f);

                        }

                    } else if (acceleroInput <= -low) {
                            m_Car.Move(-0.5f, vertical, vertical, 0f);

                        if(acceleroInput <= -high) {
                            m_Car.Move(-5f, vertical, vertical, 0f);
                        }

                    }
                }
                #endif
            // }

            if(isDownLeft) {
                if(verticalInput != 0) {
                    m_Car.Move(-5f, verticalInput, verticalInput, 0f);
                } else {
                    m_Car.Move(-5f, vertical, vertical, 0f);
                }
            }

            if (isDownRight) {
                if(verticalInput != 0) {
                    m_Car.Move(5f, verticalInput, verticalInput, 0f);
                } else {
                    m_Car.Move(5f, vertical, vertical, 0f);
                }
            }

            if(isDownBrake) {
                m_Car.Move(horizontal, -5f, -5f, 0f);

                if(isDownLeft) {
                    m_Car.Move(-5f, -5f, -5f, 0f);
                }

                if(isDownRight) {
                    m_Car.Move(5f, -5f, -5f, 0f);
                }
            }

            if(isDownLeft && isDownRight) {
                m_Car.Move(horizontal, vertical, vertical, 0f);

                if(isDownBrake) {
                    m_Car.Move(horizontal, -5f, -5f, 0f);
                }
            }

            // pass the input to the car!
//             float h = CrossPlatformInputManager.GetAxis("Horizontal");
//             float v = CrossPlatformInputManager.GetAxis("Vertical");
// #if !MOBILE_INPUT
//             float handbrake = CrossPlatformInputManager.GetAxis("Jump");
//             m_Car.Move(h, v, v, handbrake);
// #else
//             m_Car.Move(h, v, v, 0f);
// #endif
        }


        public void onPointerDownLeft() {
            isDownLeft = true;
        }

        public void onPointerUpLeft() {
            isDownLeft = false;
        }

        public void onPointerDownRight() {
            isDownRight = true;
        }

        public void onPointerUpRIght() {
            isDownRight = false;
        }

        public void onPointerDownBrake() {
            isDownBrake = true;
        }

        public void onPointerUpBrake() {
            isDownBrake = false;
        }
    }
}
