using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWaterProject.Models;

public class Admin
{
    [Key]
    public int AdminId { get; set; }

    public int FirstName { get; set; }

    public int LastName { get; set; }

    public int UserRoleId { get; set; }
}
