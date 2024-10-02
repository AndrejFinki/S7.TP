public class IfConditionPlayerBin : IfCondition {
	
	public override bool check () {

		PlayerQueue queue = PlayerQueue.getInstance ();
		if (queue == null) {
			return false;
		}
		return check (queue.GetCurrentPlayer ().bin);

	}

}
