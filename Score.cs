using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Author: Max Weber
 * Statement: This is my own work
 * Teacher: Bill Hughes
 * Class: CST-227
 */
namespace Minesweeper
{
    public class Score : IComparable<Score>
    {
        //defines properties
        public string Name { get; }
        public string Difficulty { get; }
        public int Value { get; }
        public string GameTime {get;set;}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="difficulty"></param>
        /// <param name="value"></param>
        /// <param name="gameTime"></param>
        public Score(string name, string difficulty, int value, string gameTime)
        {
            //initializes properties
            Name = name;
            Difficulty = difficulty;
            Value = value;
            GameTime = gameTime;
        }
        /// <summary>
        /// Allows sorting using the IComparable interface
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int CompareTo(Score s)
        {
            return Value.CompareTo(s.Value);
        }
    }
}
