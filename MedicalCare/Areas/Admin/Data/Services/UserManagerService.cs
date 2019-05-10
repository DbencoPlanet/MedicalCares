using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Data;
using MedicalCare.Models;
using MedicalCare.Models.AccountViewModels;
using MedicalCare.Models.Entities;
using MedicalCare.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
       

        public UserManagerService (UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;

        }


        public async Task AddUserToRoleAsync(string userId, string rolename)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, rolename);
            }
        }

        public async Task<List<ApplicationUser>> AllUsersAsync(string searchString, string currentFilter, int? page)
        {
            var users = _userManager.Users.Where(x => x.UserName != "SuperAdmin" );
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.SurName.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.SurName.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.SurName).ToListAsync();
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> GetDoctorAsync(int? id)
        {
            var doctor = await _context.Doctor.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }

        public async Task<Accountants> GetAccountantAsync(int? id)
        {
            var accountant = await _context.Accountants.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if(accountant != null)
                {
                return accountant;
               }
            return null;
        }

        public async Task<Patient> GetPatientAsync(int? id)
        {
            var patient = await _context.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if(patient != null)
            {
                return patient;
            }
            return null;
        }

        public async Task<ApplicationUser> GetUserByUserEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<ApplicationUser> GetUserByUserIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> IsUsersinRoleAsync(string userid, string role)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
              
                await _userManager.IsInRoleAsync(user, role);
              
            }
            return true;

        }

        //public async Task<List<Doctor>> ListDoctorAsync(string searchString, string currentFilter, int? page)
        //{
        //    var list = _context.Doctor.Include(x => x.User);
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        if (CountString(searchString) > 1)
        //        {
        //            string[] searchStringCollection = searchString.Split(' ');

        //            foreach (var item in searchStringCollection)
        //            {
        //                list.Where(s => s.User.SurName.ToUpper().Contains(item.ToUpper()) || s.User.FirstName.ToUpper().Contains(item.ToUpper()) || s.User.OtherName.ToUpper().Contains(item.ToUpper()) || s.User.DoctorReg.ToUpper().Contains(item.ToUpper()) || s.User.UserName.ToUpper().Contains(item.ToUpper()));
        //            }
        //        }
        //        else
        //        {
        //            list.Where(s => s.User.SurName.ToUpper().Contains(searchString.ToUpper()) || s.User.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.User.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.User.DoctorReg.ToUpper().Contains(searchString.ToUpper())|| s.User.UserName.ToUpper().Contains(searchString.ToUpper()));
                    
        //        }

        //    }
        //    return await list.ToListAsync();
        //}

        //public async Task<List<Employee>> ListEmployeeAsync(string searchString, string currentFilter, int? page)
        //{
        //    var list = _context.Employees.Include(x => x.User);
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        if (CountString(searchString) > 1)
        //        {
        //            string[] searchStringCollection = searchString.Split(' ');

        //            foreach (var item in searchStringCollection)
        //            {
        //                list.Where(s => s.User.SurName.ToUpper().Contains(item.ToUpper()) || s.User.FirstName.ToUpper().Contains(item.ToUpper()) || s.User.OtherName.ToUpper().Contains(item.ToUpper()) || s.User.EmployeeReg.ToUpper().Contains(item.ToUpper()) || s.User.UserName.ToUpper().Contains(item.ToUpper()));
        //            }
        //        }
        //        else
        //        {
        //            list.Where(s => s.User.SurName.ToUpper().Contains(searchString.ToUpper()) || s.User.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.User.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.User.EmployeeReg.ToUpper().Contains(searchString.ToUpper()) || s.User.UserName.ToUpper().Contains(searchString.ToUpper()));

        //        }

        //    }
        //    return await list.ToListAsync();
        //}

        //public async Task<List<Patient>> ListPatientAsync(string searchString, string currentFilter, int? page)
        //{
        //    var list = _context.Patients.Include(x => x.User);
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        if (CountString(searchString) > 1)
        //        {
        //            string[] searchStringCollection = searchString.Split(' ');

        //            foreach (var item in searchStringCollection)
        //            {
        //                list.Where(s => s.User.SurName.ToUpper().Contains(item.ToUpper()) || s.User.FirstName.ToUpper().Contains(item.ToUpper()) || s.User.OtherName.ToUpper().Contains(item.ToUpper()) || s.User.PatientReg.ToUpper().Contains(item.ToUpper()) || s.User.UserName.ToUpper().Contains(item.ToUpper()));
        //            }
        //        }
        //        else
        //        {
        //            list.Where(s => s.User.SurName.ToUpper().Contains(searchString.ToUpper()) || s.User.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.User.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.User.PatientReg.ToUpper().Contains(searchString.ToUpper()) || s.User.UserName.ToUpper().Contains(searchString.ToUpper()));

        //        }

        //    }
        //    return await list.ToListAsync();
        //}

       
        public async Task<string> NewDoctorAsync(RegisterViewModel model, List<IFormFile> files, int BloodGpId, string designation, int deptId, string biography, string specialist, string education)
        {
            //var officer = await HttpContext.IdentityUser.GetUserName();

            //var officer = HttpContext.Current.User.Identity.GetUserName();
            //if (officer == "SuperAdmin")
            //{
            //    officer = "Admin";
            //}

            var fileName = await _dotnetdesk.UploadFile(files, _env);


            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                EntityStatus = EntityStatus.Active,
                ProfilePicture = "/uploads/" + fileName
                //RegisteredBy = officer
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Doctor");

                Doctor doctor = new Doctor();
                doctor.UserId = user.Id;
                doctor.EmailAddress = user.Email;
                doctor.SurName = user.SurName;
                doctor.FirstName = user.FirstName;
                doctor.OtherName = user.OtherName;
                doctor.MobileNo = user.MobileNo;
                doctor.Address = user.Address;
                doctor.Status = user.EntityStatus;
                doctor.ProfilePicture = "/uploads/" + fileName;
                doctor.BloodGroupId = BloodGpId;
                doctor.Designation = designation;
                doctor.DeptId = deptId;
                doctor.Biography = biography;
                doctor.Specialist = specialist;
                doctor.Education = education;
                
                _context.Doctor.Add(doctor);
                await _context.SaveChangesAsync();

                var docReg = await _context.Doctor.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = docReg.Id.ToString("D6");
                docReg.DoctorReg = "DOC" + "/" + numberid;
                _context.Entry(docReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<string> NewAccountantAsync(RegisterViewModel model, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                ProfilePicture = "/uploads/" + fileName,
                EntityStatus = EntityStatus.Active
               
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Accountant");

                Accountants acct = new Accountants();
                acct.UserId = user.Id;
                acct.EmailAddress = user.Email;
                acct.SurName = user.SurName;
                acct.FirstName = user.FirstName;
                acct.OtherName = user.OtherName;
                acct.MobileNo = user.MobileNo;
                acct.Address = user.Address;
                acct.Status = user.EntityStatus;
                acct.ProfilePicture = "/uploads/" + fileName;

                _context.Accountants.Add(acct);
                await _context.SaveChangesAsync();

                var acctReg = await _context.Accountants.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = acctReg.Id.ToString("D6");
                acctReg.EmployeeReg = "ACC" + "/" + numberid;
                _context.Entry(acctReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<string> NewPatientAsync(RegisterViewModel model, List<IFormFile> files, int BloodGpId)
        {
            //var officer = await HttpContext.IdentityUser.GetUserName();

            //var officer = HttpContext.Current.User.Identity.GetUserName();
            //if (officer == "SuperAdmin")
            //{
            //    officer = "Admin";
            //}
            var fileName = await _dotnetdesk.UploadFile(files, _env);
            //try to update the user profile pict
            //ApplicationUser appUser = await _userManager.GetUserAsync(User);
            //appUser.ProfilePictureUrl = "/uploads/" + fileName;


            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                EntityStatus = EntityStatus.Active,
                ProfilePicture = "/uploads/" + fileName
                //RegisteredBy = officer
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Patient");

                Patient patient = new Patient();
                patient.UserId = user.Id;
                patient.EmailAddress = user.Email;
                patient.SurName = user.SurName;
                patient.FirstName = user.FirstName;
                patient.OtherName = user.OtherName;
                patient.MobileNo = user.MobileNo;
                patient.Address = user.Address;
                patient.Status = user.EntityStatus;
                patient.BloodGroupId = BloodGpId;
                patient.ProfilePicture = "/uploads/" + fileName;

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                var patReg = await _context.Patients.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = patReg.Id.ToString("D6");
                patReg.PatientReg = "PAT" + "/" + numberid;
                _context.Entry(patReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task RemoveUserToRoleAsync(string userId, string rolename)
        {
           

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, rolename);
            }
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser model)
        {
            try
            {
               
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<List<ApplicationUser>> UserAllAsync()
        {
            var users = _userManager.Users.Where(x => x.UserName != "SuperAdmin" ).OrderBy(x => x.UserName);
            return await users.ToListAsync();
        }

        public async Task<List<ApplicationUser>> UsersAsync()
        {
            return (await _userManager.Users.ToListAsync());
        }

        public int CountString(string searchString)
        {
            int result = 0;

            searchString = searchString.Trim();

            if (searchString == "")
                return 0;

            while (searchString.Contains("  "))
                searchString = searchString.Replace("  ", " ");

            foreach (string y in searchString.Split(' '))

                result++;


            return result;
        }

        public async Task<string> NewLaboratoristAsync(RegisterViewModel model, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                ProfilePicture = "/uploads/" + fileName,
                EntityStatus = EntityStatus.Active

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Laboratorist");

                Laboratorist lab = new Laboratorist();
                lab.UserId = user.Id;
                lab.EmailAddress = user.Email;
                lab.SurName = user.SurName;
                lab.FirstName = user.FirstName;
                lab.OtherName = user.OtherName;
                lab.MobileNo = user.MobileNo;
                lab.Address = user.Address;
                lab.Status = user.EntityStatus;
                lab.ProfilePicture = "/uploads/" + fileName;

                _context.Laboratorists.Add(lab);
                await _context.SaveChangesAsync();

                var labReg = await _context.Laboratorists.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = labReg.Id.ToString("D6");
                lab.EmployeeReg = "LAB" + "/" + numberid;
                _context.Entry(labReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<Laboratorist> GetLaboratoristAsync(int? id)
        {
            var lab = await _context.Laboratorists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (lab != null)
            {
                return lab;
            }
            return null;
        }

        public async Task<string> NewNurseAsync(RegisterViewModel model, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                ProfilePicture = "/uploads/" + fileName,
                EntityStatus = EntityStatus.Active

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Nurse");

                Nurse nur = new Nurse();
                nur.UserId = user.Id;
                nur.EmailAddress = user.Email;
                nur.SurName = user.SurName;
                nur.FirstName = user.FirstName;
                nur.OtherName = user.OtherName;
                nur.MobileNo = user.MobileNo;
                nur.Address = user.Address;
                nur.Status = user.EntityStatus;
                nur.ProfilePicture = "/uploads/" + fileName;

                _context.Nurse.Add(nur);
                await _context.SaveChangesAsync();

                var nurReg = await _context.Nurse.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = nurReg.Id.ToString("D6");
                nur.EmployeeReg = "NUR" + "/" + numberid;
                _context.Entry(nurReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<Nurse> GetNurseAsync(int? id)
        {
            var nurse = await _context.Nurse.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (nurse != null)
            {
                return nurse;
            }
            return null;
        }

        public async Task<string> NewPharmacistAsync(RegisterViewModel model, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                ProfilePicture = "/uploads/" + fileName,
                EntityStatus = EntityStatus.Active

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Pharmacist");

                Pharmacist pha = new Pharmacist();
                pha.UserId = user.Id;
                pha.EmailAddress = user.Email;
                pha.SurName = user.SurName;
                pha.FirstName = user.FirstName;
                pha.OtherName = user.OtherName;
                pha.MobileNo = user.MobileNo;
                pha.Address = user.Address;
                pha.Status = user.EntityStatus;
                pha.ProfilePicture = "/uploads/" + fileName;

                _context.Pharmacists.Add(pha);
                await _context.SaveChangesAsync();

                var phaReg = await _context.Pharmacists.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = phaReg.Id.ToString("D6");
                pha.EmployeeReg = "PHA" + "/" + numberid;
                _context.Entry(phaReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<Pharmacist> GetPharmacistAsync(int? id)
        {
            var pha = await _context.Pharmacists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (pha != null)
            {
                return pha;
            }
            return null;
        }

        public async Task<string> NewReceptionistAsync(RegisterViewModel model, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                MobileNo = model.MobileNo,
                Address = model.Address,
                ProfilePicture = "/uploads/" + fileName,
                EntityStatus = EntityStatus.Active

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Receptionist");

                Receptionist recp = new Receptionist();
                recp.UserId = user.Id;
                recp.EmailAddress = user.Email;
                recp.SurName = user.SurName;
                recp.FirstName = user.FirstName;
                recp.OtherName = user.OtherName;
                recp.MobileNo = user.MobileNo;
                recp.Address = user.Address;
                recp.Status = user.EntityStatus;
                recp.ProfilePicture = "/uploads/" + fileName;

                _context.Receptionists.Add(recp);
                await _context.SaveChangesAsync();

                var recpReg = await _context.Receptionists.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = recpReg.Id.ToString("D6");
                recp.EmployeeReg = "REC" + "/" + numberid;
                _context.Entry(recpReg).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<Receptionist> GetReceptionistAsync(int? id)
        {
            var recp = await _context.Receptionists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (recp != null)
            {
                return recp;
            }
            return null;
        }

       

        public async Task<bool> UpdateDoctorAsync(ApplicationUser model)
        {
            try
            {
               

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<bool> UpdateAccountantAsync(ApplicationUser model)
        {
            try
            {
               
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<bool> UpdateLaboratoristAsync(ApplicationUser model)
        {
            try
            {
                ;

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<bool> UpdateNurseAsync(ApplicationUser model)
        {
            try
            {
               
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<bool> UpdatePharmacistAsync(ApplicationUser model)
        {
            try
            {
               

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<bool> UpdateReceptionistAsync(ApplicationUser model)
        {
            try
            {
                

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

        public async Task<bool> UpdatePatientAsync(ApplicationUser model)
        {
            try
            {
               

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception c)
            {

            }

            return false;
        }

       
    }
}
