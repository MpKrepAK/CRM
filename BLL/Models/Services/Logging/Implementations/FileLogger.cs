using BLL.Models.Services.Logging.Interfaces;

namespace BLL.Models.Services.Logging.Implementations;

public class FileLogger : ILogger
{
    private static string fileName;

    static FileLogger()
    {
        fileName = new Random().Next().ToString() + " - Log.txt";
        if (!File.Exists(fileName))
        {
            File.Create(fileName);
        }
    }

    public async Task Log(string message)
    {
        TextWriter stream = new StreamWriter(fileName, true);
        try
        {
            await stream.WriteLineAsync("Logged "+message);
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {
            stream.Close();
        }
    }

    public async Task LogError(string message)
    {
        TextWriter stream = new StreamWriter(fileName, true);
        try
        {
            await stream.WriteLineAsync("LoggedError "+message);
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {
            stream.Close();
        }
    }
}