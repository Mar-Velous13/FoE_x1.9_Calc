using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace foe_calc.Objects
{
    /* Object that holds information on a specific Great Building */
    public class GB
    {
        private string _image, _shortName, _age, _name;
        private int _level;

        public GB(string era, string name, string shortForm, int lastLvl, string imgSrc) {
            this.Name = name;
            this.Age = era;
            this.ShortName = shortForm;
            this.Level = lastLvl;
            this.Image = imgSrc;
        }

        /* Getters and setters */
        public string Name { get { return _name; } set { _name = value; } }

        public string Image  { get { return _image; } set { _image = value; } }

        public int Level { get { return _level; } set { _level = value; } }

        public string ShortName { get { return _shortName; } set { _shortName = value; } }

        public string Age { get { return _age; } set { _age = value; } }

        public string ListName
        {
            get { return this.GenerateAgeTag() +"\t"+ this.Name.Replace("_"," "); }
        }

        public Color ListColor
        {
            get { return this.GenerateListColor(); }
        }

        public Color TableColor { get { return this.GenerateTableColor(); } }


        
        string GenerateAgeTag()
        {/* Adjust list items (GB) so that they can be found/seen easier */
            switch (this.Age)
            {
                case "bronze": return "[BA]";
                case "iron": return "[IA] ";
                case "ema": return "[EMA] ";
                case "hma": return "[HMA] ";
                case "lma": return "[LMA] ";
                case "colonial": return "[CA] ";
                case "industrial": return "[INA] ";
                case "progressive": return "[PE] ";
                case "modern": return "[ME] ";
                case "post_modern": return "[PME] ";
                case "contemporary": return "[CE] ";
                case "tomorrow": return "[TE] ";
                case "future": return "[FE] ";
                case "arctic_future": return "[AF] ";
                case "oceanic_future": return "[OF] ";
                case "virtual_future": return "[VF] ";
                case "sa_mars": return "[SAM] ";
                case "sa_asteroid_belt": return "[SAAB] ";
                case "sa_jupiter_moon": return "[SAJM] ";
                case "sa_venus": return "[SAV] ";
                case "sa_titan": return "[SAT] ";
                case "sa_space_hub": return "[SASH] ";
                default: return "[ - ] ";
            }
        }

        public Color GenerateListColor()
        {/* Adjust list items (GB) so that they can be found/seen easier */
            switch (this.Age)
            {
                case "bronze": return Color.FromRgb(0xB4, 0x8B, 0x13);
                case "iron": return Color.FromRgb(0x9A, 0x47, 0x21);
                case "ema": return Color.FromRgb(0x50, 0x83, 0x44);
                case "hma": return Color.FromRgb(0x30, 0x8E, 0x8E);
                case "lma": return Color.FromRgb(0x80, 0x43, 0x89);
                case "colonial": return Color.FromRgb(0xCD, 0x64, 0x04);
                case "industrial": return Color.FromRgb(0xA6, 0x2A, 0x26);
                case "progressive": return Color.FromRgb(0xB6, 0x9A, 0x61);
                case "modern": return Color.FromRgb(0x5E, 0xA2, 0xE8);
                case "post_modern": return Color.FromRgb(0xA6, 0xB1, 0xA6);
                case "contemporary": return Color.FromRgb(0xFE, 0x8E, 0x6A);
                case "tomorrow": return Color.FromRgb(0x40, 0x43, 0x48);
                case "future": return Color.FromRgb(0x94, 0xC1, 0x67);
                case "arctic_future": return Color.FromRgb(0x00, 0x00, 0x00); //return Color.FromRgb(0xFF, 0xFF, 0xFF);
                case "oceanic_future": return Color.FromRgb(0x2D, 0x91, 0x76);
                case "virtual_future": return Color.FromRgb(0x55, 0x3A, 0x8B);
                case "sa_mars": return Color.FromRgb(0xBB, 0x65, 0x3D);
                case "sa_asteroid_belt": return Color.FromRgb(0x66, 0x00, 0x66);
                case "sa_jupiter_moon": return Color.FromRgb(0x2E, 0x67, 0x6B);
                case "sa_venus": return Color.FromRgb(0xDD, 0xC5, 0x00);
                case "sa_titan": return Color.FromRgb(0x25, 0x3F, 0x35);
                case "sa_space_hub": return Color.FromRgb(0x80, 0x00, 0x00);
                default: return Color.FromRgb(0x88, 0x6E, 0x42);
            }
        }

        public Color GenerateTableColor()
        {/* Adjust list items (GB) so that they can be found/seen easier */
            switch (this.Age)
            {
                case "bronze": return Color.FromRgb(0xD9, 0xC5, 0x89);
                case "iron": return Color.FromRgb(0xCC, 0xA3, 0x90);
                case "ema": return Color.FromRgb(0x84, 0xA8, 0x7C);
                case "hma": return Color.FromRgb(0x6E, 0xAF, 0x69);
                case "lma": return Color.FromRgb(0xC9, 0xAF, 0xCD);
                case "colonial": return Color.FromRgb(0xE1, 0xA2, 0x68);
                case "industrial": return Color.FromRgb(0xC0, 0x69, 0x67);
                case "progressive": return Color.FromRgb(0xB6, 0x9A, 0x61);
                case "modern": return Color.FromRgb(0x8E, 0xBD, 0xEE);
                case "post_modern": return Color.FromRgb(0xA6, 0xB1, 0xA6);
                case "contemporary": return Color.FromRgb(0xFE, 0xBB, 0xA5);
                case "tomorrow": return Color.FromRgb(0x9F, 0xA1, 0xA3);
                case "future": return Color.FromRgb(0x94, 0xC1, 0x67);
                case "arctic_future": return Color.FromRgb(0xCC, 0xCC, 0xCC); //return Color.FromRgb(0xFF, 0xFF, 0xFF);
                case "oceanic_future": return Color.FromRgb(0x81, 0xBD, 0xAC);
                case "virtual_future": return Color.FromRgb(0xAA, 0x9C, 0xC5);
                case "sa_mars": return Color.FromRgb(0xDD, 0xB2, 0x9E);
                case "sa_asteroid_belt": return Color.FromRgb(0xC9, 0xA5, 0xC9);
                case "sa_jupiter_moon": return Color.FromRgb(0x96, 0xB3, 0xB5);
                case "sa_venus": return Color.FromRgb(0xEA, 0xDC, 0x66);
                case "sa_titan": return Color.FromRgb(0x92, 0x9F, 0x9A);
                case "sa_space_hub": return Color.FromRgb(0xBF, 0x7F, 0x7F);
                default: return Color.FromRgb(0xB7, 0xA8, 0x8D);
            }
        }

    }
}
