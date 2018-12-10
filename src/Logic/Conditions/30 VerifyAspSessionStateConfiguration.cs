namespace Sitecore.ContactIdentification.Sample.Logic.Conditions
{
  using System.Web;
  using System.Web.SessionState;
  using Sitecore.Abstractions;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Configuration;
  using Sitecore.ContactIdentification.Sample.Abstractions;

  public class VerifyAspSessionStateConfiguration : PreconditionChecker
  {
    #region Constructor
    public VerifyAspSessionStateConfiguration(BaseLog log) : base(log)
    {
    }

    #endregion

    public override bool ConditionVerified(ITracker tracker, IdentificationArgs args)
    {
      var session = HttpContext.Current?.Session;
      HttpSessionStateBase wrapper = session != null ? new HttpSessionStateWrapper(session) : null;

      return this.SessionConfiguredCorrectly(tracker, args, wrapper);
    }

    protected virtual bool SessionConfiguredCorrectly(ITracker tracker, IdentificationArgs args, [CanBeNull] HttpSessionStateBase session)
    {
      if (session == null)
      {
        this.Log.Warn("No ASP.NET session set", owner: this);
        return false;
      }

      if (session.IsReadOnly)
      {
        this.Log.Warn("Session is readonly", owner: this);
        return false;
      }

      if (session.Mode == SessionStateMode.InProc)
      {
        this.Log.Warn("Session is set to inProc." +
                      " Each application instance will get its own session copy, thus we'll get multiple sessions with same ids," +
                      " and non of them will carry all the data", owner: this);
        return false;
      }

      if (session.Mode == SessionStateMode.SQLServer || session.Mode == SessionStateMode.StateServer)
      {
        this.Log.Warn("Session type does not support SessionEnd event, thereby no data is getting aggregated.", owner: this);
        return false;
      }

      // Other sessions must support session end.
      return true;
    }
  }
}
