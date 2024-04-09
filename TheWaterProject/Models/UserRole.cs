using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWaterProject.Models;

public partial class UserRole
{
    [Key]
    public int UserRoleId { get; set; }

    public string Description { get; set; } = null!;
}
