using UnityEngine;

namespace Patterns
{
    public class FreeCam : MonoBehaviour
    {
        public float movementSpeed = 10f;
        public float fastMovementSpeed = 100f;
        public float freeLookSensitivity = 3f;
        public float zoomSensitivity = 10f;
        public float fastZoomSensitivity = 50f;
        private bool looking = false;

        private void Update()
        {
            bool fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            float movementSpeed = fastMode ? fastMovementSpeed : this.movementSpeed;
            Transform mTr = transform;
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                mTr.position = mTr.position + (-mTr.right * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                mTr.position = mTr.position + (mTr.right * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                mTr.position = mTr.position + (mTr.forward * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                mTr.position = mTr.position + (-mTr.forward * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                mTr.position = mTr.position + (mTr.up * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                mTr.position = mTr.position + (-mTr.up * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
            {
                mTr.position = mTr.position + (Vector3.up * movementSpeed * Time.unscaledDeltaTime);
            }

            if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
            {
                mTr.position = mTr.position + (-Vector3.up * movementSpeed * Time.unscaledDeltaTime);
            }

            if (looking)
            {
                float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
                float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
                transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
            }

            float axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0)
            {
                float zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
                transform.position = transform.position + transform.forward * axis * zoomSensitivity;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartLooking();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                StopLooking();
            }
        }

        private void OnDisable()
        {
            StopLooking();
        }
        
        private void StartLooking()
        {
            looking = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void StopLooking()
        {
            looking = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
