using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Table("Industry")]
public partial class Industry
{
    [Key]
    public double Id { get; set; }

    public string Name { get; set; } = null!;

    public long? ParentId { get; set; }
}
