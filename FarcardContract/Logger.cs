using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Linq;

namespace FarcardContract
{
    public class Logger<T>
    {
        // LoggingConfiguration config = new LoggingConfiguration();
        Logger logs = null;
        FileTarget fileTarget = new FileTarget();
        LogLevel _minLevel = LogLevel.Trace;


        // string ProcessName = "${processname}";
        string NAMETRACE = "${basedir}/log/${shortdate}/trace.log";
        string LOGNAME = "${basedir}/log/${shortdate}/" + $"{GetFormattedName(typeof(T))}.log";
        public void Error(string st)
        {
            fileTarget.FileName = LOGNAME;
            fileTarget.Layout = @"${longdate} ${Level} ${message}";
            logs.Error(st);
        }
        public void Debug(string st)
        {
            //  fileTarget.FileName = LOGNAME;
            //   fileTarget.Layout = @"${longdate} ${Level} ${message}";
            logs.Debug(st);
        }
        public void ToLog(string st)
        {
            fileTarget.FileName = NAMETRACE;
            fileTarget.Layout = @"${longdate} ${message}";

            logs.Debug(st);
        }
        public void Trace(string st)
        {
            fileTarget.FileName = LOGNAME;
            fileTarget.Layout = @"${longdate} ${Level} ${message}";
            logs.Trace(st);
        }

        public Logger(int minLevel = 0)
        {
            var loggername = GetFormattedName(typeof(T));
            fileTarget.FileName = LOGNAME;//.Replace("`","_");
            fileTarget.Layout = @"${longdate} ${Level} ${message}";
            fileTarget.Name = "file" + loggername;

            LoggingRule rul = new LoggingRule(loggername, LogLevel.Trace, fileTarget);



            if (LogManager.Configuration == null)
            {
                var config = new LoggingConfiguration();
                config.AddTarget(fileTarget.Name, fileTarget);
                config.LoggingRules.Add(rul);

                LogManager.Configuration = config;
            }
            else
            {
                var target = LogManager.Configuration.FindTargetByName(fileTarget.Name);
                if (target == null)
                {
                    LogManager.Configuration.AddTarget(fileTarget.Name, fileTarget);
                }
                var logrule = LogManager.Configuration.LoggingRules.FirstOrDefault(_ => rul.LoggerNamePattern == _.LoggerNamePattern);
                if (logrule == null)
                    LogManager.Configuration.LoggingRules.Add(rul);
                {
                    LogManager.Configuration.LoggingRules.Remove(rul);
                    rul = new LoggingRule(loggername, _minLevel, fileTarget);
                }
                LogManager.ReconfigExistingLoggers();
            }
            logs = LogManager.GetLogger(loggername);

        }
        void SetLevel(int minLevel = 0)
        {
            switch (minLevel)
            {
                case -1:
                    {
                        _minLevel = LogLevel.Off;
                        break;
                    }
                case 1:
                    {
                        _minLevel = LogLevel.Fatal;
                        break;
                    }
                case 2:
                    _minLevel = LogLevel.Error;
                    break;
                default:
                    {
                        _minLevel = LogLevel.Info;
                        break;
                    }
            }
        }
        static string GetFormattedName(Type type)
        {
            if (type.IsGenericType)
            {
                string genericArguments = type.GetGenericArguments()
                                    .Select(x => x.Name)
                                    .Aggregate((x1, x2) => $"{x1}, {x2}");
                return $"{type.Name.Substring(0, type.Name.IndexOf("`"))}"
                     + $"[{genericArguments}]";
            }
            return type.Name;
        }

    }
}
