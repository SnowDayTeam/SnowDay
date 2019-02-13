using UnityEngine;
using UnityEngine.UI;

public class GamemodeGUIManagerBase : MonoBehaviour {

	[SerializeField] protected Text[] TeamsScore = null;

    [SerializeField] Text GameTimeText = null;

    [SerializeField] GameObject EndGameWindow = null;
    [SerializeField] Text EndGameWindowText = null;

    public virtual void UpdateScoreTextAtIndex(int Index, int Score) {
        this.TeamsScore[Index].text = Score.ToString();
    }

    public virtual void UpdateGameTimeText(float SecondsLeftInRound) {

        //To ensure time doesnt get displayed if it is negative
        SecondsLeftInRound = Mathf.Max(0, SecondsLeftInRound);
        //done like this for toString formatting down the road
        int minutes = (int)(SecondsLeftInRound / 60.0f);
        int seconds = (int)(SecondsLeftInRound % 60);

        this.GameTimeText.text = minutes + ":" + seconds;
    }

    public void ShowEndGameWindow(string EndGameText) {

        this.EndGameWindow.gameObject.SetActive(true);
        this.EndGameWindowText.text = EndGameText;
    }
}
