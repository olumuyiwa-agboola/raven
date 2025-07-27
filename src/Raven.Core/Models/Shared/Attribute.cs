namespace Raven.Core.Models.Shared;

/// <summary>
/// Represents a database attribute and its value.
/// </summary>
/// <remarks>This record is typically used to model a key-value pair where the key is the attribute name and
/// the value represents the associated data. The value can be null to indicate the absence of data for the
/// specified column.</remarks>
/// <param name="Name">The name of the database attribute.</param>
/// <param name="Value">The value of the database attribute.</param>
public record Attribute(string Name, string? Value)
{
    /// <summary>
    /// The name of the attribute.
    /// </summary>
    public string Name = Name;

    /// <summary>
    /// The value of the attribute.
    /// </summary>
    public string? Value = Value;
}
