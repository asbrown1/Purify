using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 startOffset = new Vector3(0f, 7.5f, -4f);
        public float rotateSpeed = 1f;

        void start()
        {
            transform.rotation = target.rotation;
        }
        private void LateUpdate()
        {
            Vector3 offset = startOffset;
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
            transform.Rotate(0, Input.GetAxis("Mouse X")*Time.deltaTime*rotateSpeed, 0, Space.World);
            offset.z = (float)Math.Cos(transform.eulerAngles.y*Mathf.Deg2Rad)*startOffset.z;
            offset.x = (float)Math.Sin(transform.eulerAngles.y*Mathf.Deg2Rad) * startOffset.z;
            transform.position = target.position + offset;
        }
    }
}
