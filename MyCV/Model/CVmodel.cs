using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCV.Model
{
    public struct Candidate
    {
        public string Picture { get; set; }
       
        public string Name { get; set; }

        public string SurName { get; set; }

        public string Title { get; set; }

        public string Profile { get; set; }
    }

    public struct ExperienceItem
    {
        public string JobPosition { get; set; }

        public string Company { get; set; }

        public string JobTimeSpan { get; set; }

        public string JobDescription { get; set; }

        public List<string> BulletJobTasks { get; set; }
    }

    public struct SkillItem
    {
        public string SkillName { get; set; }

        int mSkillValue;
        public int SkillValue
        {
            get
            {
                return mSkillValue;
            }

            set
            {
                value = value * 220 / 100;
                mSkillValue = value;                
            }
        }
    }

    public struct EducationItem
    {
        public string Year { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }

    }

    public struct ContactItem
    {
        public string ContactKey { get; set; }
        public string ContactValue { get; set; }
    }

    public struct JsonCV
    {
        public Candidate Applicant { get; set; }
        public List<ExperienceItem> Experiences { get; set; }
        public List<SkillItem> Skills { get; set; }
        public List<EducationItem> Educations { get; set; }
        public List<ContactItem> Contacts { get; set; }
    }
}
