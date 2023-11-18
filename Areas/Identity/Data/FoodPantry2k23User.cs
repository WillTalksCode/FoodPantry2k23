using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FoodPantry2k23.Areas.Identity.Data;

// Add profile data for application users by adding properties to the FoodPantry2k23User class
public class FoodPantry2k23User : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    [PersonalData]
    public string? LastName { get; set; }

}

