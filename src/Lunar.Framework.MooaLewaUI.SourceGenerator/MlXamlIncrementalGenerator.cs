using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

[Generator]
public class MlXamlSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // ISourceGenerator doesn't use this method for our purposes.
        // In more complex scenarios, could register a syntax receiver here.
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var mlxamlFiles = context.AdditionalFiles
            .Where(file => file.Path.EndsWith(".mlxaml", StringComparison.OrdinalIgnoreCase));

        foreach (var file in mlxamlFiles)
        {
            var xmlContent = file.GetText(context.CancellationToken)?.ToString() ?? "";

            var astNodes = MlXamlParser.Parse(xmlContent, out var diagnostics);

            foreach (var diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic);
            }

            if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
            {
                continue;
            }

            var className = GeneratorHelper.SanitizeClassName(file.Path);
            var hintName = $"{className}.g.cs";

            var syntaxTree = MlXamlCodeGenerator.Generate(astNodes, className);
            var sourceCode = syntaxTree.GetRoot().ToFullString();

            context.AddSource(hintName, sourceCode);
        }
    }
}