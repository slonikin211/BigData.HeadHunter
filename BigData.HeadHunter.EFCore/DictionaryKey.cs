using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class DictionaryKey
{
    public string Id { get; set; } = null!;

    public virtual ICollection<DictionaryValue> DictionaryValues { get; } = new List<DictionaryValue>();
}
