using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using foe_calc.Objects;
using System.Collections.Generic;

namespace foe_calc
{
    public class DBManager
    {
        SQLiteConnection conn;
        SQLiteCommand sql_command;
        string query;
        SQLiteDataReader reader;
        List<GBLevel> gb_lvls = new List<GBLevel>();
        List<GB> gbs = new List<GB>();



        /* CONSUTRCTUOR for checking if the connection can be made*/
        public DBManager() {
            conn = new SQLiteConnection("Data Source=Assets/database/foe_data.db;");
            try
            {
                conn.Open();
                Console.WriteLine("Connection has been successfully made!");
            }
            catch (Exception e) { Console.WriteLine(e.Message);}
        }

        /* Populates table with levels */
        public List<GBLevel> ReadLevelingData(string table) {
            query = "SELECT * FROM " + table;
            sql_command = new SQLiteCommand(query, conn);
            try {reader = sql_command.ExecuteReader(); }
            catch(Exception e) { Console.WriteLine(e.Message);}

            gb_lvls.Clear();
            while (reader.Read()) {
                gb_lvls.Add(new GBLevel(reader.GetInt16(0), reader.GetInt32(1), new Int32[]{ reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6) }));
                //Console.WriteLine(string.Format("{0} \t {1} \t [{2},{3},{4},{5},{6}]",reader["level"], reader["total_fp"], reader["p1"], reader["p2"], reader["p3"], reader["p4"], reader["p5"]));
            }
            return gb_lvls;
            //conn.Close();
        }

        
        public List<GB> ReadGBs() { /* Gets list of all GB for dropdown list element */
            query = "SELECT * FROM age_gb";
            sql_command = new SQLiteCommand(query, conn);
            try { reader = sql_command.ExecuteReader(); }
            catch(Exception e) { Console.WriteLine(e.Message); }

            gbs.Clear();
            while(reader.Read())
                gbs.Add(new GB(reader.GetString(1), reader.GetString(2),reader.GetString(3),reader.GetInt32(4), reader.GetString(5)));
            return gbs;

        }

        public string GetImage_lastGB(string lastGB)
        {/* used for img_gb on GUI on application start */
            query = "select * from age_gb WHERE gb_short = '" + lastGB + "'";
            sql_command = new SQLiteCommand(query, conn);
            try { reader = sql_command.ExecuteReader(); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            if (reader.Read())
                return reader.GetString(5);
            return "";
        }

        
        public GB Get_lastGB(string lastGB)
        {/* used for selecting last used GB on dropdown list */
            query = "select * from age_gb WHERE gb_short = '" + lastGB + "'";
            sql_command = new SQLiteCommand(query, conn);
            try { reader = sql_command.ExecuteReader(); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            if (reader.Read())
                return new GB(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5));
            return null;
        }


        public UserData GetUserData()
        {/* Get user data - last visited GB and level he generate string for */
            query = "select * from user_data";
            sql_command = new SQLiteCommand(query, conn);
            try { reader = sql_command.ExecuteReader(); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            if (reader.Read())
                return new UserData(reader.GetString(1), reader.GetInt16(2), reader.GetInt16(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt16(6), reader.GetInt16(7), reader.GetString(8));
            return null;
        }


        
        public void WriteUserData(int state, UserData ud)
        {/* There was a show/hide contribution checkbox earlier but was removed (hence 2-contribution)*/
            switch (state)
            {
                case 0: query = "UPDATE user_data SET prefix = '" + ud.Prex + "' WHERE id = 1"; break;
                case 1: query = "UPDATE user_data SET shortForm = " + ud.DisplayShort + " WHERE id = 1"; break;
                case 2: query = "UPDATE user_data SET showTransition = " + ud.LevelTransition + " WHERE id = 1"; break;
                case 3: query = "UPDATE user_data SET lastGb = '" + ud.Last_GB + "' WHERE id = 1"; break;
                case 4: query = "UPDATE user_data SET showPositions = " + ud.Positions + " WHERE id = 1"; break;
                case 5: query = "UPDATE user_data SET showGuide = " + ud.DisplayGuide + " WHERE id = 1"; break;
                case 6: query = "UPDATE user_data SET showRowColor = " + ud.EnableRowColor + " WHERE id = 1"; break;
                case 7: query = "UPDATE user_data SET rowColor = '" + ud.RowColor + "' WHERE id = 1"; break;
            }
            Console.WriteLine(string.Format("[DB_WRITE_UD] \t {0}", query));
            sql_command = new SQLiteCommand(query, conn);
            try { var updateRow = sql_command.ExecuteNonQuery(); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public void Update_GB_Level(string gb, int lvl)
        {/* called when user clicks row/button on table to generate string */
            query = "UPDATE age_gb SET gb_last_lvl = " + lvl + " WHERE gb_short='" + gb + "'";
            sql_command = new SQLiteCommand(query, conn);
            try { var updateRow = sql_command.ExecuteNonQuery(); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            Console.WriteLine(string.Format("[DB_Update_Level] \t {0}", query));
        }

        public void Update_GB_ShortName(string gb, string shortName)
        {/* called when user changes abbrevation field value */
            query = "UPDATE age_gb SET gb_short = '" + shortName + "' WHERE gb_short='" + gb + "'";
            sql_command = new SQLiteCommand(query, conn);
            try { var updateRow = sql_command.ExecuteNonQuery(); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            Console.WriteLine(string.Format("[DB_Update_SHORTFORM] \t {0}", query));
        }

        public int Get_GB_Level(string gb)
        {/* table should scroll user to the last level he clicked on specific GB */
            query =  "select * from age_gb WHERE gb_short ='" + gb + "'";
            sql_command = new SQLiteCommand(query, conn);
            try { reader = sql_command.ExecuteReader(); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            if (reader.Read())
                return reader.GetInt16(4);
            return -1;
        }
    }
}
