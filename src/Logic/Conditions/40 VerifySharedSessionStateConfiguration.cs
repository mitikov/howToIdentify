namespace Sitecore.ContactIdentification.Sample.Logic.Conditions
{
  using System;
  using System.Reflection;
  using System.Web.SessionState;
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Configuration;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Analytics.Tracking.SharedSessionState;
  using Sitecore.ContactIdentification.Sample.Abstractions;
  using Sitecore.DependencyInjection;
  using Sitecore.Diagnostics;

  public class VerifySharedSessionConfiguration : PreconditionChecker
  {    
    private readonly SharedSessionStateManager sharedSessionStateManager;
    private readonly SessionStateStoreProviderBase sharedSessionProvider;

    #region Constructors

    public VerifySharedSessionConfiguration([NotNull]BaseLog log) :
  this(ServiceLocator.ServiceProvider.GetRequiredService<BaseFactory>(), log)
    {
    }

    public VerifySharedSessionConfiguration([NotNull]BaseFactory factory, BaseLog log) : this((ContactManager)factory.CreateObject("tracking/contactManager", assert: true), log)
    {
    }

    public VerifySharedSessionConfiguration([NotNull]ContactManager contactManager, BaseLog log) : this(ExtractSharedSessionState(contactManager), log)
    {
    }

    public VerifySharedSessionConfiguration([NotNull]SharedSessionStateManager sharedSessionStateManager, BaseLog log) : this(sharedSessionStateManager, ExtractSessionProvider(sharedSessionStateManager), log)
    {
    }


    public VerifySharedSessionConfiguration([NotNull]SharedSessionStateManager sharedSessionStateManager, [NotNull]SessionStateStoreProviderBase sharedSessionProvider, BaseLog log) : base(log)
    {
      this.sharedSessionStateManager = sharedSessionStateManager;
      this.sharedSessionProvider = sharedSessionProvider;
    }

    #endregion

    protected virtual string AnalyticsHostName
    {
      get
      {
        return AnalyticsSettings.HostName;
      }
    }

    public override bool ConditionVerified(ITracker tracker, IdentificationArgs args)
    {
      return this.SharedSessionProviderIsValid() && this.AnalyticsHostNameIsConfigured();
    }

    protected virtual bool SharedSessionProviderIsValid()
    {
      var otherThanInProc = this.sharedSessionProvider.GetType().FullName.IndexOf("InProcSessionState", StringComparison.OrdinalIgnoreCase) == -1;

      if (otherThanInProc)
      {
        return true;
      }

      this.Log.Warn("Shared session is configured to use inProc (Sitecore.Analytics.Tracking.config) - each server will have its own copy of the data.", owner: this);
      return false;
    }

    // TODO: improve - check actual redirect.
    protected virtual bool AnalyticsHostNameIsConfigured()
    {
      var blankHostName = string.IsNullOrWhiteSpace(this.AnalyticsHostName);

      if (blankHostName)
      {
        this.Log.Warn("No Analytics.Hostname is configured.", owner: this);
        return false;
      }

      return true;
    }

    #region Reflection dirty tricks

    [NotNull]
    private static SessionStateStoreProviderBase ExtractSessionProvider(SharedSessionStateManager sharedSessionStateManager)
    {
      var encapsulatedField = sharedSessionStateManager.GetType().GetField("provider", BindingFlags.Instance | BindingFlags.NonPublic);

      Assert.Required(encapsulatedField, "field that stores shared session provider");

      var sessionProvider = encapsulatedField.GetValue(sharedSessionStateManager) as SessionStateStoreProviderBase;

      return Assert.ResultNotNull(sessionProvider, "sharedSessionProvider");
    }

    [NotNull]
    private static SharedSessionStateManager ExtractSharedSessionState(ContactManager contactManager)
    {
      var encapsulatedField = typeof(ContactManager).GetField("sharedSessionStateManager", BindingFlags.Instance | BindingFlags.NonPublic);

      Assert.Required(encapsulatedField, "field that stores shared session in ContactManager");

      var sharedSessionStateManager = encapsulatedField.GetValue(contactManager) as SharedSessionStateManager;

      return Assert.ResultNotNull(sharedSessionStateManager, "sharedSessionStateManager");
    }

    #endregion
  }
}
