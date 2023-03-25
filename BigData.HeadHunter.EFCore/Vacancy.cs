using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class Vacancy
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? SalaryFrom { get; set; }

    public long? SalaryTo { get; set; }

    public string? SalaryCurrency { get; set; }

    public long? SalaryGross { get; set; }

    public long? AreaId { get; set; }

    public string? Url { get; set; }

    public string? PublishedDate { get; set; }

    public string? CreatedDate { get; set; }

    public long? EmployerId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual Employer? Employer { get; set; }
}
