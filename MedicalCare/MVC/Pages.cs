using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.MVC
{
    public class Pages
    {
        public static class HomeIndex
        {
            public const string Controller = "Home";
            public const string Action = "Index";
            public const string Role = "Home";
            public const string Url = "/Home/Index";
            public const string Name = "Home";
        }


        public static class SuperAdmin
        {
            public const string RoleName = "SuperAdmin";
        }

        public static class Admin
        {
            public const string RoleName = "Admin";
        }

        public static class Doctor
        {
            public const string RoleName = "Doctor";
        }

        public static class Nurse
        {
            public const string RoleName = "Nurse";
        }

        public static class Laboratorist
        {
            public const string RoleName = "Laboratorist";

        }

        public static class Pharmacist
        {
            public const string RoleName = "Pharmacist";

        }

        public static class Patient
        {
            public const string RoleName = "Patient";

        }

        public static class Receptionist
        {
            public const string RoleName = "Receptionist";
        }

        public static class Accountant
        {
            public const string RoleName = "Accountant";
        }

    }
}
