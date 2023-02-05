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
    public float shotValue;
    public string wasShotHit;
    public float shotProbability;
    public string difficulty;
    public string gameMode;
    public double priorScoreDiff;
    public double newScoreDiff;
    public double goal;
    //public string amtID;
    public bool unlimitedShots;
    public int ballsLeft;
    public int accuracy_correct;
    public int accuracy_min_shots;
    public int round_num_of_shots;
    public int round_num_of_movements;
    public int total_num_of_shots;
    public int total_num_of_movements;
    public int wps_correct;
    public int wps_min_shots;
    public int excess_shots;
    public float movement_time; 
    public float preplan_time;
    //public float total_round_time;
    public float total_game_time;

    // Start is called before the first frame update

    public Log(string action, string shotHit, double priorScore)
    {
        actionType = action;
        playerID = GameHandler.playerId;
        date_and_time = GameHandler.time;
        //shoot = GameHandler.lastAction;
        x_pos = Character.x_pos;
        y_pos = Character.y_pos;
        shotValue = Character.scoreFrom;
        shotProbability = Character.prob; 
        wasShotHit = shotHit;
        Score = GameHandler.Score;
        OldScore = priorScore;
        difficulty = GameHandler.difficulty;
        gameMode = GameHandler.GameMode;
        representation = GameHandler.representation;
        priorScoreDiff = GameHandler.goalScore - OldScore;
        newScoreDiff = GameHandler.goalScore - Score;
        goal = GameHandler.goalScore;
       // amtID = GameHandler.amtID;
        unlimitedShots = GameHandler.unlimitedShots;
        ballsLeft = GameHandler.ballsRemaining;
        accuracy_correct = GameHandler.accuracy_correct;
        round_num_of_shots = GameHandler.round_num_of_shots;
        round_num_of_movements = GameHandler.round_num_of_movements;
        accuracy_min_shots = GameHandler.accuracy_min_shots;
        total_num_of_shots = GameHandler.total_num_of_shots;
        total_num_of_movements = GameHandler.total_num_of_movements;
        wps_correct = GameHandler.wps_correct;
        wps_min_shots = GameHandler.wps_min_shots;
        excess_shots = GameHandler.excess_shots;
        movement_time = GameHandler.movement_time;
        preplan_time = GameHandler.preplan_time;
        //total_round_time = GameHandler.total_round_time;
        total_game_time = GameHandler.total_game_time;

}
}
