using UnityEngine;
using System.Collections;

public class CameraMethodSwitch : MonoBehaviour
{

	public TIMCamera.CameraFollow2D behavioursOn;
	public MonoBehaviour[] behavioursOff;
	public bool defaultBehaviours;

	private bool on;

	void Awake ()
	{
		SetCameraMode (defaultBehaviours);
	}

	public void SetCameraMode (bool enable)
	{
		on = enable;
		if (enable) {
			behavioursOn.enabled = enable;
			for (int i = 0; i < behavioursOff.Length; i++) {
				behavioursOff [i].enabled = !enable;
			}
		} 
	}

	void Update ()
	{
		if (!on && behavioursOn.enabled) {
			if (behavioursOn.isOnTarget()) {
				behavioursOn.enabled = false;
				for (int i = 0; i < behavioursOff.Length; i++) {
                    behavioursOff [i].enabled = true;
                }
			}
		}
	}

}
