using System;
using System.Collections;
using System.Configuration;

using Node.Core;
using Node.Core.Biz.Handler.WebMethods;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Logging;
using Node.Core.Util;

namespace Node.TaskHandler
{
    public class NodeTask
    {
        static void Main(string[] args)
        {
            try
            {
                SystemConfiguration config = new SystemConfiguration();
                Phrase.LoggerPath = ConfigurationManager.AppSettings[Phrase.LOG_PATH_KEY];
                Phrase.LoggerLevel = config.GetLoggingLevel("Task");
            }
            catch (Exception e)
            {
                Logger logger = new Logger(System.AppDomain.CurrentDomain.BaseDirectory + "FatalExceptionLog.txt", Logger.LEVEL_DEBUG);
                logger.Log(e);
            }
            if (args != null && args.Length > 0)
            {
                try
                {
                    if (args.Length == 1 || args.Length == 2)
                    {
                        Node.Core.Biz.Handler.TaskHandler handler = new Node.Core.Biz.Handler.TaskHandler(args[0]);
                        handler.Execute();
                    }
                    else if (args.Length >= 9)
                    {
                        string[] parameters = null;
                        if (args.Length > 9)
                        {
                            parameters = new string[args.Length - 9];
                            for (int i = 0; i < args.Length - 9; i++)
                                parameters[i] = args[9 + i];
                        }
                        SolicitProcess process = new SolicitProcess(args[1], int.Parse(args[2]), args[3], args[4], args[5], args[6], args[7], args[8], parameters);
                        process.Execute();
                    }
                    else
                    {
                        Logger logger = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
                        logger.Log("No Task", "Incorrect Input Parameters for Task", Logger.LEVEL_WARN);
                    }
                }
                catch (Exception e)
                {
                    Logger logger = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
                    logger.Log(e);
                }
            }
            else
            {
                Logger logger = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
                logger.Log("No Task", "No Input Parameters for Task", Logger.LEVEL_WARN);
            }
        }
    }
}
