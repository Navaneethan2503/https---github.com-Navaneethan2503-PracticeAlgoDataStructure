using System;

namespace School{

    public partial class Staff{
        public string Name { get; set; }
        public int Amount { get; set; }
    }


    interface IEducation{
        void TotalStudent();

        void PrintDepartment();

        void DisplaySchoolName(){
            System.Console.WriteLine("VMHSS");
        }

    
    }
}