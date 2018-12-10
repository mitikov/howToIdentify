namespace Sitecore.ContactIdentification.Sample.Logic
{
  using System;
  using System.Text;
  using Sitecore.Abstractions;
  using Sitecore.Caching;
  using Sitecore.Diagnostics;

  /// <summary>
  /// Allows to set <see cref="MessageBuilder"/> that will get messages written into.
  /// <para>That might be useful to log single request processing.</para>
  /// </summary>
  /// <seealso cref="Sitecore.Abstractions.BaseLog" />
  public class InRequestLog : BaseLog
  {
    #region Fields
    [NotNull]
    private readonly BaseLog baseLogImplementation;

    #endregion

    public InRequestLog([NotNull]BaseLog inner)
    {
      Assert.ArgumentNotNull(inner, "inner");
      this.baseLogImplementation = inner;
    }

    public StringBuilder MessageBuilder { get; set; }

    #region Modified methods
    public override void Error(string message, Exception exception, object owner)
    {
      this.baseLogImplementation.Error(message, exception, owner);

      var messageBuilder = this.MessageBuilder;

      if (messageBuilder != null)
      {
        messageBuilder.AppendLine("Exception: " + message + Environment.NewLine + exception);
      }
    }

    public override void Info(string message, object owner)
    {
      this.baseLogImplementation.Info(message, owner);

      var messageBuilder = this.MessageBuilder;

      if (messageBuilder != null)
      {
        messageBuilder.AppendLine("INFO: " + message);
      }
    }

    public override void Warn(string message, object owner)
    {
      this.baseLogImplementation.Warn(message, owner);

      var messageBuilder = this.MessageBuilder;

      if (messageBuilder != null)
      {
        messageBuilder.AppendLine("WARN: " + message);
      }
    }

    #endregion

    #region Pure proxied
    public override void Audit(string message, object owner)
    {
      this.baseLogImplementation.Audit(message, owner);
    }

    public override void Audit(string message, string loggerName)
    {
      this.baseLogImplementation.Audit(message, loggerName);
    }

    public override void Audit(string message, Type ownerType)
    {
      this.baseLogImplementation.Audit(message, ownerType);
    }

    public override void Audit(Type ownerType, string format, params string[] parameters)
    {
      this.baseLogImplementation.Audit(ownerType, format, parameters);
    }

    public override void Audit(object owner, string format, params string[] parameters)
    {
      this.baseLogImplementation.Audit(owner, format, parameters);
    }

    public override void Debug(string message, object owner)
    {
      this.baseLogImplementation.Debug(message, owner);
    }

    public override void Debug(string message)
    {
      this.baseLogImplementation.Debug(message);
    }

    public override void Debug(string message, string loggerName)
    {
      this.baseLogImplementation.Debug(message, loggerName);
    }

    public override void Error(string message, object owner)
    {
      this.baseLogImplementation.Error(message, owner);
    }

    public override void Error(string message, Type ownerType)
    {
      this.baseLogImplementation.Error(message, ownerType);
    }

    public override void Error(string message, Exception exception, Type ownerType)
    {
      this.baseLogImplementation.Error(message, exception, ownerType);
    }

    public override void Error(string message, Exception exception, string loggerName)
    {
      this.baseLogImplementation.Error(message, exception, loggerName);
    }

    public override void Fatal(string message, object owner)
    {
      this.baseLogImplementation.Fatal(message, owner);
    }

    public override void Fatal(string message, Type ownerType)
    {
      this.baseLogImplementation.Fatal(message, ownerType);
    }

    public override void Fatal(string message, string loggerName)
    {
      this.baseLogImplementation.Fatal(message, loggerName);
    }

    public override void Fatal(string message, Exception exception, object owner)
    {
      this.baseLogImplementation.Fatal(message, exception, owner);
    }

    public override void Fatal(string message, Exception exception, Type ownerType)
    {
      this.baseLogImplementation.Fatal(message, exception, ownerType);
    }

    public override void Info(string message, string loggerName)
    {
      this.baseLogImplementation.Info(message, loggerName);
    }

    public override void SingleError(string message, object owner)
    {
      this.baseLogImplementation.SingleError(message, owner);
    }

    public override void SingleFatal(string message, Exception exception, object owner)
    {
      this.baseLogImplementation.SingleFatal(message, exception, owner);
    }

    public override void SingleFatal(string message, Exception exception, Type ownerType)
    {
      this.baseLogImplementation.SingleFatal(message, exception, ownerType);
    }

    public override void SingleWarn(string message, object owner)
    {
      this.baseLogImplementation.SingleWarn(message, owner);
    }

    public override void Warn(string message, Exception exception, object owner)
    {
      this.baseLogImplementation.Warn(message, exception, owner);
    }

    public override void Warn(string message, Exception exception, string loggerName)
    {
      this.baseLogImplementation.Warn(message, exception, loggerName);
    }

    public override bool Enabled
    {
      get
      {
        return this.baseLogImplementation.Enabled;
      }
    }

    public override bool IsDebugEnabled
    {
      get
      {
        return this.baseLogImplementation.IsDebugEnabled;
      }
    }

    public override ICache Singles
    {
      get
      {
        return this.baseLogImplementation.Singles;
      }
    }
    #endregion

  }
}

