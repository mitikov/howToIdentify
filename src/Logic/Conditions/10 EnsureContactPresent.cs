namespace Sitecore.ContactIdentification.Sample.Logic.Conditions
{
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.ContactIdentification.Sample.Abstractions;

  public class VerifyTrackerHasContact : PreconditionChecker
  {
    public VerifyTrackerHasContact([NotNull]BaseLog log) : base(log)
    {
    }

    public override bool ConditionVerified(ITracker tracker, IdentificationArgs args)
    {
      if (tracker == null)
      {
        this.Log.Warn("No tracker", this);
        return false;
      }

      if (tracker.Contact == null)
      {
        this.Log.Warn("No contact", this);
        return false;
      }

      return true;
    }
  }
}
