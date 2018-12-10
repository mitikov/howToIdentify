namespace Sitecore.ContactIdentification.Sample.Abstractions
{
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.Diagnostics;

  public abstract class PreconditionChecker
  {
    [NotNull]
    protected BaseLog Log { get; private set; }

    protected PreconditionChecker([NotNull]BaseLog log)
    {
      Assert.ArgumentNotNull(log, "log");
      this.Log = log;
    }

    public abstract bool ConditionVerified(ITracker tracker, IdentificationArgs args);
  }
}
