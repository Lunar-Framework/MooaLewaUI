using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

[Generator]
public class MlXamlIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var mlxamlFiles = context.AdditionalTextsProvider
            .Where(static file => file.Path.EndsWith(".mlxaml", StringComparison.OrdinalIgnoreCase));

        var parsedProvider = mlxamlFiles.Select((file, cancellationToken) =>
        {
            var xmlContent = file.GetText(cancellationToken)?.ToString() ?? "";
            var astNodes = MlXamlParser.Parse(xmlContent, out var diagnostics);
            var className = GeneratorHelper.SanitizeClassName(file.Path);
            var hintName = $"{className}.g.cs";

            return (HintName: hintName, AstNodes: astNodes, Diagnostics: diagnostics, ClassName: className);
        });

        context.RegisterSourceOutput(parsedProvider, (sourceProductionContext, parsedData) =>
        {
            foreach (var diagnostic in parsedData.Diagnostics)
            {
                sourceProductionContext.ReportDiagnostic(diagnostic);
            }

            if (parsedData.Diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
            {
                return;
            }

            var syntaxTree = MlXamlCodeGenerator.Generate(parsedData.AstNodes, parsedData.ClassName);
            var sourceCode = syntaxTree.GetRoot().ToFullString();

            sourceProductionContext.AddSource(parsedData.HintName, sourceCode);
        });
    }
}