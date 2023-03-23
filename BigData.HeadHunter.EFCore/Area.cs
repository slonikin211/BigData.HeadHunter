using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class Area
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employer> Employers { get; } = new List<Employer>();

    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
