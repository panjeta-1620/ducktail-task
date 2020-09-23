using System;
using System.Collections.Generic;
using System.Text;
using StudentApplicationEntities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace StudentApplicationDAL
{
    public class StudentDAL
    {
        SqlConnection con = null;
        string ConnectionString = ConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ConnectionString;

        public List<ClassEntity> GetClasses()
        {
            List<ClassEntity> classes = new List<ClassEntity>();
            using (con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ClassID, ClassName FROM Class", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClassEntity cls = new ClassEntity();
                    cls.ClassID = reader.GetInt32(0);
                    cls.ClassName = reader.GetString(1);
                    classes.Add(cls);
                }

                return classes;
            }
        }
        public List<SubjectEntity> GetSubjects(int ClassID)
        {
            List<SubjectEntity> subjects = new List<SubjectEntity>();
            using (con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SubjectID, SubjectName FROM Subject  where ClassID=@ClassID ", con);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SubjectEntity sub = new SubjectEntity();
                    sub.SubjectID = reader.GetInt32(0);
                    sub.SubjectName = reader.GetString(1);
                    subjects.Add(sub);
                }

                return subjects;
            }
        }
        public int InsertStudentData(StudentEntity stu)
        {

            using (con = new SqlConnection(ConnectionString))
            {
                // SqlTransaction objTrans = null;
                // objTrans = con.BeginTransaction();
                StudentEntity showStu = new StudentEntity();
                int studentID = 0;
                try
                {
                    if (stu.StudentID == 0)
                    {
                        List<int> subjects = stu.SubjectsIDs;
                        // SqlCommand cmd = new SqlCommand("dbo.spInsertStudentData", con, objTrans);
                        SqlCommand cmd = new SqlCommand("dbo.spInsertStudentData", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("FirstName", stu.FirstName);
                        cmd.Parameters.AddWithValue("LastName", stu.LastName);
                        cmd.Parameters.AddWithValue("ClassID", stu.ClassID);
                        SqlParameter StudentID = new SqlParameter("@StudentID", SqlDbType.Int);
                        StudentID.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(StudentID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        studentID = Convert.ToInt32(StudentID.Value);
                        foreach (var subject in subjects)
                        {
                            SqlCommand cmd3 = new SqlCommand("INSERT INTO StudentSubject(StudentID,SubjectID) VALUES (@StudentID,@SubjectID)", con);
                            // SqlCommand cmd3 = new SqlCommand("INSERT INTO StudentSubject(StudentID,SubjectID) VALUES (@StudentID,@SubjectID)", con, objTrans);
                            cmd3.Parameters.AddWithValue("StudentID", studentID);
                            cmd3.Parameters.AddWithValue("SubjectID", subject);
                            cmd3.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        SqlCommand cmdd = new SqlCommand("dbo.spUpdateStudent", con);
                        cmdd.CommandType = CommandType.StoredProcedure;
                        List<int> subjects = stu.SubjectsIDs;
                        cmdd.Parameters.AddWithValue("StudentID", stu.StudentID);
                        cmdd.Parameters.AddWithValue("FirstName", stu.FirstName);
                        cmdd.Parameters.AddWithValue("LastName", stu.LastName);
                        cmdd.Parameters.AddWithValue("ClassID", stu.ClassID);
                        con.Open();
                        cmdd.ExecuteNonQuery();   
                        studentID = Convert.ToInt32(stu.StudentID);
                        foreach (var sub in subjects)
                        {
                            SqlCommand cmd3 = new SqlCommand("Delete from SubjectStudent Where StudentID=@StudentID", con);
                            cmd3.Parameters.AddWithValue("StudentID", studentID); 
                            cmd3.ExecuteNonQuery();
                        }
                        foreach (var subject in subjects)
                        {
                            SqlCommand cmd3 = new SqlCommand("INSERT INTO SubjectStudent(StudentID,SubjectID) VALUES (@StudentID,@SubjectID)", con);
                            cmd3.Parameters.AddWithValue("StudentID", studentID);
                            cmd3.Parameters.AddWithValue("SubjectID", subject);                        
                            cmd3.ExecuteNonQuery();  
                        }
                    }
                    // objTrans.Commit();
                    con.Close();
                }
                catch (Exception ex)
                {
                    // objTrans.Rollback();
                    throw ex;
                }
                return studentID;

            }
        }
        public List<ShowDatainGrid> GetStudentData()
        {
            var result = new List<ShowDatainGrid>();
            try
            {
                using (con = new SqlConnection(ConnectionString))
                {


                    SqlCommand cmd = new SqlCommand("dbo.spShowStudentData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    List<int> student_id = new List<int>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ShowDatainGrid showData = new ShowDatainGrid();
                        showData.StudentID = int.Parse(row["StudentID"].ToString());
                        showData.FirstName = row["FirstName"].ToString();
                        showData.LastName = row["LastName"].ToString();
                        showData.Class = row["ClassName"].ToString();
                        showData.Subject = row["SubjectName"].ToString();

                        if (!student_id.Contains(showData.StudentID))
                        {
                            result.Add(showData);
                            student_id.Add(showData.StudentID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public List<ShowDatainGrid> SearchData(ShowDatainGrid searchData)
        {
            var result = new List<ShowDatainGrid>();
            using (con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.spSearchStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(searchData.Keyword))
                {
                    cmd.Parameters.AddWithValue("@Keyword", searchData.Keyword);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Keyword", DBNull.Value);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                List<int> student_id = new List<int>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ShowDatainGrid showData = new ShowDatainGrid();
                    showData.StudentID = int.Parse(row["StudentID"].ToString());
                    showData.FirstName = row["FirstName"].ToString();
                    showData.LastName = row["LastName"].ToString();
                    showData.Class = row["ClassName"].ToString();
                    showData.Subject = row["SubjectName"].ToString();

                    if (!student_id.Contains(showData.StudentID))
                    {
                        result.Add(showData);
                        student_id.Add(showData.StudentID);
                    }
                }
            }
        
        
          
            return result;
            }

        public void GetStudentById(int StudentID, out StudentEntity stuField)
        {
            List<SubjectEntity> subFields = new List<SubjectEntity>();
            using (con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmdStudentDetails = new SqlCommand(@"Select s.FirstName,s.LastName,s.ClassID
                                                                 From Student s                                                               
                                                                where s.StudentID=@StudentID", con);
                cmdStudentDetails.Parameters.AddWithValue("StudentID", StudentID);
                con.Open();
                SqlDataReader rdrStudent = null;
                
                rdrStudent = cmdStudentDetails.ExecuteReader();
                StudentEntity stu = new StudentEntity();
                while(rdrStudent.Read())
                {
                    stu.StudentID =rdrStudent.GetInt32(0);
                    stu.FirstName = rdrStudent.GetString(1);
                    stu.LastName = rdrStudent.GetString(2);
                    stu.ClassID = rdrStudent.GetInt32(3); ;

                }
                stuField = stu;

            }
        }
        public void DeleteStudentData(StudentEntity stuId)
        {
            using (con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd3 = new SqlCommand(@"Delete From StudentSubject where StudentID=@StudentID;
                                                   Delete From Student where StudentID=@StudentID
                                                   ", con);
                cmd3.Parameters.AddWithValue("StudentID", stuId.StudentID);
                con.Open();
                cmd3.ExecuteNonQuery();
                
            }
        }
    }
}

