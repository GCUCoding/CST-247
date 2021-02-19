using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Milestone_Project.Models.UserInfo
{
    public class PlayerStats : IComparable<PlayerStats>
    {
        public int FinalScore;
        public string Initials;
        public string Difficulty;
        public double Time;
        public string highscoresFilePath = AppContext.BaseDirectory + @"\Resources\DataFiles\HighScores.txt";

        public PlayerStats(int finalScore, string initials, string difficulty, double time)
        {
            FinalScore = finalScore;
            Initials = initials;
            Difficulty = difficulty;
            Time = time;
        }


        /// <summary>
        /// add new score to file
        /// </summary>
        /// <param name="finalScore"></param>
        /// <param name="initials"></param>
        /// <param name="difficulty"></param>
        /// <param name="time"></param>
        public void AddScore(int finalScore, string initials, string difficulty, int time)
        {
            string newScore = initials.ToString() + ", " + finalScore.ToString() + ", " + difficulty.ToString()
                + ", " + time.ToString() + Environment.NewLine;
            File.AppendAllText(highscoresFilePath, newScore);
        }

        /// <summary>
        /// reads scores from a file then returns a datatable
        /// </summary>
        /// <param name="gameScores"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable PopulateDataGrid(List<PlayerStats> gameScores, DataTable dt)
        {
            //Define the variables
            string line = "";

            //Read in the comma deliminated file, parse out the data and put it in a list
            using (StreamReader file = new StreamReader(highscoresFilePath))
            {
                //loop through all lines and add each each line to the list
                while ((line = file.ReadLine()) != null)
                {
                    string[] array = line.Split(',');
                    string playerInitials = array[0];
                    int score = Convert.ToInt32(array[1]);
                    string playerDifficulty = array[2];
                    int timeInSeconds = Convert.ToInt32(array[3]);

                    //---------------------------------------------------------------------------------------------------------------
                    //Add a new player and score to the list
                    gameScores.Add(new PlayerStats(score, playerInitials, playerDifficulty, timeInSeconds));
                }
            }
            //---------------------------------------------------------------------------------------------------------------
            // Apply LINQ to the List
            var highscores = (from gameScore in gameScores
                              where gameScore.FinalScore > 0
                              orderby gameScore.FinalScore descending
                              select new
                              {
                                  PlayerInitials = gameScore.Initials,
                                  PlayerScore = gameScore.FinalScore,
                                  PlayerDifficulty = gameScore.Difficulty,
                                  PlayerTime = gameScore.Time
                              }).Take(5);
            dt.Columns.Add("Initials", typeof(string));
            dt.Columns.Add("Score", typeof(int));
            dt.Columns.Add("Difficulty", typeof(string));
            dt.Columns.Add("Time (Seconds)", typeof(int));

            //---------------------------------------------------------------------------------------------------------------
            //Populate datatable from linq query
            foreach (var item in highscores)
            {
                // Populate data table
                dt.Rows.Add(item.PlayerInitials, item.PlayerScore, item.PlayerDifficulty, item.PlayerTime);
            }
            return dt;
        }

        public int CompareTo([AllowNull] PlayerStats other)
        {
            return other.FinalScore.CompareTo(FinalScore);
        }
    }
}
