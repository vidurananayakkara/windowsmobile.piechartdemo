using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace WPChartDemo
{
    public class ModelViewModel : INotifyPropertyChanged
    {

        private readonly ModelContext _modelDbModel;

        public ModelViewModel(string toDoDbConnectionString)
        {
            _modelDbModel = new ModelContext(toDoDbConnectionString);
        }

        public void SaveChangesToDb()
        {
            _modelDbModel.SubmitChanges();
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
        private ObservableCollection<Model> _models;
        public ObservableCollection<Model> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                NotifyPropertyChanged("Models");
            }
        }

        public void LoadCollectionsFromDatabase()
        {
            var modelsInDb = from Model model in _modelDbModel.Items
                             select model;

            Models = new ObservableCollection<Model>(modelsInDb);
        }

        public void AddModelItem(Model model)
        {
            _modelDbModel.Items.InsertOnSubmit(model);
            _modelDbModel.SubmitChanges();
            Models.Add(model);
        }

        public void DeleteModelItem(Model modelForDelete)
        {
            Models.Remove(modelForDelete);
            _modelDbModel.Items.DeleteOnSubmit(modelForDelete);
            _modelDbModel.SubmitChanges();
        }
    }
}
