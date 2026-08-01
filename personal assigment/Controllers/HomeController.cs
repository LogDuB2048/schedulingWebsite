using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using personal_assigment.Models;
using System.Diagnostics;
using System;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;
namespace personal_assigment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Multmodel mainModel;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            //this are varaibles needed to connect to mysql
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataReader dr;
            //arrays that will store the data from mysql, 50 is a arbitrary large number
            Student[] testing = new Student[50];
            Course[] courses = new Course[50];
            Instrucotors[] instrctors = new Instrucotors[50];
            //path and command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "select * from personal_assigment.students";
            int counter = 0;
            //connecting to mysl
            con = new MySqlConnection(path);
            //if connecting works
            try
            {
                //performs the command
                con.Open();
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                //reading results
                while (dr.Read())
                {
                    //adding reults to array
                    testing[counter] = new Student() { Id = int.Parse(dr.GetValue(0) + ""), Name=dr.GetValue(1)+"",credits = int.Parse(dr.GetValue(2)+"") };
                    counter++;
                }
                //done with command
                dr.Close();
                cmd.Dispose();
                //resize array to have no empty elements
                Array.Resize(ref testing, counter);
                counter = 0;
                //next command, does things the same way as last command 
                sql = "select * from personal_assigment.course";
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    courses[counter] = new Course() { Id = int.Parse(dr.GetValue(0) + ""), Course_Title=dr.GetValue(1)+"",Instructor_Id=int.Parse(dr.GetValue(2)+"")};
                    counter++;
                }
                dr.Close();
                cmd.Dispose();
                Array.Resize(ref courses, counter);
                
                counter = 0;
                //next command, does things the same way as last command 
                sql = "select * from personal_assigment.instructors";
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    instrctors[counter] = new Instrucotors() { Id = int.Parse(dr.GetValue(0) + ""), Name = dr.GetValue(1) + "", courseDepartment = dr.GetValue(2) + "" };
                    counter++;
                }
                dr.Close();
                cmd.Dispose();
                Array.Resize(ref instrctors, counter);
                //close connection to mysql
                con.Close();
                //creates multimodel combing the three main models
                mainModel = new() { m_students = testing, m_courses = courses, m_instrucoors=instrctors};

                return View(mainModel);
            }
            //if failed to connect to mysql
            catch (Exception ex)
            {
                return View("Privacy");
            }

        }
        //loads privacy page
        public IActionResult Privacy()
        {
            return View();
        }
        //loads error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //this is the delete function
        public IActionResult Delete(int id, string table,string IdName)
        {
            
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            //path and command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "delete from personal_assigment."+ table+" where "+IdName+"="+id;
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                //performs the command
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            //if failed
            catch
            {
                return View("Privacy");
            }
            Index();
            return View("Index",mainModel);
        }
        //loads page that create students
        public IActionResult CreateStudent()
        {
            return View();
        }
        //loads page that creates instructors
        public IActionResult CreateInstructor()
        {
            return View();
        }
        //loads page that creates courses
        public IActionResult CreateCourse()
        {
            return View();
        }
        //functon that creates a student, using data from createStudent page
        public IActionResult createStudentData(int id, string name,int credits)
        {
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            //path and command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "insert into personal_assigment.students(idstudents,students_name,credits) values(@id, @name, @credits)";
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                //add the values to command
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@credits", credits);
                //performing the command
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            //if failed
            catch
            {
                return View("Privacy");
            }
            Index();
            //return to main page
            return View("Index",mainModel);
        }
        //this is function that creates instrucors, using data from createInstructor page
        public IActionResult CreateInstructorData(int id, string name,string courseDepartment)
        {
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            //path and command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "insert into personal_assigment.instructors(idinstructors, name, course_department) values(@id, @name, @department)";
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                //adding variables to command
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@department", courseDepartment);
                //perform command
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            //if failed
            catch
            {
                return View("Privacy");
            }
            Index();
            return View("Index", mainModel);
        }
        //function that creates course, using data from createCourse page
        public IActionResult CreateCourseData(int id, int Instructor_Id, string Course_Title)
        {
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            //path and command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "insert into personal_assigment.course(idcourse, course_title, instructor) values(@id, @Course_Title, @Instructor_Id)";
            //connect to mysql
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                //adding data to command
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Instructor_Id", Instructor_Id);
                cmd.Parameters.AddWithValue("@Course_Title", Course_Title);
                //perform command
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            //if failed
            catch
            {
                return View("Privacy");
            }
            Index();
            return View("Index", mainModel);
        }
        //displaying students
        public IActionResult displayStudents(int id)
        {
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataReader dr;
            StudentCourses[] studentCourses = new StudentCourses[50];
            //path and command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "select * from personal_assigment.student_grades where idcourse=" + id;
            int counter = 0;
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                //peforming command
                dr = cmd.ExecuteReader();
                //reading results
                while (dr.Read())
                {
                    studentCourses[counter] = new StudentCourses() { StudentId = int.Parse(dr.GetValue(1) + ""), CourseId = int.Parse(dr.GetValue(2) + ""), Grade = float.Parse(dr.GetValue(3) + "") };
                    counter++;
                }
                dr.Close();
                cmd.Dispose();
                con.Close();
                //resize array to remove empty elements
                Array.Resize(ref studentCourses, counter);
            }
            //if failed
            catch (Exception ex)
            { 
                return View("Privacy"); 
            }
            Index();
            return View(studentCourses);
        }
        //loads page to add students to a course
        public IActionResult addStudentToCourse()
        {
            return View();
        }
        //fucntion that adds student to course using data from addStudentToCourse page
        public IActionResult addStudentToCourseData(int StudentId, int CourseId, float grade)
        {
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataReader dr;
            Student[] temp = new Student[50];
            Course[] temp2 = new Course[50];
            int counter = 0;
            //path and first command for mysql
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "select * from personal_assigment.students where idstudents=" + StudentId;
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                //perform first command
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //gets student to se if student is real, should only ever have one or zero element
                    temp[counter]= new Student() { Id = int.Parse(dr.GetValue(0) + ""), Name = dr.GetValue(1) + "", credits = int.Parse(dr.GetValue(2) + "") };
                    counter++;
                }
                dr.Close();
                cmd.Dispose();
                Array.Resize(ref temp, counter);
                counter = 0;
                //performs second commnand
                sql = "select * from personal_assigment.course where idcourse=" + CourseId;
                cmd = new MySqlCommand(sql, con);
                dr= cmd.ExecuteReader();
                while (dr.Read())
                {
  
                    //get course to see if course is real, should only ever have one or zero elements
                    temp2[counter] = new Course { Id = int.Parse(dr.GetValue(0) + ""), Course_Title = dr.GetValue(1) + "", Instructor_Id = int.Parse(dr.GetValue(2) + "") };
                    counter++;
                }
                dr.Close();
                cmd.Dispose();

                Array.Resize(ref temp2, counter);
                //checking if student and course exist
                if (temp.Length > 0 && temp2.Length>0)
                {
                    //command to add row into table
                    sql = "insert into personal_assigment.student_grades(idstudent, idcourse, grade) values(@StudentId, @CourseId, @grade)";
                    cmd = new MySqlCommand(sql, con);
                    //adding data to command
                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@CourseId", CourseId);
                    cmd.Parameters.AddWithValue("@grade", grade);
                    //performs command
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                con.Close();
            }
            catch (Exception ex) { return View("Privacy"); }
            Index();
            return View("Index",mainModel);
        }
        //displaying courses
        public IActionResult displayCourses(int id)
        {
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataReader dr;
            StudentCourses[] studentCourses = new StudentCourses[50];
            //path and command
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "select * from personal_assigment.student_grades where idstudent=" + id;
            int counter = 0;
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
               
                while (dr.Read())
                {
                    //adding Studentcourse to array
                    studentCourses[counter] = new StudentCourses() { StudentId = int.Parse(dr.GetValue(1) + ""), CourseId = int.Parse(dr.GetValue(2) + ""), Grade = float.Parse(dr.GetValue(3) + "") };
                    counter++;
                }
                dr.Close();
                cmd.Dispose();
                con.Close();
                Array.Resize(ref studentCourses, counter);
            }
            catch (Exception ex) { return View("Privacy"); }
            Index();
            return View(studentCourses);
        }

        public IActionResult dropStudent(int courseid, int studentId)
        {
            Index();
            MySqlConnection con;
            MySqlCommand cmd;
            //path and command
            string path = "server=127.0.0.1;port=3306;user=root;password=Iu7#01kp;database=personal_assigment";
            string sql = "delete from personal_assigment.student_grades where idstudent=" + studentId +" and idcourse="+courseid;
            //connect to server
            con = new MySqlConnection(path);
            //if success
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch
            {
                return View("Privacy");
            }
            return View("Index", mainModel);
        }
    }
}
