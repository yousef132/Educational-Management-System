using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Educational_management_system
{
    public class Course : Program
    {
        public string doctor { get; set; }
        public string name { get; set; }
        public List<string> students;
        public List<string> assignments;
        public Dictionary<string, List<Tuple<string, string>>> studentsDict; //student name , ass & sol
        public Course()
        {
            students = new List<string>();
            assignments = new List<string>();
            studentsDict = new Dictionary<string, List<Tuple<string, string>>>();
        }
        public void print()
        {
            Console.WriteLine($"course Name {name}");
            Console.WriteLine("doctor " + " " + doctor);
            Console.WriteLine("---------");

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();
            Console.WriteLine("---------");
            foreach (var assignment in assignments)
            {
                Console.WriteLine(assignment);
            }
            Console.WriteLine("---------");
            foreach (KeyValuePair<string, List<Tuple<string, string>>> student in studentsDict)
            {
                Console.WriteLine("Student: {0}", student.Key);
                Console.WriteLine();
                foreach (Tuple<string, string> assignment in student.Value)
                {
                    Console.WriteLine("Assignment: {0}", assignment.Item1);
                    Console.WriteLine("Solution: {0}", assignment.Item2);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("---------------------------------------");
        }
        public void print_in_view(string username)
        {
            Console.WriteLine($"Course {name} taught by Doctor {doctor} ");
            Console.WriteLine($"Course has {assignments.Count} assignment :");
            foreach (string ass in assignments)
            {
                bool solved = false;
                Console.WriteLine($"*Assignment : {ass} :");
                if (studentsDict.ContainsKey(username))
                {
                    foreach (Tuple<string, string> tuple in studentsDict[username])
                    {
                        if (tuple.Item1 == ass)
                        {
                            solved = true;
                            Console.WriteLine($"- solution : {tuple.Item2}");
                            break;

                        }
                    }
                }
                if (!solved)
                {
                    Console.WriteLine("- solution : Not submitted");
                }
            }
        }

        public void DoctorView()
        {
            Console.WriteLine("-----------------------------\n");
            Console.WriteLine($"Course Name : {name}");
            if (students.Count == 0 || students.Count == 1 && students[0].Length == 0)
            {
                Console.WriteLine("There are no students !");
            }
            else
            {
                Console.WriteLine($"* students   : \n");
                foreach (string st in students)
                {
                    if (st.Length > 0)
                    {
                        Console.WriteLine(st);
                    }
                }
            }           
            Console.WriteLine();
            Console.WriteLine("----------\n");
            if(assignments.Count == 0 || assignments.Count == 1 && assignments[0].Length == 0)
            {
                Console.WriteLine("There are No assignments !");
            }
            else
            {
                Console.WriteLine($"* assignments : \n");
                foreach (string ass in assignments)
                {
                    if (ass.Length > 0)
                    {
                        Console.WriteLine(ass);
                    }
                }
            }
          
            Console.WriteLine();
            Console.WriteLine("-----------------\n");
            if (studentsDict.Count > 0)
            {
                Console.WriteLine("Each student with his assignment and solution : \n");
                foreach (KeyValuePair<string, List<Tuple<string, string>>> dict in studentsDict)
                {
                    Console.WriteLine($"Student {dict.Key} : ");

                    //List<Tuple<string, string>> assignments = c.studentsDict[username];
                    for (int i = 0; i < dict.Value.Count; i++)
                    {
                        Console.WriteLine($"* Assignment : {dict.Value[i].Item1}\n");
                        Console.WriteLine($"- Solution : {dict.Value[i].Item2}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No one did any assignment");
            }
           
        }
    }
}
