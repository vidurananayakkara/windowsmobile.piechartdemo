using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WPChartDemo
{
    [Table]
    public class Model : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string _title;

        [Column]
        public string Title
        {
            get { return _title; }
            set
            {
                NotifyPropertyChanging("Title");
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private int _value;

        [Column]
        public int Value
        {
            get { return _value; }
            set
            {
                NotifyPropertyChanging("Value");
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;

        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class ModelContext : DataContext
    {
        public ModelContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Model> Items;
    }
}
