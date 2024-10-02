using UnityEngine;
using System.Collections;

public class ChangeCameraTarget : MonoBehaviour
{

	public TIMCamera.CameraFollow2D cameraFollow;

	void Start ()
	{
		Messenger.AddListener<PlayerState> (MessengerIDs.PLAYER_START_TURN, PlayerStartedTurn);
	}

	private void PlayerStartedTurn (PlayerState player)
	{
		cameraFollow.m_Player = player.transform;
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener<PlayerState> (MessengerIDs.PLAYER_START_TURN, PlayerStartedTurn);
	}
}
