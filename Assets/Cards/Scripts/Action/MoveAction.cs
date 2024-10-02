using UnityEngine;
using System.Collections;

public delegate void ActionCompleted (bool successful);

public abstract class MoveAction : MonoBehaviour
{

	public abstract void PlayAction (ActionCompleted callback, int move, BasePlayerControll playerControll);

	public abstract int ActionAiScore (int moves);

	public abstract Tile ActionAiTile (int moves);

}
