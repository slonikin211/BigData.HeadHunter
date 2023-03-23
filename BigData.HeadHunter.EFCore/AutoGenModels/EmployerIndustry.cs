using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Keyless]
[Table("Employer.Industries")]
public partial class EmployerIndustry
{
    public long EmployerId { get; set; }

    public double IndustryId { get; set; }

    [ForeignKey("EmployerId")]
    public virtual Employer Employer { get; set; } = null!;

    [ForeignKey("IndustryId")]
    public virtual Industry Industry { get; set; } = null!;
}
