namespace Sitecore.ContactIdentification.Sample.Logic.Conditions
{
  using System;
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.ContactIdentification.Sample.Abstractions;
  using Sitecore.Diagnostics;

  /// <summary>
  /// Ensures identifier is different from the one we are trying to store.
  /// </summary>
  /// <seealso cref="Sitecore.ContactIdentification.Sample.Abstractions.PreconditionChecker" />
  public class VerifyDifferentIdentifiers : PreconditionChecker
  {
    public VerifyDifferentIdentifiers([NotNull]BaseLog log) : base(log)
    {
    }

    public override bool ConditionVerified([NotNull]ITracker tracker, [NotNull]IdentificationArgs args)
    {
      Assert.ArgumentNotNull(tracker, "tracker");
      Assert.ArgumentNotNull(args, "args");

      var contact = tracker.Contact;
      var existingIdentifier = contact.Identifiers.Identifier;

      var sameIdentifiers = string.Equals(existingIdentifier, args.Identifier, StringComparison.OrdinalIgnoreCase);

      if (sameIdentifiers)
      {
        this.Log.Warn("Contact identifier is same as previously, identification skipped.", owner: this);
        return false;
      }
      
      return true;
    }
  }
}
