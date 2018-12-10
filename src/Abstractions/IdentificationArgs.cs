namespace Sitecore.ContactIdentification.Sample.Abstractions
{
  using System.Diagnostics;
  using Sitecore.Diagnostics;

  /// <summary>
  /// Carries the information for contact identification - identifier, and facet data.
  /// </summary>
  [DebuggerDisplay("{Identifier}")]
  public class IdentificationArgs
  {
    [NotNull]
    public string Identifier { get; private set; }

    public string Name { get; set; }

    public IdentificationArgs([NotNull]string identifier)
    {
      Assert.ArgumentNotNullOrEmpty(identifier, "identifier");
      this.Identifier = identifier;
    }
  }
}
