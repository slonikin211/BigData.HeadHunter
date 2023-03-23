using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Table("Area")]
[Index("Id", IsUnique = true)]
public partial class Area
{
    [Key]
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Name { get; set; } = null!;

    [InverseProperty("Area")]
    public virtual ICollection<Employer> Employers { get; } = new List<Employer>();

    [InverseProperty("Area")]
    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
