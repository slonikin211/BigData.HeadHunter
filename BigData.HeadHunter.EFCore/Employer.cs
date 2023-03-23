using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class Employer
{
    public long Id { get; set; }

    public long Trusted { get; set; }

    public string Name { get; set; } = null!;

    public long? AreaId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
