namespace Sitecore.ContactIdentification.Sample.Abstractions
{
  using Sitecore.Analytics;

  /// <summary>
  /// Does the identification operation.
  /// </summary>
  public abstract class IdentificationHelper
  {
    public abstract void DoIdentification(ITracker tracker, IdentificationArgs args);
  }
}