using System.Diagnostics;

namespace QuickEdit
{
    /// <summary>
    /// Gibt Textzeilen oder eine existierende Textdatei in NotePad.exe aus.
    /// </summary>
    /// <remarks>
    /// 17.11.2023 Erik Nagel: erstellt.
    /// </remarks>
    public class EditorCaller
    {
        /// <summary>
        /// Calls notepad with a given content and/or file path.
        /// </summary>
        /// <param name="path">Rath to the logfile.</param>
        /// <param name="lines">Text content.</param>
        /// <exception cref="ArgumentException">Throws if lines and path are null or empty.</exception>
        public void Edit(string? path, List<string>? lines)
        {
            try
            {
                if (File.Exists(path))
                {
                    if (lines != null)
                    {
                        File.WriteAllLines(path, lines);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        path = getTempLogPath();
                    }
                    if (lines != null)
                    {
                        File.WriteAllLines(path, lines);
                    }
                    else
                    {
                        throw new ArgumentException("No file or content to log.");
                    }
                }
                Process.Start("notepad.exe", path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Statisch, ermittelt den Default für Logfile- Pfad und Namen.
        /// </summary>
        /// <returns></returns>
        public static string getTempLogPath()
        {
            string logFilePathName = Environment.GetEnvironmentVariable("TEMP") ?? "";
            string productName = "";
            string randomPart = "";
            System.Reflection.Assembly? assembly = System.Reflection.Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                productName = assembly.GetName().Name ?? "unknown";
                randomPart = Guid.NewGuid().ToString();
            }
            return Path.Combine(logFilePathName, productName + "." + randomPart + ".log");
        }

    }
}
