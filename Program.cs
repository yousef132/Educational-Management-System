using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Educational_management_system
{
    public class Program
    {
        public const string userpath = "D:\\user.txt", coursespath = "D:\\courses.txt", ass_sol_path = "D:\\ass and sol.txt";

        public static int choice;
        public static int status;
        public static string coursename;
        public static Course courseObj = new Course();
        public static Student studentObj = new Student();
        public static Doctor doctorObj = new Doctor();
        public static List<Student> students = new List<Student>();
        public static List<Course> Courses = new List<Course>();
        public static List<Doctor> doctors = new List<Doctor>();
        public static void load_users()
        {
            foreach (string line in File.ReadLines(userpath))
            {
                string[] arr = line.Split(',');
                if (arr[2] == "0")
                {
                    studentObj.username = arr[0];
                    studentObj.password = arr[1];
                }
                else
                {
                    doctorObj.username = arr[0];
                    doctorObj.password = arr[1];
                }
                if (doctorObj.empty())
                    doctors.Add(doctorObj);
                if (studentObj.empty())
                    students.Add(studentObj);
                doctorObj = new Doctor();
                studentObj = new Student();
            }
        }       

        public static void load_courses()
        {
            //prog1,ali|khalid|omar,what are the data types for numbers|write a program for hello world | what are primes,mohamed
            foreach (string line in File.ReadLines(coursespath))
            {
                string[] seg1 = line.Split(',');
                courseObj.name = seg1[0];
                string[] seg2 = seg1[1].Split('|');
                courseObj.students.AddRange(seg2);
                string[] seg3 = seg1[2].Split('|');
                courseObj.assignments.AddRange(seg3);
                courseObj.doctor = seg1[3];
                //prog1,what are the data types for numbers,int and float ,omar,mohamed->doctor
                foreach (string line2 in File.ReadLines(ass_sol_path))
                {
                    string[] seg = line2.Split(',');
                    if (seg[0] == courseObj.name)
                    {
                        if (!courseObj.studentsDict.ContainsKey(seg[3]))
                        {
                            courseObj.studentsDict.Add(seg[3], new List<Tuple<string, string>>());
                        }
                        courseObj.studentsDict[seg[3]].Add(new Tuple<string, string>(seg[1], seg[2]));
                    }
                }
                Courses.Add(courseObj);
                courseObj = new Course();
            }
        }

        static void Main(string[] args)
        {
            load_users();
            load_courses();
            Console.WriteLine("Are you a doctor (1) or a student (0)\n");
            status = int.Parse(Console.ReadLine());
            studentObj.printing_login_and_signup();
            if (status == 0)//student
            {
                while (true)
                {
                    studentObj.menue();
                    if (choice == 1)
                    {
                        studentObj.Register_in_course();
                    }
                    else if (choice == 2)
                    {
                        studentObj.ListCourses();
                    }
                    else if (choice == 3)
                    {
                        studentObj.ViewCourses();
                    }
                    else if (choice == 4)
                    {
                        studentObj.UnregisterFromCourse();
                    }
                    else if (choice == 5)
                    {
                        studentObj.SubmitSolution();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else//doctor
            {
                while (true)
                {
                    doctorObj.menue();                   
                    if (choice == 1)
                    {
                        doctorObj.MakeCourse();
                    }
                    else if (choice == 2)
                    {
                        doctorObj.AddAssignment();
                    }
                    else if (choice == 3)
                    {
                        doctorObj.ViewCourses();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
