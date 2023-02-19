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
        bool _console = false;


        // string ProcessName = "${processname}";
        string NAMETRACE = "${basedir}/log/${shortdate}/trace.log";
        string LOGNAME = "${basedir}/log/${shortdate}/" + $"{GetFormattedName(typeof(T))}.log";

        public void Error(string st)
        {
          //  SetSettings();
            logs.Error(st);

        }

        public void Error(Exception ex)
        {
           // SetSettings();
            logs.Error(ex);
        }

        void SetSettings()
        {
            fileTarget.FileName = LOGNAME;
            fileTarget.Layout = @"${longdate} ${Level} ${message}";
        }

        public void Debug(string st)
        {
          //  SetSettings();
            logs.Debug(st);
        }
        //public void ToLog(string st)
        //{
        //    fileTarget.FileName = NAMETRACE;
        //    fileTarget.Layout = @"${longdate} ${message}";

        //    logs.Debug(st);
        //}
        public void Trace(string st)
        {
            //SetSettings();
            logs.Trace(st);
        }

        public Logger(int minLevel = 5, bool console = false)
        { 
            _console = console;
            SetLevel(minLevel);
           
            var loggername = Reconfigure();
            logs = LogManager.GetLogger(loggername);

        }

        public Logger GetLogger()
        {
            return logs;
        }

       public void SetLevel(int minLevel = 5)
        {
            switch (minLevel)
            {
                case -1:
                    {
                        _minLevel = LogLevel.Off;
                        break;
                    }
                case 0:
                    {
                        _minLevel = LogLevel.Fatal;
                        break;
                    }
                case 1:
                    _minLevel = LogLevel.Error;
                    break;
                case 2:
                    _minLevel = LogLevel.Warn;
                    break;
                case 3:
                    _minLevel = LogLevel.Info;
                    break;
                case 4:
                    _minLevel = LogLevel.Debug;
                    break;
                default:
                    {
                        _minLevel = LogLevel.Trace;
                        break;
                    }
            }

            Reconfigure();
        }


        string Reconfigure()
        {
            var loggername = GetFormattedName(typeof(T));
            fileTarget.FileName = LOGNAME; //.Replace("`","_");
            fileTarget.Layout = @"${longdate} ${Level} ${message}";
            fileTarget.Name = "file" + loggername;
            var consolename = $"Console{loggername}";
            LoggingRule rul = new LoggingRule(loggername, _minLevel, fileTarget);
            var consoleTarget = new ConsoleTarget();
            consoleTarget.AutoFlush = true;
            consoleTarget.Name = loggername;
            consoleTarget.Layout = fileTarget.Layout;
            consoleTarget.DetectConsoleAvailable = true;

            var consoleRule = new LoggingRule(loggername, _minLevel, consoleTarget);

            if (LogManager.Configuration == null)
            {
                var config = new LoggingConfiguration();
                config.AddTarget(fileTarget.Name, fileTarget);
                if (_console)
                {
                    config.AddTarget(loggername, consoleTarget);
                    config.LoggingRules.Add(consoleRule);
                }
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

                if (_console)
                {
                    var _consoleTarget = LogManager.Configuration.AllTargets.FirstOrDefault(x=>x.Name==consolename&&x.GetType() == typeof(ConsoleTarget));
                    if (_consoleTarget == null)
                        LogManager.Configuration.AddTarget(consoleTarget.Name, consoleTarget);
                }

                var logrule = LogManager.Configuration.LoggingRules.FirstOrDefault(_ => rul.LoggerNamePattern == _.LoggerNamePattern);
                if (logrule != null)
                {
                    LogManager.Configuration.LoggingRules.Remove(logrule);
                }

                var _consoleRule =
                    LogManager.Configuration.LoggingRules.FirstOrDefault(_ =>
                        consoleRule.LoggerNamePattern == _.LoggerNamePattern);
                if (_consoleRule != null)
                    LogManager.Configuration.LoggingRules.Remove(_consoleRule);

                LogManager.Configuration.LoggingRules.Add(rul);
                if (_console)
                    LogManager.Configuration.LoggingRules.Add(consoleRule);

                LogManager.ReconfigExistingLoggers();
            }

            return loggername;
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
