namespace Sitecore.ContactIdentification.Sample.Logic.Identification
{
  using System;
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.ContactIdentification.Sample.Abstractions;
  using Sitecore.StringExtensions;

  /// <summary>
  /// Logs the process of identification into logs.
  /// </summary>
  /// <seealso cref="Sitecore.ContactIdentification.Sample.Abstractions.LogAwareIdentificationWrapper" />
  public class IdentificationProcessLoggerWrapper : LogAwareIdentificationWrapper
  {    
    private static readonly string ContactUpdateBeginMessage = "Starting to update contact id {0}";
    private static readonly string ContactUpdateEndMessage = "Contact id {0} was successfully updated";

    private readonly IdentificationHelper identificationHelper;

    public IdentificationProcessLoggerWrapper(IdentificationHelper identificationHelper, BaseLog log) : base(log)
    {
      this.identificationHelper = identificationHelper;
    }

    public override void DoIdentification(ITracker tracker, IdentificationArgs args)
    {
      try
      {
        var contact = tracker.Contact;

        this.Log.Info(ContactUpdateBeginMessage.FormatWith(contact.ContactId), this);

        this.identificationHelper.DoIdentification(tracker, args);

        // Note: contact ID can change should a contact with same identifier exist in xDB.
        var freshContact = tracker.Contact;

        this.Log.Info(ContactUpdateEndMessage.FormatWith(freshContact.ContactId), this);
      }
      catch (Exception ex)
      {
        this.Log.Error("Could not identify contact", ex, this);
      }
    }
  }
}
