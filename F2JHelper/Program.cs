namespace F2JHelper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (args.Length == 1) Init.path = args[0];
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            
        }
    }
}