namespace Sitecore.ContactIdentification.Sample.Logic.Conditions
{
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.ContactIdentification.Sample.Abstractions;
  using Sitecore.Xdb.Configuration;

  /// <summary>
  /// Verifies valid license is in place, xDB is On, and tracking is On as well.
  /// </summary>
  /// <seealso cref="Sitecore.ContactIdentification.Sample.Abstractions.PreconditionChecker" />
  public class VerifyAnalyticConfiguration : PreconditionChecker
  {
    #region Fields
    private readonly bool xdbEnabled;
    private readonly bool hasValidLicense;
    private readonly bool xdbTrackingEnabled;
    #endregion

    #region Constructors 
    public VerifyAnalyticConfiguration([NotNull]BaseLog log)
  : this(log, XdbSettings.Enabled, XdbSettings.HasValidLicense, XdbSettings.Tracking.Enabled)
    {
    }

    protected VerifyAnalyticConfiguration(BaseLog log, bool xdbEnabled, bool hasValidLicense, bool xdbTrackingEnabled)
      : base(log)
    {
      this.xdbEnabled = xdbEnabled;
      this.hasValidLicense = hasValidLicense;
      this.xdbTrackingEnabled = xdbTrackingEnabled;
    }

    #endregion


    public override bool ConditionVerified([CanBeNull]ITracker tracker, [CanBeNull]IdentificationArgs args)
    {
      if (!this.hasValidLicense)
      {
        this.Log.Warn("xDB does not have valid license", this);
        return false;
      }

      if (!this.xdbEnabled)
      {
        this.Log.Warn("xDB is disabled", this);
        return false;
      }

      if (!this.xdbTrackingEnabled)
      {
        this.Log.Warn("xDB tracking is disabled", this);
        return false;
      }

      return true;
    }
  }
}
