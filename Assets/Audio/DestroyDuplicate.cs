using UnityEngine;
using System.Collections;

public class DestroyDuplicate : MonoBehaviour
{

	void Awake ()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
		if (objects.Length > 1) {
			DestroyImmediate(gameObject);
		}
	}

}
