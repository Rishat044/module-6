using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Thread thread1 = new Thread(() =>
        {
            var configManager = ConfigurationManager.GetInstance();
            Console.WriteLine("Thread 1 Instance ID: " + configManager.GetHashCode());

            configManager.LoadSettings("config.txt");
            Console.WriteLine("Thread 1 - AppName: " + configManager.GetSetting("AppName"));

            configManager.SetSetting("AppVersion", "2.0.0");
        });

        Thread thread2 = new Thread(() =>
        {
            var configManager = ConfigurationManager.GetInstance();
            Console.WriteLine("Thread 2 Instance ID: " + configManager.GetHashCode());

            try
            {
                Console.WriteLine("Thread 2 - AppName: " + configManager.GetSetting("AppName"));
                Console.WriteLine("Thread 2 - AppVersion: " + configManager.GetSetting("AppVersion"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Thread 2 Exception: " + ex.Message);
            }
        });

        thread1.Start();
        thread1.Join();

        thread2.Start();
        thread2.Join();

        var configManagerMain = ConfigurationManager.GetInstance();
        configManagerMain.SaveSettings("config_updated.txt");
    }
}
