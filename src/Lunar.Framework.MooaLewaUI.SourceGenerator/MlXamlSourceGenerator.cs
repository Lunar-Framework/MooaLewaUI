using System;
using System.Collections.Generic;
using System.IO;
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
        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.projectdir", out var projectDir);
        
        var mlxamlFiles = (from file in context.AdditionalFiles
            let path = file.Path
            where path.EndsWith(".mlxaml", StringComparison.OrdinalIgnoreCase)
            select file).ToList();
        
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
            
            var namespaceName = GeneratorHelper.GetNamespaceFromFilePath(file.Path, projectDir);

            var syntaxTree = MlXamlCodeGenerator.Generate(className, namespaceName);
            var sourceCode = syntaxTree.GetRoot().ToFullString();

            context.AddSource(hintName, sourceCode);
        }
    }
}