using NetEti.FileTools;

namespace NetEti.DemoApplications
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EditorCaller editorCaller = new EditorCaller();
            // Forced error:
            List<string>? content = null;
            string? logPath = null;
            editorCaller.Edit(logPath, content);
            // with content but no path
            content = new()
            {
                "first line",
                "second line",
                "third and last line"
            };
            editorCaller.Edit(logPath, content);
            // with path but no content
            editorCaller.Edit("Demo.txt", null);
            // with path and content
            editorCaller.Edit(Path.Combine(Environment.GetEnvironmentVariable("TEMP") ?? "", "Demo.log"), content);
        }
    }
}
