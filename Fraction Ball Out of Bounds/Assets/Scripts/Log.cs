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
        difficulty = GameGenerator.difficulty;
        gameMode = GameGenerator.GameMode;
        representation = GameGenerator.representation;
        priorScoreDiff = GameGenerator.goalScore - OldScore;
        newScoreDiff = GameGenerator.goalScore - Score;
        goal = GameGenerator.goalScore;
       // amtID = GameGenerator.amtID;
        unlimitedShots = GameGenerator.unlimitedShots;
        ballsLeft = GameGenerator.ballsRemaining;
        accuracy_correct = GameGenerator.accuracy_correct;
        round_num_of_shots = GameGenerator.round_num_of_shots;
        round_num_of_movements = GameGenerator.round_num_of_movements;
        accuracy_min_shots = GameGenerator.accuracy_min_shots;
        total_num_of_shots = GameGenerator.total_num_of_shots;
        total_num_of_movements = GameGenerator.total_num_of_movements;
        wps_correct = GameGenerator.wps_correct;
        wps_min_shots = GameGenerator.wps_min_shots;
        excess_shots = GameGenerator.excess_shots;
        movement_time = GameGenerator.movement_time;
        preplan_time = GameGenerator.preplan_time;
        //total_round_time = GameGenerator.total_round_time;
        total_game_time = GameGenerator.total_game_time;

}
}
