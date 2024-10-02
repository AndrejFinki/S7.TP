using System;
using UnityEngine;

namespace TIMCamera
{
	public class CameraFollow2D : MonoBehaviour
	{
        public delegate void OnTargetCallback();

		public float xSmooth = 8f;
		public float ySmooth = 8f;

		public bool lockX;
		public bool lockY;

		private float xSpeed;
		private float ySpeed;

		public Transform m_Player;

		private bool onTarget;
		private Vector3 prevPosition;
        private OnTargetCallback callback;

		void Update ()
		{
			onTarget = prevPosition == transform.position;
			prevPosition = transform.position;
			TrackPlayer ();
		}

		private void TrackPlayer ()
		{
			if (!m_Player)
				return;
			
			float targetX = transform.position.x;
			float targetY = transform.position.y;

			if (!lockX)
				targetX = Mathf.SmoothDamp (transform.position.x, m_Player.position.x, ref xSpeed, xSmooth);
			if (!lockY)
				targetY = Mathf.SmoothDamp (transform.position.y, m_Player.position.y, ref ySpeed, ySmooth);

			transform.position = new Vector3 (targetX, targetY, transform.position.z);

            if (callback != null && isOnTarget()) {
                callback();
                callback = null;
            }
		}

		public bool isOnTarget() {
			return onTarget;
		}

        public void OnTarget(OnTargetCallback callback) {
            this.callback = callback;
        }
	}

}
