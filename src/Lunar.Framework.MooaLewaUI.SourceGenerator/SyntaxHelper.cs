using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

public static class SyntaxHelper
{
    public static MemberAccessExpressionSyntax QualifiedMember(string typeName, string memberName)
    {
        return MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            IdentifierName(typeName),
            IdentifierName(memberName)
        );
    }
}