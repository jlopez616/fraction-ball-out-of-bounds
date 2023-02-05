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
        playerID = TaskGenerator.playerId;
        date_and_time = TaskGenerator.time;
        //shoot = TaskGenerator.lastAction;
        x_pos = Character.x_pos;
        y_pos = Character.y_pos;
        shotValue = Character.scoreFrom;
        shotProbability = Character.prob; 
        wasShotHit = shotHit;
        Score = TaskGenerator.Score;
        OldScore = priorScore;
        difficulty = TaskGenerator.difficulty;
        gameMode = TaskGenerator.GameMode;
        representation = TaskGenerator.representation;
        priorScoreDiff = TaskGenerator.goalScore - OldScore;
        newScoreDiff = TaskGenerator.goalScore - Score;
        goal = TaskGenerator.goalScore;
       // amtID = TaskGenerator.amtID;
        unlimitedShots = TaskGenerator.unlimitedShots;
        ballsLeft = TaskGenerator.ballsRemaining;
        accuracy_correct = TaskGenerator.accuracy_correct;
        round_num_of_shots = TaskGenerator.round_num_of_shots;
        round_num_of_movements = TaskGenerator.round_num_of_movements;
        accuracy_min_shots = TaskGenerator.accuracy_min_shots;
        total_num_of_shots = TaskGenerator.total_num_of_shots;
        total_num_of_movements = TaskGenerator.total_num_of_movements;
        wps_correct = TaskGenerator.wps_correct;
        wps_min_shots = TaskGenerator.wps_min_shots;
        excess_shots = TaskGenerator.excess_shots;
        movement_time = TaskGenerator.movement_time;
        preplan_time = TaskGenerator.preplan_time;
        //total_round_time = TaskGenerator.total_round_time;
        total_game_time = TaskGenerator.total_game_time;

}
}
