using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace foe_calc.Objects
{
    public class GBLevel
    {
        private int lvl, total, contribs;
        private int[] positions = new int[5], secure = new int[5];

        public GBLevel(int level, int fp_total, int [] fp_positions)
        {
            this.Level = level;
            this.Total = fp_total;
            this.Positions = fp_positions;
            this.CalculateSecuringCosts();
        }

        /* ======================== GETTERS & SETTERS =================*/
        public int Level
        {
            get { return lvl; }
            set { lvl = value;
            }
        }

        public int Total
        {
            get { return total; }
            set { total = value;
            }
        }

        public int [] Positions
        {
            get { return positions; }
            set { positions = value;
                CalculateSecuringCosts();

            }
        }

        public int Pos1 { get { return positions[0]; } }
        public int Pos2 { get { return positions[1]; } }
        public int Pos3 { get { return positions[2]; } }
        public int Pos4 { get { return positions[3]; } }
        public int Pos5 { get { return positions[4]; } }

        public string Sec1 { get { return FormatSecureValue(0); } }
        public string Sec2 { get { return FormatSecureValue(1); } }
        public string Sec3 { get { return FormatSecureValue(2); } }
        public string Sec4 { get { return FormatSecureValue(3); } }
        public string Sec5 { get { return FormatSecureValue(4); } }

        public Color Sec1Col { get { return secure[0] > 0 ? Color.FromRgb(0xFF, 0x00, 0x00) : Color.FromRgb(0x20, 0x7D, 0x1D); } }
        public Color Sec2Col { get { return secure[1] > 0 ? Color.FromRgb(0xFF, 0x00, 0x00) : Color.FromRgb(0x20, 0x7D, 0x1D); } }
        public Color Sec3Col { get { return secure[2] > 0 ? Color.FromRgb(0xFF, 0x00, 0x00) : Color.FromRgb(0x20, 0x7D,0x1D); } }
        public Color Sec4Col { get { return secure[3] > 0 ? Color.FromRgb(0xFF, 0x00, 0x00) : Color.FromRgb(0x20, 0x7D, 0x1D); } }
        public Color Sec5Col { get { return secure[4] > 0 ? Color.FromRgb(0xFF, 0x00, 0x00) : Color.FromRgb(0x20, 0x7D, 0x1D); } }
        // 32, 125, 29 0x20,0x7D,0x1D


        string FormatSecureValue(int id)
        {
            if (positions[id] == 0) return "";
            else return string.Format("{0}{1}", secure[id] > 0 ? "+" : "", secure[id]);
        }

        public void SetPosition(int id, int value)
        {
            positions[id] = value;
        }

        void CalculateSecuringCosts()
        {
            for(var i=0; i < 5; i++)
            {//loop through all 5 positions
                contribs = 0;
                for(var j=i-1; j >= 0; j--)
                {//e.g. P4 has to check previous positions (P1,P2,P3) to check how much has been contributed already
                    contribs += positions[j];
                }
                secure[i] = total - positions[i] * 2 - contribs;
            }
        }


    }
}
