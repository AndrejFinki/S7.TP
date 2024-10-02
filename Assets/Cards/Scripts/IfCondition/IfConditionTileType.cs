public class IfConditionTileType : IfCondition {

	public int tileType;

	public override bool check () {

		TypeCounter typeCounter = TypeCounter.getInstance ();
		if (typeCounter == null) {
			return false;
		}
		return check (typeCounter.GetCountable (tileType));

	}

}
