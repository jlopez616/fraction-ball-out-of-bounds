    public class GameState 
        {
            public string representation;
            public string goalScore; // goalScore should be in the format "number numerator/denominator" or "a b/c" or "a" or "0"
            public string notation;
            public double initialScore;
            public bool limitedShots;
            public string timeOfDay;
            public string gameSetting;
            public GameState(string representationName, string goalScoreValue, string notationValue, double startScore, bool limitShots, string timeOfDayValue,string Setting = "EXACTLY") {
                representation = representationName; //string, "FRACTIONS" or "DECIMALS"
                goalScore = goalScoreValue; // if zero, randomly generated; otherwise, we define this here
                notation = notationValue; //fourths, thirds, sixths, eighths, etc. = whatever our denominator is supposed to be
                initialScore = startScore; // right now all zero
                limitedShots = limitShots; //Boolean, whether or not the player is guaranteed to score
                timeOfDay = timeOfDayValue; //String, "day" or "night"
                gameSetting = Setting; // String , "EXACTLY" , "RAPID FIRE"
            }

            public GameState() {
                representation = "DECIMALS";
                goalScore = "0";
                notation = "fourths";
                initialScore = 0;
                limitedShots = true;
                timeOfDay = "day";
                gameSetting = "EXACTLY";
            }
        }

