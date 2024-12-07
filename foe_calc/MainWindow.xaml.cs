using foe_calc.forms;
using foe_calc.Objects;
using foe_calc.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace foe_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //objects
        DBManager db;
        UserData ud;
        GB temp_gb;
        GBViewModel gb_vm;
        GBLViewModel gbl_vm;
        Guide guide;

        //variables
        int ListItemSelected = 0;
        bool initConcluded = false, changingGB = false;
        string[] outputString = new string[] { "", "", " P5", " P4", " P3", " P2", " P1" };
        string tempString;

        /// <summary>
        /// Initialize objects (including database), load and set gui elements
        /// </summary>
        public MainWindow()//constructor
        {
            db = new DBManager();
            ud = db.GetUserData();
            InitializeComponent();

            PrefixBox.Text = ud.Prex;
            outputString[0] = ud.Prex;
            LoadList();
            ListItemSelected = LastSelectedGB();
            LoadTable();
            SetCheckBoxes();
            this.initConcluded = true;//used to prevent triggering events before everything has loaded correctly
            UpdateOutputString();
            Abbrev.Text = ud.Last_GB;
            DisplayGuide(false);
        }

        int LastSelectedGB()/*return index of lastGB, used to assign value to gbList.selectedIndex*/
        {
            foreach (GB gb in gb_vm.GBS)
            {
                if (ud.Last_GB.Equals(gb.ShortName)) return gb_vm.GBS.IndexOf(gb);
            }
            return -1;
        }


        /* Upon application start look for user settings regarding the checkbox states and checks/unchecks them*/
        void SetCheckBoxes()
        {
            LevelTrans.IsChecked = ud.LevelTransition == 1;
            CheckShort.IsChecked = ud.DisplayShort == 1;
            CheckRowColor.IsChecked = ud.EnableRowColor == 1;
            SetRowAltColor();
            outputString[1] = ud.DisplayShort == 1 ? FindLastGB(ud.Last_GB).ShortName : FindLastGB(ud.Last_GB).Name;
            CheckP5.IsChecked = ud.GetPosition(0).Equals('1'); outputString[2] = ud.GetPosition(0).Equals('1') ? " P5()" : "";
            CheckP4.IsChecked = ud.GetPosition(1).Equals('1'); outputString[3] = ud.GetPosition(1).Equals('1') ? " P4()" : "";
            CheckP3.IsChecked = ud.GetPosition(2).Equals('1'); outputString[4] = ud.GetPosition(2).Equals('1') ? " P3()" : "";
            CheckP2.IsChecked = ud.GetPosition(3).Equals('1'); outputString[5] = ud.GetPosition(3).Equals('1') ? " P2()" : "";
            CheckP1.IsChecked = ud.GetPosition(4).Equals('1'); outputString[6] = ud.GetPosition(4).Equals('1') ? " P1()" : "";
        }

        void UpdateOutputString()
        {/* Updates output string on gui, seen in field at left side panel below prefix */
            OutputBox.Text = string.Format("{0} {1} {2}{3}{4}{5}{6}{7}", 
                outputString[0], 
                outputString[1], 
                (ud.LevelTransition == 1) ? "X → Y" : "", 
                outputString[2], 
                outputString[3], 
                outputString[4], 
                outputString[5], 
                outputString[6]);
        }

        GB FindLastGB(string lastGB)
        {/* Used to access various fields on last used GB (age, shortname, etc)*/
            foreach (GB gb in gb_vm.GBS)
            {
                if (gb.ShortName.Equals(lastGB)) return gb;
            }
            return null;
        }

        void LoadList()//Load list of great buildings (side panel)
        {
            gb_vm = new GBViewModel();
            gb_vm.LoadGreatBuildings(db);

            ListItemSelected = LastSelectedGB();
            gb_list.ItemsSource = gb_vm.GBS;
            gb_list.Items.Refresh();
        }

        void LoadTable()//load leveling data on great building (table)
        {
            gbl_vm = new GBLViewModel();
            gbl_vm.LoadLevels(db, FindLastGB(ud.Last_GB).Age);

            tableData.ItemsSource = gbl_vm.GBLevels;
            tableData.Items.Refresh();
        }


        void SetRowAltColor()//set table row color(true) or just color picker button's background (false)
        {
            var converter = new BrushConverter();
            if (ud.EnableRowColor == 1)
                tableData.AlternatingRowBackground = (SolidColorBrush)converter.ConvertFromString(ud.RowColor);
            else
                tableData.AlternatingRowBackground = tableData.Background;
            rowColor.Background = (SolidColorBrush)converter.ConvertFromString(ud.RowColor);

        }

        /* Hide or display guide for user*/
        void DisplayGuide(bool requested)
        {
            guide = new Guide();
            if (ud.DisplayGuide == 1)
            {
                ud.DisplayGuide = 0;
                db.WriteUserData(5, ud);
                guide.showGuide();
            }
            else if(requested == true) guide.showGuide();
        }

 /* ========================================== onCLICK & OTHER EVENTS =====================================*/

        private void List_Loaded(object sender, RoutedEventArgs e)
        {//convert from ud.last_gb to int to assign correct selected building
            gb_list.SelectedIndex = LastSelectedGB();
        }

        private void ListSelectionChanged(object sender, RoutedEventArgs e)
        {/* when list items are changed */

            if (!initConcluded) return;// restrict unneccessary DB writing on component init
            changingGB = true;
            temp_gb = (GB)gb_list.SelectedItem;
            gb_img.Source = new BitmapImage(new Uri("/foe_calc;component/Assets/images/" + temp_gb.Image, UriKind.Relative));
            ud.Last_GB = temp_gb.ShortName;
            db.WriteUserData(3, ud);
            Abbrev.Text = temp_gb.ShortName;
            outputString[1] = (CheckShort.IsChecked == true) ? ud.Last_GB : FindLastGB(ud.Last_GB).Name;


            //GB has changed so we need to load correct leveling data for it
            gbl_vm.ReloadLevels(db, temp_gb.Age);
            tableData.Items.Refresh();

            this.UpdateOutputString();
            changingGB = false;
        }

        private void Prefix_TextChanged(object sender, TextChangedEventArgs e)
        {/* Save new users username/prefix, won't have to type it every time the application is opened */
            if (!initConcluded) { outputString[0] = ud.Prex; return; }// restrict unneccessary DB writing on component init
            ud.Prex = PrefixBox.Text;
            db.WriteUserData(0, ud);//update database
            outputString[0] = ud.Prex;
            this.UpdateOutputString();//change output string
        }

        private void Abbrev_TextChanged(object sender, TextChangedEventArgs e)
        {/* Save new users username/prefix, won't have to type it every time the application is opened */
            //Console.WriteLine(string.Format("[Abbrev] Status:{0}{1}\tText:{2}\tFull_Name:{3}", CheckShort.IsChecked, CheckShort.IsChecked == (bool?) true, Abbrev.Text, FindLastGB(ud.Last_GB).Name));
            if (!initConcluded || changingGB)// restrict unneccessary DB writing on component init
            {
                outputString[1] = (CheckShort.IsChecked == true) ? ud.Last_GB : FindLastGB(ud.Last_GB).Name;
                return;
            }

            ud.Last_GB = Abbrev.Text;
            db.Update_GB_ShortName(ud.Last_GB, Abbrev.Text);
            db.WriteUserData(3, ud);
            for (var i = 0; i < gb_vm.GBS.Count; i++)
            {//collection must be edited or else won't match newly changed shortName of great building
                temp_gb = gb_vm.GBS.ElementAt(i);
                if (temp_gb.ShortName.Contains(Abbrev.Text) || Abbrev.Text.Contains(temp_gb.ShortName))
                {
                    gb_vm.GBS.ElementAt(i).ShortName = Abbrev.Text; break;
                }
            }

            outputString[1] = (CheckShort.IsChecked == true) ? Abbrev.Text : FindLastGB(ud.Last_GB).Name;
            this.UpdateOutputString();//change output string
        }


        /* Updating info when user clicks on any of the checkboxes*/
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox cb = (System.Windows.Controls.CheckBox)sender;
            switch (cb.Name)
            {
                case "CheckP1":
                    ud.SetSinglePosition(4);
                    db.WriteUserData(4, ud);
                    outputString[6] = ud.GetPosition(4).Equals('1') ? " P1()" : "";
                    break;
                case "CheckP2":
                    ud.SetSinglePosition(3);
                    db.WriteUserData(4, ud);
                    outputString[5] = ud.GetPosition(3).Equals('1') ? " P2()" : "";
                    break;
                case "CheckP3":
                    ud.SetSinglePosition(2);
                    db.WriteUserData(4, ud);
                    outputString[4] = ud.GetPosition(2).Equals('1') ? " P3()" : "";
                    break;
                case "CheckP4":
                    ud.SetSinglePosition(1);
                    db.WriteUserData(4, ud);
                    outputString[3] = ud.GetPosition(1).Equals('1') ? " P4()" : "";
                    break;
                case "CheckP5":
                    ud.SetSinglePosition(0);
                    db.WriteUserData(4, ud);
                    outputString[2] = ud.GetPosition(0).Equals('1') ? " P5()" : "";
                    break;

                case "CheckShort":
                    Abbrev.IsEnabled = CheckShort.IsChecked == true ? true : false;
                    ud.DisplayShort = ud.DisplayShort == 1 ? 0 : 1;
                    db.WriteUserData(1, ud);
                    foreach(GB gb in gb_vm.GBS)
                    {
                        if (gb.ShortName.Equals(ud.Last_GB))
                        {
                            outputString[1] = ud.DisplayShort == 1 ? gb.ShortName : gb.Name;
                            break;
                        }
                    }
                    break;

                case "LevelTrans"://display next level
                    ud.LevelTransition = ud.LevelTransition == 1 ? 0 : 1;
                    db.WriteUserData(2, ud);
                    break;

                case "CheckRowColor"://enable table row custom color
                    ud.EnableRowColor = ud.EnableRowColor == 1 ? 0 : 1;
                    db.WriteUserData(6, ud);
                    SetRowAltColor();
                    break;
            }
            UpdateOutputString();
        }


        private void TableData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//Event triggered when user CLICKS ON ROW a.k.a. string generation
            GBLevel gbl = (GBLevel)tableData.SelectedItem;
            if (gbl == null) return;//was triggered when changing great building from the list, caused error

            //generate string to copy to clipboard
            tempString = string.Format("{0} {1} {7}{2}{3}{4}{5}{6}", outputString[0], outputString[1],
                CheckP5.IsChecked == true ? " P5(" + gbl.Pos5 + ")" : "",
                CheckP4.IsChecked == true ? " P4(" + gbl.Pos4 + ")" : "",
                CheckP3.IsChecked == true ? " P3(" + gbl.Pos3 + ")" : "",
                CheckP2.IsChecked == true ? " P2(" + gbl.Pos2 + ")" : "",
                CheckP1.IsChecked == true ? " P1(" + gbl.Pos1 + ")" : "",
                LevelTrans.IsChecked == true ? (gbl.Level-1)+ " → " + gbl.Level : "");

            System.Windows.Forms.Clipboard.SetText(tempString);
            db.Update_GB_Level(ud.Last_GB, gbl.Level);

            //show notification
            new Notification("String has been generated for level "+gbl.Level+"! Now use Ctrl+V");
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "DISCLAIMER. The materials used in this application are purely for informational purposes. I do not claim ownership of the media used, and no copyright infringement is intended. All rights go to their respective owners."
                + Environment.NewLine + Environment.NewLine + "This application was made by Mar. Contact me at:"+Environment.NewLine+"> Mar13 on Noarsil & Odhrorvar"+Environment.NewLine+"> E-mail: vict100@mail.com";
            string title = "Application Info!";
            MessageBoxButton buttons = MessageBoxButton.OK;
            System.Windows.MessageBox.Show(message, title, buttons, MessageBoxImage.Information);
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayGuide(true);
        }


        private void OpenColorPicker(object sender, RoutedEventArgs e)
        {
            var col = ((SolidColorBrush)rowColor.Background).Color;

            ColorDialog MyDialog = new ColorDialog
            {
                // Keeps the user from selecting a custom color.
                AllowFullOpen = true,
                // Allows the user to get help. (The default is false.)
                ShowHelp = false,
                // Sets the initial color select to the current text color.
                Color = System.Drawing.Color.FromArgb(col.A, col.R, col.G, col.B)
            };

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ud.RowColor = string.Format("{0}", new SolidColorBrush(Color.FromArgb(MyDialog.Color.A, MyDialog.Color.R, MyDialog.Color.G, MyDialog.Color.B)));
                db.WriteUserData(7, ud);
                rowColor.Background = new SolidColorBrush(Color.FromArgb(MyDialog.Color.A, MyDialog.Color.R, MyDialog.Color.G, MyDialog.Color.B));
                SetRowAltColor();
            }
        }



    }
}
