using System;
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
}