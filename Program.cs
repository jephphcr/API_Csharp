using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace api
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // new Student{Name = "Jefferson", Subscription = "002", Notes = new List<double>(){8, 9, 10}}.Save();

            var students = Student.All();
            foreach(var student in students)
            {
                student.Notes.Add(10);
                student.Save();
                Console.WriteLine(student.Name);
                Console.WriteLine(student.Subscription);
                foreach(var notes in student.Notes)
                {
                  var index = Convert.ToInt32(student.Notes.IndexOf(notes)) + 1;
                  Console.WriteLine("A sua " + index + "ª nota foi:");
                  Console.WriteLine(notes);  
                }
                // Console.WriteLine(student.Notes[0]);
                // Console.WriteLine(string.Join(",",student.Notes.ToArray()));
            }
            
            
            return;
            
        }
    } 
}
