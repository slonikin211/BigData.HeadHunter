using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Table("Dictionary.Values")]
public partial class DictionaryValue
{
    [Key]
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string KeyId { get; set; } = null!;

    [ForeignKey("KeyId")]
    [InverseProperty("DictionaryValues")]
    public virtual DictionaryKey Key { get; set; } = null!;
}
