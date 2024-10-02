using UnityEngine;
using System.Collections;

public abstract class IfCondition : MonoBehaviour {

	public enum Condition {
		less, greater, equal
	}

	public Condition condition;
	public int goal;

	protected bool check(int value) {

		switch (condition) {
		case Condition.less:
			return value < goal;
		case Condition.greater:
			return value > goal;
		case Condition.equal:
			return value == goal;
		default:
			return false;
		}

	}

	public abstract bool check ();

}
