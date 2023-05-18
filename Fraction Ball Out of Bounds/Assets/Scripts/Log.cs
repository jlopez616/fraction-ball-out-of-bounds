using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    public string actionType;
    public double Score;
    public double OldScore;
    public string playerID;
    public string date_and_time;
    //public string shoot;
    public string representation;
    public float x_pos;
    public float y_pos;
    public double shotValue;
    public string wasShotHit;
    public float shotProbability;
    public string gameMode;
    public double priorScoreDiff;
    public double newScoreDiff;
    public double goal;
    public string input;
    //public string amtID;
    public bool unlimitedShots;
    public int ballsLeft;

    // Start is called before the first frame update

    public Log(string action, string shotHit, double priorScore)
    {
        actionType = action;
        playerID = GameGenerator.playerId;
        date_and_time = GameGenerator.time;
        //shoot = GameGenerator.lastAction;
        x_pos = Character.x_pos;
        y_pos = Character.y_pos;
        shotValue = Character.scoreFrom;
        shotProbability = Character.prob; 
        wasShotHit = shotHit;
        Score = GameGenerator.Score;
        OldScore = priorScore;
        gameMode = GameGenerator.GameMode;
        representation = GameGenerator.representation;
        priorScoreDiff = GameGenerator.goalScore - OldScore;
        newScoreDiff = GameGenerator.goalScore - Score;
        goal = GameGenerator.goalScore;
        input = GameGenerator.user_input;
       // amtID = GameGenerator.amtID;
        unlimitedShots = GameGenerator.unlimitedShots;
        ballsLeft = GameGenerator.ballsRemaining;

}
}
