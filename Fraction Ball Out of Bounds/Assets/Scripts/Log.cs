using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    public string actionType;
    public double Score;
    public double OldScore;
    public string playerID;
    public string timeStamp;
    //public string shoot;
    public string representation;
    public string notation;
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
    public float total_time;
    public int round_num_of_shots;
    public int round_num_of_movements;

    // Start is called before the first frame update

    public Log(string action, string shotHit, double priorScore)
    {
        actionType = action;
        playerID = GameGenerator.playerId;
        timeStamp = GameGenerator.time;
        //shoot = GameGenerator.lastAction;
        gameMode  = GameGenerator.GameSetting;
        notation = GameGenerator.notation;
        x_pos = Character.x_pos;
        y_pos = Character.y_pos;
        shotValue = Character.scoreFrom;
        shotProbability = Character.prob; 
        wasShotHit = shotHit;
        Score = GameGenerator.Score;
        OldScore = priorScore;
        representation = GameGenerator.GameMode;
        //priorScoreDiff = GameGenerator.goalScore - OldScore;
        //newScoreDiff = GameGenerator.goalScore - Score;
        goal = GameGenerator.goalScore;
        input = GameGenerator.user_input;
       // amtID = GameGenerator.amtID;
        unlimitedShots = GameGenerator.unlimitedShots;
        ballsLeft = GameGenerator.ballsRemaining;
        total_time = GameGenerator.total_game_time + GameGenerator.movement_time;
        round_num_of_shots = GameGenerator.round_num_of_shots;
        round_num_of_movements = GameGenerator.round_num_of_movements;

}
}
