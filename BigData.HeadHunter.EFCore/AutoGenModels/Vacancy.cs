using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Table("Vacancy")]
[Index("Id", IsUnique = true)]
public partial class Vacancy
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    [Column("Salary.From")]
    public long? SalaryFrom { get; set; }

    [Column("Salary.To")]
    public long? SalaryTo { get; set; }

    [Column("Salary.Currency")]
    public string SalaryCurrency { get; set; } = null!;

    [Column("Salary.Gross")]
    public long SalaryGross { get; set; }

    public long? AreaId { get; set; }

    [Column("URL")]
    public string? Url { get; set; }

    public string? PublishedDate { get; set; }

    public string? CreatedDate { get; set; }

    public long EmployerId { get; set; }

    [ForeignKey("AreaId")]
    [InverseProperty("Vacancies")]
    public virtual Area? Area { get; set; }

    [ForeignKey("EmployerId")]
    [InverseProperty("Vacancies")]
    public virtual Employer Employer { get; set; } = null!;
}
