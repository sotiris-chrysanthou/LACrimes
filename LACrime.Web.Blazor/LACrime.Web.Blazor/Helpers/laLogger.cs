namespace LACrimes.Web.Blazor.Server.Helpers {
    public static class laLogger {

        public static async Task Log(ILogger logger, string message, LogType logType, bool throwEx = false, Exception ex = null!) {
            switch(logType) {
                case LogType.Information:
                    message = DateTime.Now + " - " + "Information: " + message + Environment.NewLine;
                    System.IO.File.AppendAllText("ErrorLog.txt", message);
                    logger.LogInformation(message);
                    break;
                case LogType.Warning:
                    message = DateTime.Now + " - " + "Warning: " + message + Environment.NewLine;
                    System.IO.File.AppendAllText("ErrorLog.txt", message);
                    logger.LogWarning(message);
                    break;
                case LogType.Error:
                    message = DateTime.Now + " - " + "Error: " + message + Environment.NewLine;
                    System.IO.File.AppendAllText("ErrorLog.txt", message);
                    if(throwEx) {
                        if(ex != null) {
                            logger.LogError(ex, message);
                            throw ex;
                        }
                        logger.LogError(message);
                        throw new Exception(message);
                    }
                    if(ex != null) {
                        Console.WriteLine(message);
                        logger.LogError(ex, message);
                        break;
                    }
                    Console.WriteLine(message);
                    logger.LogError(message);
                    break;
            }
        }
    }
    public enum LogType {
        Information,
        Warning,
        Error
    }
}
