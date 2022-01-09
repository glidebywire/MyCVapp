using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCV.Model;

namespace MyCV.ViewModel
{
    public class ExperienceItemViewModel : INotifyPropertyChanged
    {
        const string bulletChar = "•";
        public ExperienceItemViewModel(ExperienceItem model)
        {
            mExperienceItem = model;
            model.BulletJobTasks.ForEach(BulletJobTasks.Add);
            JobPosition = model.JobPosition;
            Company = model.Company;
            JobTimeSpan = model.JobTimeSpan;
            JobDescription = model.JobDescription;
        }

        #region properties

        ExperienceItem mExperienceItem;
        public ExperienceItem Experience
        {
            get => mExperienceItem;
        }

        string mJobPosition = "";
        public string JobPosition
        {
            get => mJobPosition;
            set
            {
                mJobPosition = value;
                OnPropertyChanged("JobPosition");
            }
        }

        string mCompany = "";
        public string Company
        {
            get => mCompany;
            set
            {
                mCompany = value;
                OnPropertyChanged("JobPosition");
            }
        }

        string mJobTimeSpan = "";
        public string JobTimeSpan
        {
            get => mJobTimeSpan;
            set
            {
                mJobTimeSpan = value;
                OnPropertyChanged("JobTimeSpan");
            }
        }

        string mJobDescription = "";
        public string JobDescription
        {
            get => mJobDescription;
            set
            {
                mJobDescription = value;
                OnPropertyChanged("JobDescription");
            }
        }

        ObservableCollection<string> mBulletJobTasks = new ObservableCollection<string>();
        public ObservableCollection<string> BulletJobTasks
        {
            get => mBulletJobTasks;
            set
            {
                mBulletJobTasks = value;
                OnPropertyChanged("BulletJobTasks");
            }
        }

        #endregion

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
