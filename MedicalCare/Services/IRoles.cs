using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Services
{
    interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string UserId);
    }
}
