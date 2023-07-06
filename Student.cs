using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_management_system
{
    public class Student : Program
    {
        public string username { get; set; }
        public string password { get; set; }
        public static string line;
        public static string assignmentname;
        public static List<string> lines = new List<string>();
        public void clear()
        {
            username = "";
        }
        public Student()
        {

        }
        public bool empty()
        {
            return (username!=null);
        }
        public void printing_login_and_signup()
        {
            Console.WriteLine("Enter a choice \n");
            Console.WriteLine();
            Console.WriteLine("1) Login ");
            Console.WriteLine("2) Sign up ");
            choice = int.Parse(Console.ReadLine());
            if (choice > 2 || choice < 1)
            {
                Console.WriteLine("Wrong choice ! \n Try again .");
                Console.WriteLine("------------------------------------");
                printing_login_and_signup();
            }
            if (choice == 1)
            {
                login();
            }
            else if (choice == 2)
            {
                signup();
            }

        }
        public void login()
        {
            if(status == 0)
            {
                Console.WriteLine();
                Console.WriteLine("1) Enter username .");
                studentObj.username = Console.ReadLine();
                Console.WriteLine("2) Enter password ");
                studentObj.password = Console.ReadLine();

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("1) Enter username .");
                doctorObj.username = Console.ReadLine();
                Console.WriteLine("2) Enter password ");
                doctorObj.password = Console.ReadLine();
            }
            if (searchforuser() == 2)
            {
                welcome();
               
            }
            else
            {
                Console.WriteLine("Wrong username or password ! \n Please try again .");
                Console.WriteLine("------------------------------------");
                login();
            }

        } 
        public void adduser()
        {
            if(status == 0)
            {
                line = $"{studentObj.username},{studentObj.password},{status},{Environment.NewLine}";

            }
            else
            {
                line = $"{doctorObj.username},{doctorObj.password},{status},{Environment.NewLine}";
            }
            File.AppendAllText(userpath, line);

        }
        public virtual void ListCourses()
        {
            foreach(Course c in Courses)
            {
                if (!c.students.Contains(username))
                {
                    Console.WriteLine(c.name);
                }
            }
        }
        public void signup()
        {
            if (status == 0)
            {
                Console.WriteLine();
                Console.WriteLine("1) Enter username .");
                studentObj.username = Console.ReadLine();
                Console.WriteLine("2) Enter password ");
                studentObj.password = Console.ReadLine();

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("1) Enter username .");
                doctorObj.username = Console.ReadLine();
                Console.WriteLine("2) Enter password ");
                doctorObj.password = Console.ReadLine();
            }
            if (searchforuser() > 0)
            {
                Console.WriteLine("You have entered used username or password !\n Please try again.");
                Console.WriteLine("------------------------------------");
                signup();
            }
            else
            {
                adduser();
                welcome();
             
            }
            
        }
        private void welcome()
        {
            Console.WriteLine($"Welcome {doctorObj.username}{studentObj.username} You are Logged in ");

        }
        public virtual void menue()
        {
            Console.WriteLine("------------------------\n");
            Console.WriteLine();
            Console.WriteLine( "1) Register in course\n");
            Console.WriteLine( "2) List Courses\n");
            Console.WriteLine( "3) View a course\n");
            Console.WriteLine( "4) Unregister from course \n");
            Console.WriteLine( "5) Submite solution \n");
            Console.WriteLine( "6) Log out\n");
            Console.WriteLine("Enter a choice \n");
            choice = int.Parse(Console.ReadLine());
        }
        private bool searchforassginment(Course c)
        {

            foreach (string ass in c.assignments)
            {
                if (ass == assignmentname)
                {
                    return true;
                }
            }
            return false;   
        }
        private void AddASolution(string solution)
        {
            lines=new List<string>();
            foreach (Course c in Courses)
            {
                if (c.name == coursename)
                {
                    //check for submition for an assignment
                    foreach (KeyValuePair<string, List<Tuple<string, string>>> dict in c.studentsDict)
                    {
                        if (dict.Key == username)
                        {
                            //List<Tuple<string, string>> assignments = c.studentsDict[username];
                            for (int i = 0; i < dict.Value.Count; i++)
                            {
                                //Tuple<string, string> t = dict.Value[i];
                                if (dict.Value[i].Item1 == assignmentname)
                                {
                                    if (!string.IsNullOrEmpty(dict.Value[i].Item2)) 
                                    {
                                        Console.WriteLine("You already answered this assignment");
                                        return;
                                    }                          
                                }
                            }
                        }   
                    }
                    if (!c.studentsDict.ContainsKey(username))
                    {
                        c.studentsDict.Add(username, new List<Tuple<string, string>>());
                    }
                    c.studentsDict[username].Add(new Tuple<string, string>(assignmentname,solution));                   
                }
                foreach (KeyValuePair<string, List<Tuple<string, string>>> kvp in c.studentsDict)
                {
                    foreach(Tuple<string ,string > t in kvp.Value)
                    {
                        if(!string.IsNullOrEmpty(t.Item2) && !string.IsNullOrEmpty(t.Item1))
                        {
                            line = $"{c.name},{t.Item1},{t.Item2},{kvp.Key},{c.doctor}";
                            lines.Add(line);
                        }
                    }
                }
            }
            File.WriteAllLines(ass_sol_path, lines);
        }
        public void SubmitSolution()
        {
            Console.WriteLine("Enter Name of course : ");
            coursename = Console.ReadLine();
            Course course = searchforcourse(coursename);
            if (course!=null)
            {
                if (!course.students.Contains(username))
                {
                    Console.WriteLine("You didn't register in this course ! ");
                    return;
                }
                Console.WriteLine("Enter assignment name : ");
                assignmentname = Console.ReadLine();
                if (searchforassginment(course))
                {
                    Console.WriteLine("Enter solution For This assignment.");
                    string solution = Console.ReadLine();
                    AddASolution(solution);
                }
                else
                {
                    Console.WriteLine("NO such assignment in this course ! Try again .");
                    return;
                }

            }
            else
            {
                Console.WriteLine("There is no course like this ! Try again .");
                return;
            }

        }       
        public  int  searchforuser()
        {
            if(status == 0)
            {
                foreach (Student st in students)
                {
                    if (st.username == studentObj.username && st.password == studentObj.password)// for login
                    {
                        return 2;
                    }
                    else if (st.username == studentObj.username || st.password == studentObj.password)// for signup
                    {
                        return 1;
                    }
                }
                return 0;
            }
            else
            {
                foreach (Doctor dr in doctors)
                {
                    if (dr.username == doctorObj.username && dr.password == doctorObj.password)// for login
                    {
                        return 2;
                    }
                    else if (dr.username == doctorObj.username || dr.password == doctorObj.password)// for signup
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }
        public Course searchforcourse(string name)
        {
            foreach (Course c in Courses)
            {
                if (c.name == name)
                {
                    return c;
                }
            }
            return null;
        }
        public string tostring(List<string> l)
        {
            string ans = "";
            for(int i = 0; i < l.Count-1; i++)
            {
                ans += l[i] + '|';
            }
            if(l.Count !=0)
            {
                ans += l.Last();
            }
            return ans;
        }
        private void addcourse()
        {
            lines = new List<string>();
            foreach (Course c in Courses)
            {
                if (c.name == coursename)
                {
                    if(c.students.Contains(username))
                    {
                        Console.WriteLine("You have already registered in this course !");
                        return;
                    }
                    c.students.Add(username);
                }
                line = $"{c.name},{tostring(c.students)},{tostring(c.assignments)},{c.doctor}";
                lines.Add(line);
            }
            File.WriteAllLines(coursespath, lines);
        }
        private void DeletUserFromCourse(Course c)
        {
            lines = new List<string>();
            line = "";
            if (!c.students.Contains(username))
            {
                Console.WriteLine("You didn't Register in this course ! Try another one.");
                return;
            }
            else
            {
                foreach (Course course in Courses)
                {
                    if (course.name == coursename && course.students.Contains(username))
                    {
                        course.students.Remove(username);                    
                    }
                    line = $"{course.name},{tostring(course.students)},{tostring(course.assignments)},{course.doctor}";

                    lines.Add(line);
                }
            }
            File.WriteAllLines(coursespath, lines);
        }
        private void DeleteAssignments()
        {
            lines = new List<string>();
            foreach (string line in File.ReadLines(ass_sol_path))
            {
                string [] segments = line.Split(',');
                if (!(segments[0]==coursename && segments[segments.Count()-2]==username))
                {
                    lines.Add(line);
                }
            }
            foreach(Course course in Courses)
            {
                if(course.name == coursename)
                {
                    foreach(KeyValuePair<string ,List<Tuple<string,string>>> dict in course.studentsDict)
                    {
                        if(dict.Key == username)
                        {
                            course.studentsDict.Remove(dict.Key);
                            break;
                        }
                    }
                }
            }
            File.WriteAllLines(ass_sol_path, lines);
        }
        public void UnregisterFromCourse()
        {
            Console.WriteLine("Enter course name ");
            coursename = Console.ReadLine();
            Course c = searchforcourse(coursename);
            if (c!=null)
            {
                DeletUserFromCourse(c);
                DeleteAssignments();
            }
            else
            {
                Console.WriteLine("NO such course ! Try again ");
                return;
            }
        }
        public void Register_in_course()
        {
            Console.WriteLine("Enter course name ");
            coursename = Console.ReadLine();
            Course c = searchforcourse(coursename);
            if (c != null)
            {
                addcourse();
            }
            else
            {
                Console.WriteLine("There is no couser like this !  try again .");
                return;
            }
        }
        private void ListMyCourses()
        {
            Console.WriteLine(" Course : ");
            Console.WriteLine();
            foreach (Course c in Courses)
            {
                if (c.students.Contains(username))
                {
                    Console.WriteLine(c.name);
                }
            }
        }
        public virtual void ViewCourses()
        {
            Console.WriteLine("My courses list :");
            Console.WriteLine();
            ListMyCourses();
            Console.WriteLine("Enter name of Course To view :");
            string s = Console.ReadLine();
            bool found = false;
            foreach (Course C in Courses)
            {
                if (s == C.name)
                {
                    if (C.students.Contains(username))
                    {
                        C.print_in_view(username);
                        found = true;

                    }
                }
            }
            if (!found)
            {
                Console.WriteLine("There is no such course in you list ! Try again ");
                return;
            }
        }
    }
}