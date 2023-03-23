using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class DictionaryValue
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string KeyId { get; set; } = null!;

    public virtual DictionaryKey Key { get; set; } = null!;
}
