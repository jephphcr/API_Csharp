using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace api
{
        partial class Student
        {
            #region "Properties"
            public int Id {get; set;}
            public string Name {get; set;}
            public string Subscription {get; set;}
            public List<double> notes;

            public List<double> Notes
            {
                get
                {
                    if(this.notes == null) this.notes = new List<double>();
                    return this.notes;
                }
                set
                {
                    this.notes = value;
                }
            }
            #endregion

            #region "Instance Methods"
            public double CalculateAVG()
            {
                var sumNotes = 0.0;
                foreach(var note in this.Notes)
                {
                    sumNotes += note;
                }
                return sumNotes/this.Notes.Count;
            }

            public string Situation()
            {
                return this.CalculateAVG() >= 7 ? "Approved" : "Disapproved";
            }

            public void Delete()
            {
                Student.DeleteById(this.Id);                
            }

            public void Save()
            {
                if(this.Id>0)
                {
                    Student.Update(this);
                }
                else
                {
                    Student.Insert(this);
  
                }                
            }
            #endregion

        }
        
}