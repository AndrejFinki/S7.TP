using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class CloudsRespawn : MonoBehaviour
{

	public float minSpeed;
	public float maxSpeed;

	private RectTransform[] clouds;
	private RectTransform rectTransform;

	void Start ()
	{
		clouds = GetComponentsInChildren<RectTransform> ();
		rectTransform = GetComponent<RectTransform> ();
		for (int i = 0; i < clouds.Length; i++) {
			if (clouds [i].gameObject != gameObject) {
				clouds [i].GetComponent<AutoMoveAndRotate> ().moveUnitsPerSecond.value = new Vector3 (Random.Range (minSpeed, maxSpeed), 0, 0);
			}
		}
	}

	void Update ()
	{
		for (int i = 0; i < clouds.Length; i++) {
			if (clouds [i].gameObject != gameObject && clouds [i].anchoredPosition.x > rectTransform.rect.width) {
				float x = -clouds [i].rect.width;
				float height = rectTransform.rect.height - clouds [i].rect.height / 2;
				float y = height * 10 / Random.Range (0, 11);
				clouds [i].anchoredPosition = new Vector2 (x, y);
				clouds [i].GetComponent<AutoMoveAndRotate> ().moveUnitsPerSecond.value = new Vector3 (Random.Range (minSpeed, maxSpeed), 0, 0);
			}
		}
	}

}
