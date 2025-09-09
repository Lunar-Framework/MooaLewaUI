using System.Linq;
using Microsoft.CodeAnalysis;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator
{
    [Generator]
    public class MlXamlIncrementalGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var mlxamlFiles = context.AdditionalTextsProvider
                .Where(static file => file.Path.EndsWith(".mlxaml", System.StringComparison.OrdinalIgnoreCase));
            
            var parsedProvider = mlxamlFiles.Select((file, cancellationToken) =>
            {
                var xmlContent = file.GetText(cancellationToken)?.ToString() ?? "";
                var astNodes = MlXamlParser.Parse(xmlContent, out var diagnostics);
                var hintName = $"{System.IO.Path.GetFileNameWithoutExtension(file.Path)}.g.cs";

                return (HintName: hintName, AstNodes: astNodes, Diagnostics: diagnostics);
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

                var syntaxTree = MlXamlCodeGenerator.Generate(parsedData.AstNodes);
                var sourceCode = syntaxTree.GetRoot().ToFullString();

                sourceProductionContext.AddSource(parsedData.HintName, sourceCode);
            });
        }
    }
}