using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DinerClub
{
    /// <summary>
    /// Executable entry point for application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            var log = log4net.LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);
            try
            {
                var commandLineArguments = new CommandLineArgs(args);
                var orderNameService = new OrderNameService();
                var converter = new OrderNumberToOrderConverter(orderNameService);
                var orderValidator = new OrderValidator(commandLineArguments.DayTime);

                var application = new DinerClubApplication(
                    commandLineArguments, 
                    converter,
                    orderValidator);

                var result = application.Run();
                Console.WriteLine(result);
            }
            catch (InvalidCommandLineArgumentException exception)
            {
                log.Error(exception.Message, exception);
                log.InfoFormat("Usage examples: {0}", exception.Examples);
            }
            catch (Exception exception)
            {
                log.Fatal(exception.Message, exception);
                throw;
            }
        }
    }
}
