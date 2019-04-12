using UnityEngine;
using UnityEngine.UI;
using SnowDay.Diego.CharacterController;

public class DeathmatchGamemodeManager : GamemodeManagerBase 
{
    [SerializeField] DeathMatchTeam[] Teams = null;

    //Leron Added-----------------------------------------------
    public GameObject[] teamPanels;
    public GameObject img;
     int[,] prefabValue;
    int index = 0;
    public Texture[] characterImages;

    //----------------------------------------------------------

    [System.Serializable]
	public class DeathMatchTeam : TeamBase
    {
        public int PlayersAlive;
    }

    public int GetTeamScore(int TeamID)
    {
        return Teams[TeamID].Score;
    }

    /// <summary>
    /// Called from PlayerActor.DecreaseHealth. Happens only when a player dies
    /// </summary>
    /// <param name="DeadPlayerTeamID"> The ID of the team the player who died was part of </param>
    /// <param name="KillingPlayerTeamID"> The ID of the team who killed the player </param>
    public void OnPlayerDied(int DeadPlayerTeamID, int KillingPlayerTeamID) 
    {
        this.Teams[DeadPlayerTeamID].PlayersAlive--;
        this.Teams[KillingPlayerTeamID].Score ++;

        this.CheckGameEndConditions();
    }

    protected override void Start() 
    {
        base.Start();

        this.SetPlayerTeamIDs();

    }

    public override TeamBase[] GetTeams() 
    {
        return this.Teams;
    }

    protected override void CheckGameEndConditions() 
    {
        if(base.DidGameEnd)
            return;

        foreach(DeathMatchTeam team in this.Teams) 
        {
            if (team.PlayersAlive <= 0) 
            {
                base.EndGame();
                return;
            }
        }
    }

    protected override void CheckGameWinnerAndDisplayResults() 
    {
        //show game end screen here when it exists
    }

    private void SetPlayerTeamIDs() 
    {
        prefabValue = new int[2, 8];

        for (int i = 0; i < this.Teams.Length; i++) 
        {
            Teams[i].PlayersAlive = Teams[i].Players.Count;
          //  teamPanels[i] = 

            foreach (PlayerController player_controller in this.Teams[i].Players) 
            {

                player_controller.GetComponentInChildren<PlayerActor>().TeamID = i;


                //Leron Added-----------------------------------------------

                //if (index < this.Teams[i].Players.Count)
                //{
                //    index++;
                //}

                //else
                //{
                //    index = 0; // makes sure index resets on team switch
                //}

                //prefabValue[i, index] = player_controller.currentPrefab; //2D array going through each character on each team and getting the prefab's associated number

                //Debug.Log(prefabValue[i, index]); 

                //GameObject childObjects = Instantiate(img) as GameObject; // Instantiating raw image for ever character
                //childObjects.GetComponent<RawImage>().texture = characterImages[prefabValue[i, index]];
                //childObjects.transform.parent = teamPanels[i].transform; //setting it as a child under corrisponding panel color.
                //----------------------------------------------------------
            }
        }
    }
}
