using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;
using MyCV.Model;
using System.Windows.Input;
using System.Windows;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyCV.ViewModel
{
    public class PrintCommand : ICommand
    {
        Window mWindow;

        public void SetPrintTarget(Window printTarget)
        {
            mWindow = printTarget;
        }

        public void Execute(object parameter)
        {            
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {                
                printDlg.PrintVisual(mWindow, "Window Printing.");
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class CVviewModel : INotifyPropertyChanged
    {
        JsonCV mJsonCV;
        Window mWindow;

        public CVviewModel()
        {
            /* Load from file if any */
            if (!LoadResumeFromFile())
            {                
                InitDefault();
                SaveResumeToFIle();
            }
            else
            {
                InitFromFile();
            }
        }

        #region commands

        PrintCommand _printCommand = new PrintCommand();

        public ICommand PrintCmd
        {
            get { return _printCommand; }
        }

        #endregion

        #region properties

        ImageSource mPicture;
        public ImageSource Picture
        {
            get => mPicture;
            set
            {
                mPicture = value;
                OnPropertyChanged("Picture");
            }
        }

        string mName = "JOHN";
        public string Name
        {
            get => mName;
            set
            {
                mName = value;
                OnPropertyChanged("Name");
            }
        }

        string mSurName = "DOE";
        public string SurName
        {
            get => mSurName;
            set
            {
                mSurName = value;
                OnPropertyChanged("SurName");
            }
        }

        string mTitle = "GENERAL MANAGER";
        public string Title
        {
            get => mTitle;
            set
            {
                mTitle = value;
                OnPropertyChanged("Title");
            }
        }

        string mProfile = "I am an energetic, ambitious person who has developed a mature and responsible approach to any task that I undertake, or situation that I am presented with. As a graduate with three years’ experience in management, I am excellent in working with others to achieve a certain objective on time and with excellence.";
        public string Profile
        {
            get => mProfile;
            set
            {
                mProfile = value;
                OnPropertyChanged("Profile");
            }
        }

        ObservableCollection<ExperienceItemViewModel> mExperiences = new ObservableCollection<ExperienceItemViewModel>();
        public ObservableCollection<ExperienceItemViewModel> Experiences
        {
            get => mExperiences;
            set
            {
                mExperiences = value;
                OnPropertyChanged("Experiences");
            }
        }


        ObservableCollection<ContactItem> mContactItems = new ObservableCollection<ContactItem>();
        public ObservableCollection<ContactItem> ContactItems
        {
            get => mContactItems;
            set
            {
                mContactItems = value;
                OnPropertyChanged("ContactItems");
            }
        }

        ObservableCollection<EducationItem> mEducationItems = new ObservableCollection<EducationItem>();
        public ObservableCollection<EducationItem> EducationItems
        {
            get => mEducationItems;
            set
            {
                mEducationItems = value;
                OnPropertyChanged("EducationItems");
            }
        }

        ObservableCollection<SkillItem> mSkills = new ObservableCollection<SkillItem>();
        public ObservableCollection<SkillItem> Skills
        {
            get => mSkills;
            set
            {
                mSkills = value;
                OnPropertyChanged("Skills");
            }
        }

        #endregion

        #region methods

        public void SetParentView(Window view)
        {
            mWindow = view;
            _printCommand.SetPrintTarget(view);
        }

        void InitFromFile()
        {           
            Name = mJsonCV.Applicant.Name;
            SurName = mJsonCV.Applicant.SurName;
            Title = mJsonCV.Applicant.Title;
            Profile = mJsonCV.Applicant.Profile;
            var GetDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(GetDirectory, mJsonCV.Applicant.Picture);
            Picture = new BitmapImage(new Uri(filePath, UriKind.Absolute)); ;
            mJsonCV.Contacts.ForEach(ContactItems.Add);
            mJsonCV.Educations.ForEach(EducationItems.Add);
            mJsonCV.Skills.ForEach(Skills.Add);
            foreach(ExperienceItem exp in mJsonCV.Experiences)
            {
                Experiences.Add(new ExperienceItemViewModel(exp));
            }
        }

        void InitDefault()
        {
            ContactItems.Add(new ContactItem() { ContactKey = "Phone:", ContactValue = "+012 3456 789" });
            ContactItems.Add(new ContactItem() { ContactKey = "Email:", ContactValue = "examplemail@example.com" });
            ContactItems.Add(new ContactItem() { ContactKey = "Website:", ContactValue = "example.website.com" });
            ContactItems.Add(new ContactItem() { ContactKey = "Address:", ContactValue = "Your street address here" });

            EducationItems.Add(new EducationItem() { Year = "2012", Degree = "Your Degree name", Institution = "Institution" });
            EducationItems.Add(new EducationItem() { Year = "2010", Degree = "Your Degree name", Institution = "Institution" });
            EducationItems.Add(new EducationItem() { Year = "2008", Degree = "Your Degree name", Institution = "Institution" });

            Skills.Add(new SkillItem() { SkillName = "Microsoft Manager Suite", SkillValue = 70 });
            Skills.Add(new SkillItem() { SkillName = "Microsoft Manager Suite", SkillValue = 40 });
            Skills.Add(new SkillItem() { SkillName = "Microsoft Manager Suite", SkillValue = 80 });
            Skills.Add(new SkillItem() { SkillName = "Microsoft Manager Suite", SkillValue = 90 });
            Skills.Add(new SkillItem() { SkillName = "Microsoft Manager Suite", SkillValue = 40 });

            List<string> bullets = new List<string>();
            bullets.Add("• Include a suitable amount of relevant experiences.");
            bullets.Add("• Emphasize accomplishments over work duties.");
            bullets.Add("• Use action-benefit statements to describe your achievements.");
            bullets.Add("• Tailor your content to the position.");
            bullets.Add("• Make it easily readable.");            
                        
            Experiences.Add(new ExperienceItemViewModel(new ExperienceItem()
            {
                Company = "The Company / institute",
                JobDescription = "A job description summarizes the essential responsibilities, activities, qualifications and skills for a role. Also known as a JD, this document describes the type of work performed.",
                JobPosition = "GENERAL MANAGER",
                JobTimeSpan = "2017 - 2021",
                BulletJobTasks = bullets
            }));
            Experiences.Add(new ExperienceItemViewModel(new ExperienceItem()
            {
                Company = "The Company / institute",
                JobDescription = "A job description summarizes the essential responsibilities, activities, qualifications and skills for a role. Also known as a JD, this document describes the type of work performed.",
                JobPosition = "GENERAL MANAGER",
                JobTimeSpan = "2017 - 2021",
                BulletJobTasks = bullets
            }));
            /*
            Experiences.Add(new ExperienceItemViewModel(new ExperienceItem()
            {
                Company = "The Company / institute",
                JobDescription = "A job description summarizes the essential responsibilities, activities, qualifications and skills for a role. Also known as a JD, this document describes the type of work performed.",
                JobPosition = "GENERAL MANAGER",
                JobTimeSpan = "2017 - 2021",
                BulletJobTasks = bullets
            }));
            /* Load default values */
            mJsonCV = new JsonCV();
            Candidate ct = new Candidate();
            ct.Name = Name;
            ct.SurName = SurName;
            ct.Title = Title;
            ct.Profile = Profile;
            var GetDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(GetDirectory, "default.jpg");
            ct.Picture = filePath;
            mJsonCV.Applicant = ct;
            mJsonCV.Contacts = ContactItems.ToList();
            mJsonCV.Educations = EducationItems.ToList();
            mJsonCV.Skills = Skills.ToList();
            mJsonCV.Experiences = new List<ExperienceItem>();
            foreach (ExperienceItemViewModel exp in Experiences)
            {
                mJsonCV.Experiences.Add(exp.Experience);
            }          
        }

        bool SaveResumeToFIle()
        {
            var GetDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(GetDirectory, "jsonCV.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, mJsonCV);             
            }
            return true;
        }

        bool LoadResumeFromFile()
        {
            try
            {
                var GetDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(GetDirectory, "jsonCV.json");
                if (!File.Exists(filePath))
                    return false;

                mJsonCV = JsonConvert.DeserializeObject<JsonCV>(File.ReadAllText(filePath));

                return true;
            }
            catch(Exception e)
            {

            }
            return false;
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
