using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting.Serilog
{
    public class EnvironmentVariableLoggingLevelSwitch : LoggingLevelSwitch
    {
        public EnvironmentVariableLoggingLevelSwitch(string environmentVariable)
        {
            if (Enum.TryParse<LogEventLevel>(Environment.ExpandEnvironmentVariables(environmentVariable), true,
                    out var level))
                MinimumLevel = level;
        }
    }
}
