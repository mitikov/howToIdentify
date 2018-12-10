namespace Sitecore.ContactIdentification.Sample.Logic.Identification
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.ContactIdentification.Sample.Abstractions;

  public class IdentificationOnlyAfterPreconditions : IdentificationHelper
  {
    protected IReadOnlyList<PreconditionChecker> Preconditions { get; private set; }

    protected IdentificationHelper IdentificationHelper { get; private set; }

    public IdentificationOnlyAfterPreconditions(IReadOnlyList<PreconditionChecker> preconditions, IdentificationHelper identificationHelper)
    {
      this.Preconditions = preconditions;
      this.IdentificationHelper = identificationHelper;
    }

    public override void DoIdentification(ITracker tracker, IdentificationArgs args)
    {
      var allMet = this.Preconditions.All(precondition => precondition.ConditionVerified(tracker, args));

      if (allMet)
      {
        this.IdentificationHelper.DoIdentification(tracker, args);
      }
    }
  }
}
