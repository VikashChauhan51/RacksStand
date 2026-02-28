using System;
using System.Collections.Generic;
using System.Text;

namespace RacksStands.Framework.Caching.Abstractions;

public interface ISerializer
{
    byte[] Serialize<T>(T obj);
    T Deserialize<T>(ReadOnlySpan<byte> data);
}
