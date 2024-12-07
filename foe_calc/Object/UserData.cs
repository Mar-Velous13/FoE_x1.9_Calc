using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foe_calc.Objects
{
    /* Object holding values of user preferences */
    public class UserData
    {
        int positions, shortForm, levelTransition, showRowColor;
        string lastGB, prefix, rowColor;
        char[] positionValues;
        bool showGuide = false;

        public UserData(string pre, int shortF, int transition, int pos, string gb, int guide, int showColor, string color)
        {
            this.Positions = pos;
            this.DisplayShort = shortF;
            this.Prex = pre;
            this.Last_GB = gb;
            this.DisplayGuide = guide;
            this.LevelTransition = transition;
            EnableRowColor = showColor;
            RowColor = color;
        }
        /* Getters and Setters */
        public int Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                positionValues = positions.ToString().ToCharArray();
            }
        }

        public char GetPosition(int pos)
        {
            return positionValues[pos];
        }

        public int EnableRowColor// table row coloring
        {
            get{ return showRowColor; }
            set { showRowColor = value; }
        }

        public string RowColor// table row coloring
        {
            get { return rowColor; }
            set { rowColor = value; }
        }


        public void SetSinglePosition(int pos)
        {
            positionValues[pos] = positionValues[pos].Equals('1') ? '2' : '1';
            positions = Int32.Parse(new string(positionValues));
        }

        public int DisplayShort
        {
            get { return shortForm; }
            set { shortForm = value; }
        }

        public int LevelTransition
        {
            get { return levelTransition; }
            set { levelTransition = value; }
        }

        public string Last_GB
        {
            get { return lastGB; }
            set { lastGB = value; }
        }

        public string Prex
        {
            get { return prefix; }
            set { prefix = value; }
        }

        public int DisplayGuide
        {
            get { return showGuide ? 1 : 0; }
            set { showGuide = value == 1 ? true : false; }
        }
    }
}
