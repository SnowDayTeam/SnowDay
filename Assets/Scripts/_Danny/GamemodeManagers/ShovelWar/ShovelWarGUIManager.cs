using UnityEngine;

public class ShovelWarGUIManager : GamemodeGUIManagerBase {

	public override void UpdateScoreTextAtIndex(int Index, int Score) {
        this.TeamsScore[Index].text = Score + "%";
    }
}
