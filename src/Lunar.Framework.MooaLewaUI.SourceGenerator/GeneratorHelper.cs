using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

public static class GeneratorHelper
{
    public static MemberAccessExpressionSyntax QualifiedMember(string typeName, string memberName)
    {
        return MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            IdentifierName(typeName),
            IdentifierName(memberName)
        );
    }

    public static string SanitizeClassName(string fileName)
    {
        var name = Path.GetFileNameWithoutExtension(fileName);

        name = Regex.Replace(name, @"[^a-zA-Z0-9_]", "_");

        if (!SyntaxFacts.IsValidIdentifier(name))
        {
            name = "_" + name;
        }

        if (SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None)
        {
            name = "_" + name;
        }

        return name;
    }

    public static string GetNamespaceFromFilePath(string filePath, string? projectRootPath)
    {
        if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(projectRootPath))
        {
            return string.Empty;
        }

        var normalizedFilePath = filePath.Replace('\\', '/');
        var normalizedProjectRootPath = projectRootPath!.Replace('\\', '/');

        if (normalizedProjectRootPath.EndsWith("/"))
        {
            normalizedProjectRootPath = normalizedProjectRootPath.TrimEnd('/');
        }

        var baseNamespace = Path.GetFileName(normalizedProjectRootPath)?.Replace(" ", "");

        if (string.IsNullOrEmpty(baseNamespace))
        {
            return string.Empty;
        }

        normalizedProjectRootPath = normalizedProjectRootPath.Replace(baseNamespace, "");
        var relativePath = normalizedFilePath.Replace(normalizedProjectRootPath, "").TrimStart('/');

        var directory = Path.GetDirectoryName(relativePath);

        if (string.IsNullOrEmpty(directory))
        {
            return !string.IsNullOrEmpty(baseNamespace) ? baseNamespace! : "";
        }

        var finalNamespace = directory.Replace(normalizedProjectRootPath, "").TrimEnd('/').Replace('/', '.');

        return finalNamespace;
    }
}