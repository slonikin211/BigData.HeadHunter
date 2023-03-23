using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Table("Employer")]
[Index("Id", IsUnique = true)]
public partial class Employer
{
    [Key]
    public long Id { get; set; }

    public long Trusted { get; set; }

    public string Name { get; set; } = null!;

    public long? AreaId { get; set; }

    [ForeignKey("AreaId")]
    [InverseProperty("Employers")]
    public virtual Area? Area { get; set; }

    [InverseProperty("Employer")]
    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
