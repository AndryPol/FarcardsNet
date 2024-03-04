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
        private readonly Logger _logs = null;
        private readonly FileTarget _fileTarget = new FileTarget();
        private LogLevel _minLevel = LogLevel.Trace;
        private readonly bool _console = false;


        // string ProcessName = "${processname}";
        //  private string _NAMETRACE = "${basedir}/log/${shortdate}/trace.log";
        private readonly string _LOGNAME = "${basedir}/log/${shortdate}/" + $"{GetFormattedName(typeof(T))}.log";

        public void Error(string message)
        {
            //  SetSettings();
            _logs.Error(message);

        }

        public void Error(Exception ex)
        {
            // SetSettings();
            _logs.Error(ex);
        }

        public void Error(string message, Exception ex)
        {
            _logs.Error(ex, message);
        }


        //void SetSettings()
        //{
        //    _fileTarget.FileName = _LOGNAME;
        //    _fileTarget.Layout = @"${longdate} ${Level} ${message}";
        //}

        public void Debug(string message)
        {
            //  SetSettings();
            _logs.Debug(message);
        }

        public void Info(string message)
        {
            _logs.Info(message);
        }

        public void Warn(string message)
        {
            _logs.Warn(message);
        }

        public void Fatal(string message)
        {
            _logs.Fatal(message);
        }

        //public void ToLog(string message)
        //{
        //    _fileTarget.FileName = _NAMETRACE;
        //    _fileTarget.Layout = @"${longdate} ${message}";

        //    _logs.Debug(message);
        //}
        public void Trace(string message)
        {
            //SetSettings();
            _logs.Trace(message);
        }

        public Logger(int minLevel = 5, bool console = false)
        {
            _console = console;
            SetLevel(minLevel);

            var loggername = Reconfigure();
            _logs = LogManager.GetLogger(loggername);
        }

        public Logger GetLogger()
        {
            return _logs;
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
            _fileTarget.FileName = _LOGNAME; //.Replace("`","_");
            _fileTarget.Layout = @"${longdate} ${Level} ${message}";
            _fileTarget.Name = "file" + loggername;
            var consolename = $"Console{loggername}";
            LoggingRule rul = new LoggingRule(loggername, _minLevel, _fileTarget);
            var consoleTarget = new ConsoleTarget()
            {
                AutoFlush = true,
                Name = loggername,
                Layout = _fileTarget.Layout,
                DetectConsoleAvailable = true,
            };
            var consoleRule = new LoggingRule(loggername, _minLevel, consoleTarget);

            if (LogManager.Configuration == null)
            {
                var config = new LoggingConfiguration();
                config.AddTarget(_fileTarget.Name, _fileTarget);
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
                var target = LogManager.Configuration.FindTargetByName(_fileTarget.Name);
                if (target == null)
                {
                    LogManager.Configuration.AddTarget(_fileTarget.Name, _fileTarget);
                }

                if (_console)
                {
                    var _consoleTarget = LogManager.Configuration.AllTargets.FirstOrDefault(x => x.Name == consolename && x.GetType() == typeof(ConsoleTarget));
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
