namespace Sitecore.ContactIdentification.Sample.Logic
{
  using Sitecore.Abstractions;
  using Sitecore.ContactIdentification.Sample.Abstractions;
  using Sitecore.ContactIdentification.Sample.Logic.Conditions;
  using Sitecore.ContactIdentification.Sample.Logic.Identification;
  using Sitecore.Diagnostics;

  /// <summary>
  /// Gets a new instance of the <see cref="IdentificationHelper"/>.
  /// </summary>
  public class IdentifyHelperFactory
  {
    public virtual IdentificationHelper Produce([NotNull]BaseLog log)
    {
      Assert.ArgumentNotNull(log, "log");

      // The identification logic itself
      var identificationCore = new IdentificationCore();

      // Log if identification fails
      var logIdentificationProcess = new IdentificationProcessLoggerWrapper(identificationCore, log);

      // No need to assign same identifier to the contact twice.
      var skipIfContactHasSameIdentifier = new VerifyDifferentIdentifiers(log);      

      // Ensure Shared session is not InProc
      var verifySharedState = new VerifySharedSessionConfiguration(log);

      // Ensure ASP.NET session is there
      var verifyAspNetSessionConfig = new VerifyAspSessionStateConfiguration(log);

      // Ensure Analytics settings are correct
      var verifyAnalyticsOn = new VerifyAnalyticConfiguration(log);

      // Ensure contact is there
      var verifyContact = new VerifyTrackerHasContact(log);

      PreconditionChecker[] preconditions =
      {
        verifyContact, verifyAnalyticsOn, verifyAspNetSessionConfig, verifySharedState, skipIfContactHasSameIdentifier
      };

      return new IdentificationOnlyAfterPreconditions(preconditions, logIdentificationProcess);
    }
  }
}
