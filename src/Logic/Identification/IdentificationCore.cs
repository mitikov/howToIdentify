namespace Sitecore.ContactIdentification.Sample.Logic.Identification
{
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Tracking;
  using Sitecore.ContactIdentification.Sample.Abstractions;
  using Sitecore.Diagnostics;

  /// <summary>
  /// Responsible for identifying the contact.
  /// </summary>
  /// <seealso cref="Sitecore.ContactIdentification.Sample.Abstractions.IdentificationHelper" />
  public class IdentificationCore : IdentificationHelper
  {
    private static readonly string PersonalFacetName = "Personal";

    public override void DoIdentification([NotNull]ITracker tracker, [NotNull] IdentificationArgs args)
    {
      Assert.ArgumentNotNull(tracker, "tracker");
      Assert.ArgumentNotNull(args, "args");

      tracker.Session.Identify(args.Identifier);

      var identifiedContact = tracker.Contact;

      this.AssignFacets(identifiedContact, args);      
    }

    protected virtual void AssignFacets(Contact identifiedContact, IdentificationArgs args)
    {
      var personal = identifiedContact.GetFacet<IContactPersonalInfo>(PersonalFacetName);

      personal.FirstName = args.Name;
    }
  }
}
