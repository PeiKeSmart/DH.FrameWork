using Microsoft.AspNetCore.Diagnostics;

namespace DG.CoolCat.Core.Mvc.DGCompiler;

public sealed class DHCompilationFailedException : Exception, ICompilationException
{
    public DHCompilationFailedException(
            IEnumerable<CompilationFailure> compilationFailures)
        : base(FormatMessage(compilationFailures))
    {
        if (compilationFailures == null)
        {
            throw new ArgumentNullException(nameof(compilationFailures));
        }

        CompilationFailures = compilationFailures;
    }

    public IEnumerable<CompilationFailure> CompilationFailures { get; }

    private static string FormatMessage(IEnumerable<CompilationFailure> compilationFailures)
    {
        return "One or more compilation failures occurred:" + Environment.NewLine +
            string.Join(
                Environment.NewLine,
                compilationFailures.SelectMany(f => f.Messages!).Select(message => message!.FormattedMessage));
    }
}