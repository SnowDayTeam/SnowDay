using UnityEngine;
using UnityEngine.UI;

public class ShovelWarGUIManager : MonoBehaviour {

	[SerializeField] Text TeamOneClearPercentText = null;
    [SerializeField] Text TeamTwoClearPercentText = null;

    [SerializeField] Text GameTimeText = null;

    [SerializeField] GameObject EndGameWindow = null;
    [SerializeField] Text EndGameWindowText = null;

    public void UpdateClearPercentTexts(float TeamOneClearPercent, float TeamTwoClearPercent) {

        this.TeamOneClearPercentText.text = Mathf.RoundToInt(TeamOneClearPercent) + "%";
        this.TeamTwoClearPercentText.text = Mathf.RoundToInt(TeamTwoClearPercent) + "%";
    }

    public void UpdateGameTimeText(float SecondsLeftInRound) {

        //To ensure time doesnt get displayed if it is negative
        SecondsLeftInRound = Mathf.Max(0, SecondsLeftInRound);
        //done like this for toString formatting
        int minutes = (int)(SecondsLeftInRound / 60.0f);
        int seconds = (int)(SecondsLeftInRound % 60);

        this.GameTimeText.text = minutes + ":" + seconds;
    }

    public void ShowEndGameWindow(string EndGameText) {

        this.EndGameWindow.gameObject.SetActive(true);
        this.EndGameWindowText.text = EndGameText;
    }
}
