namespace Sitecore.ContactIdentification.Sample.Abstractions
{
  using Sitecore.Abstractions;
  using Sitecore.Diagnostics;

  public abstract class LogAwareIdentificationWrapper : IdentificationHelper
  {
    [NotNull]
    protected BaseLog Log { get; private set; }

    protected LogAwareIdentificationWrapper([NotNull]BaseLog log)
    {
      Assert.ArgumentNotNull(log, "log");
      this.Log = log;
    }
  }
}
