    public class GameState 
        {
            public string representation;
            public double goalScore;
            public string notation;
            public double initialScore;
            public bool limitedShots;
            public GameState(string representationName, double goalScoreValue, string notationValue, double startScore, bool limitShots) {
                representation = representationName;
                goalScore = goalScoreValue;
                notation = notationValue;
                initialScore = startScore;
                limitedShots = limitShots;
            }

            public GameState() {
                representation = "DECIMALS";
                goalScore = 0;
                notation = "fourths";
                initialScore = 0;
                limitedShots = true;
            }
        }

