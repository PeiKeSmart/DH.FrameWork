namespace LettuceEncrypt.Internal.IO;

internal interface IConsole
{
    bool IsInputRedirected { get; }
    ConsoleColor BackgroundColor { get; set; }
    ConsoleColor ForegroundColor { get; set; }
    bool CursorVisible { get; set; }

    void WriteLine(string line);
    void Write(string line);
    void ResetColor();
    string ReadLine();
}
