using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class BackyardGolf : MonoBehaviour
{
    static private BackyardGolf S;

    [Header("Inscribed")]
    public Text uitLevel; //The UIText_Level text
    public Text uitShots; //The UIText_Shots text
    //public Vector3 coursePos; //The place for the course
    //public GameObject[] courses; //An array of the courses;

    [Header("Dynamic")]
    public int level; //The current level
    public int levelMax; //The number of levels
    public int shotsTaken;
    //public GameObject course; //The current course
    public GameMode mode = GameMode.idle;

    
    // Start is called before the first frame update
    void Start()
    {
        S = this;

        level = 0;
        shotsTaken = 0;
        levelMax = 1;
        StartLevel();
        
    }

    void StartLevel() {
        //Get rid of old course if one exists
        //if( course != null ) {
            //Destroy( course );
        //}

        //GolfBall.DESTROY_GOLFBALL();

        //Instantiate new course
        //course = Instantiate<GameObject>( courses[level] );
       //course.transform.position = coursePos;

        //Reset the Goal
        Goal.goalMet = false;

        //UpdateGUI();

        mode = GameMode.playing;
    }

    // void UpdateGUI() {
    //     uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
    //     uitShots.text = "Shots Taken: " + shotsTaken;
    // }

    // Update is called once per frame
    void Update()
    {
        //UpdateGUI();

        //Check for level end
        if( (mode == GameMode.playing) && Goal.goalMet ) {
            mode = GameMode.levelEnd;

            if( level == 2 ) {
                Invoke( "SwitchToGameOver", 2f);
            }
            else {
                Invoke( "NextLevel", 2f);
            }
        }
    }

    void NextLevel() {
        level++;
        if( level == levelMax ) {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    void SwitchToGameOver() {
        SceneManager.LoadScene( "GameOver" );
    }

    static public void SHOT_FIRED() {
        S.shotsTaken++;
    }
}
