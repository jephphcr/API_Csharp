using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace api
{
        partial class Student
        {
            #region "Class/Static Methods"
            private static string connectionString()
            {
                return @"Password=sequor;Persist Security Info=True;User ID=sa;Initial Catalog=API;Data Source=SQO-185\SQLJEFFERSON";
            }

            public static void Insert(Student student)
            {
                SqlConnection sqlConn = new SqlConnection(connectionString());
                sqlConn.Open();
                
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Students (name, subscription, notes) VALUES (@name, @subscription, @notes)",sqlConn);
                sqlCommand.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCommand.Parameters["@name"].Value = student.Name;

                sqlCommand.Parameters.Add("@subscription", SqlDbType.VarChar);
                sqlCommand.Parameters["@subscription"].Value = student.Subscription;

                sqlCommand.Parameters.Add("@notes", SqlDbType.VarChar);
                sqlCommand.Parameters["@notes"].Value = string.Join(",", student.Notes.ToArray());
                sqlCommand.ExecuteNonQuery();
                
                // sqlCommand.Parameters.AddWithValue("@name", student.Name);
                // sqlCommand.Parameters.AddWithValue("@subscription", student.Subscription);
                // sqlCommand.Parameters.AddWithValue("@notes", string.Join(",", student.Notes.ToArray()));

                sqlConn.Close();
                sqlConn.Dispose();
                
            }

             public static void Update(Student student)
            {
                SqlConnection sqlConn = new SqlConnection(connectionString());
                sqlConn.Open();
                
                SqlCommand sqlCommand = new SqlCommand($"UPDATE Students SET name=@name, subscription=@subscription, notes=@notes WHERE id = @id",sqlConn);
                
                sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                sqlCommand.Parameters["@id"].Value = student.Id;
                
                sqlCommand.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCommand.Parameters["@name"].Value = student.Name;

                sqlCommand.Parameters.Add("@subscription", SqlDbType.VarChar);
                sqlCommand.Parameters["@subscription"].Value = student.Subscription;

                sqlCommand.Parameters.Add("@notes", SqlDbType.VarChar);
                sqlCommand.Parameters["@notes"].Value = string.Join(",", student.Notes.ToArray());
                sqlCommand.ExecuteNonQuery();
                
                // sqlCommand.Parameters.AddWithValue("@name", student.Name);
                // sqlCommand.Parameters.AddWithValue("@subscription", student.Subscription);
                // sqlCommand.Parameters.AddWithValue("@notes", string.Join(",", student.Notes.ToArray()));

                sqlConn.Close();
                sqlConn.Dispose();
                
            }
            public static void DeleteById(int id)
            {
                SqlConnection sqlConn = new SqlConnection(connectionString());
                sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand($"DELETE FROM Students WHERE id = {id}",sqlConn);
                sqlCommand.ExecuteNonQuery();
                
                sqlConn.Close();
                sqlConn.Dispose();
                
            }

            public static List<Student> All()
            {
                var students = new List<Student>();
                SqlConnection sqlConn = new SqlConnection(connectionString());
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Students",sqlConn);
                var reader = sqlCommand.ExecuteReader();
                while(reader.Read())
                {

                    var notes = new List<double>();
                    string strNotes = Convert.ToString(reader["notes"]);
                    foreach(var note in strNotes.Split(','))
                    {
                        notes.Add(Convert.ToDouble(note));
                    }
                    var student = new Student
                    {                    
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]),
                        Subscription = Convert.ToString(reader["subscription"]),
                        Notes = notes,
                    };
                    students.Add(student);

                }
                sqlConn.Close();
                sqlConn.Dispose();
                
                return students;
            }

            #endregion


        }
        
}