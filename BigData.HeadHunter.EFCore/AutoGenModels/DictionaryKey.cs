using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

[Table("Dictionary.Keys")]
public partial class DictionaryKey
{
    [Key]
    public string Id { get; set; } = null!;

    [InverseProperty("Key")]
    public virtual ICollection<DictionaryValue> DictionaryValues { get; } = new List<DictionaryValue>();
}
