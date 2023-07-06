using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_management_system
{
    public class Doctor : Student
    {
        public Dictionary<string, List<string>> course_assginment = new Dictionary<string,List<string>>();
        public static string NewCourseName;
        public static Course newcourse = new Course();
        public void MakeCourse()
        {
            Console.WriteLine("Choose name for the course.");
            NewCourseName = Console.ReadLine();
            if(searchforcourse(NewCourseName) != null) 
            {
                Console.WriteLine("This Name is used before ! \n  Choose another one ");
                return;
            }
            else
            {
                newcourse = new Course();
                newcourse.name = NewCourseName;
                newcourse.doctor = doctorObj.username;
                Courses.Add(newcourse);
            }
            AddForFile();
        }
        private void AddForFile()
        {
            lines = new List<string>();
            foreach (Course course in Courses)
            {
                line = $"{course.name},{tostring(course.students)},{tostring(course.assignments)},{course.doctor}";
                lines.Add(line);
            }
            File.WriteAllLines(coursespath, lines);
        }
        public override void ViewCourses()
        {
            ListCourses();
            Console.WriteLine("Enter Name of course to veiw : ");
            NewCourseName = Console.ReadLine();
            foreach(Course c in Courses)
            {
                if(c.name == NewCourseName && c.doctor == username)
                {
                    c.DoctorView();
                    return;
                }
            }
            Console.WriteLine("NO such couse !");
        }
        public override void menue()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("   1) Make a new course ");
            Console.WriteLine("   2) Add assignment ");
            Console.WriteLine("   3) View course ");
            Console.WriteLine("   4) Logout\n");
            Console.WriteLine("   Make a Choice :");
            choice=int.Parse(Console.ReadLine());
        }
        public void AddAssignment()
        {
            Console.WriteLine("Enter name of couse for this assignment");
            coursename =Console.ReadLine();
            if(searchforcourse(coursename) == null) 
            {
                Console.WriteLine("There is no such course ! Try again ");
                return;
            }
            Console.WriteLine("Enter assignment");
            assignmentname = Console.ReadLine();
            bool ok = false;
            foreach (Course course in Courses)
            {
                if (course.name == coursename && course.doctor == username)
                {
                    course.assignments.Add(assignmentname);
                    ok = true;
                    break;
                }
            }
            if (!ok)
            {
                Console.WriteLine("This course doesn't belong to you ! Try again .");
                return;               
            }
            AddForFile();
        }
        public override void ListCourses()
        {
            Console.WriteLine("My courses list : \n");

            foreach (Course course in Courses)
            {
                if(course.doctor == username)
                {
                    Console.WriteLine(course.name);
                }
            }
        }
    }
}
