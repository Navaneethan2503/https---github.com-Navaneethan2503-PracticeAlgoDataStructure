using System.IO;
using System;
using System.Text;
using School;

namespace Program{

    public partial class Staff : School.Staff{
        public Staff(string name , int amount){
            this.Name = name ;
            this.Amount = amount;
        }

        public void Print(){
            System.Console.WriteLine(this.Name + " is Staff and salary is " + this.Amount);
        }
    }
    class Program : School.IEducation {

        public static int NoOfInstances = 0 ;
        static Program(){
            System.Console.WriteLine("Static constuctor called .");
        }

        public Program(){
            NoOfInstances++;
        }
        public void TotalStudent(){
            System.Console.WriteLine("Total Strength is 200");
        }

        public void PrintDepartment(){
            System.Console.WriteLine("Computer Science and Engineering");
        }

        public void DisplaySchoolName(){
            System.Console.WriteLine("Panimalar");
        }
        static void Main1(){
            
            Staff s = new Staff("Kalpana", 2000);
            s.Print();
        }
    
    }
}