using foe_calc.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace foe_calc.ViewModel
{
    public class GBLViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GBLevel> _gbLevels { get; set; }
        public ObservableCollection<GBLevel> GBLevels
        {
            get { return _gbLevels; }
            set
            {
                _gbLevels = value;
                RaisePropertyChanged(nameof(GBLevels));
            }
        }

        public void LoadLevels(DBManager db, string gb_age)
        {
            Console.WriteLine(string.Format("[GBL-VM] Age:{0} Entries:{1}", gb_age, db.ReadLevelingData(gb_age).Count));
            GBLevels = new ObservableCollection<GBLevel>(db.ReadLevelingData(gb_age));
            //Console.WriteLine(string.Format("[GBL-VM-Loaded] size:{0}, lastGB:{1}",GBLevels.Count, gb_age));
        }

        public void ReloadLevels(DBManager db, string age)
        {
            GBLevels.Clear();
            foreach (GBLevel gbl in db.ReadLevelingData(age))
                GBLevels.Add(gbl);//Collection.Add()/Remove() triggers the update event
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


    }
}
