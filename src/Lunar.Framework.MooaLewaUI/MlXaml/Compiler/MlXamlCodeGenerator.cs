using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Lunar.Framework.MooaLewaUI.MlXaml.Compiler;

public class MlXamlCodeGenerator
{
    public static SyntaxTree Generate(List<IMlXamlNode> astNodes)
    {
        // 1. 创建 UI Root 变量
        var rootVariable = LocalDeclarationStatement(
            VariableDeclaration(IdentifierName("var"))
                .AddVariables(
                    VariableDeclarator("root")
                        .WithInitializer(EqualsValueClause(ObjectCreationExpression(IdentifierName("UIRoot"))))
                )
        );

        // 2. 遍历 AST 节点，生成添加子节点的代码
        var statements = new List<StatementSyntax> { rootVariable };
        foreach (var node in astNodes)
        {
            statements.Add(GenerateNodeAddition(node));
        }

        // 3. 将所有语句包装在一个方法中
        var methodDeclaration = MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier("InitializeUI")
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword))
            .WithBody(Block(statements));

        // 4. 将方法包装在一个类中
        var classDeclaration = ClassDeclaration("GeneratedUI")
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddMembers(methodDeclaration);

        // 5. 创建一个命名空间单元
        var namespaceDeclaration = NamespaceDeclaration(IdentifierName("GeneratedNamespace"))
            .AddMembers(classDeclaration);

        // 6. 返回完整的语法树
        var compilationUnit = CompilationUnit()
            .AddUsings(UsingDirective(IdentifierName("System")))
            .AddMembers(namespaceDeclaration);

        return CSharpSyntaxTree.Create(compilationUnit.NormalizeWhitespace());
    }

    private static StatementSyntax GenerateNodeAddition(IMlXamlNode node)
    {
        // 这里是核心：根据具体的节点类型调用不同的生成方法
        switch (node)
        {
            case TextBlockNode textBlockNode:
                return GenerateTextBlockCode(textBlockNode);
            case SpriteNode spriteNode:
                return GenerateSpriteCode(spriteNode);
            default:
                throw new NotSupportedException($"Unsupported node type: {node.GetType().Name}");
        }
    }

    // 这里是每个特定节点的生成逻辑
    private static StatementSyntax GenerateTextBlockCode(TextBlockNode node)
    {
        return ExpressionStatement(
            InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("root.Children"),
                    IdentifierName("Add")
                )
            ).AddArgumentListArguments(
                Argument(
                    ObjectCreationExpression(IdentifierName("TextBlock"))
                        .WithInitializer(
                            InitializerExpression(
                                SyntaxKind.ObjectInitializerExpression,
                                SeparatedList<ExpressionSyntax>(
                                    new[]
                                    {
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("Text"),
                                            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(node.Text))
                                        ),
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("Font"),
                                            InvocationExpression(IdentifierName("LoadFont")).AddArgumentListArguments(
                                                Argument(LiteralExpression(SyntaxKind.StringLiteralExpression,
                                                    Literal(node.Font)))
                                            )
                                        )
                                    }
                                )
                            )
                        )
                )
            )
        );
    }

    private static StatementSyntax GenerateSpriteCode(SpriteNode node)
    {
        return ExpressionStatement(
            InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("root.Children"),
                    IdentifierName("Add")
                )
            ).AddArgumentListArguments(
                Argument(
                    ObjectCreationExpression(IdentifierName("Sprite"))
                        .WithInitializer(
                            InitializerExpression(
                                SyntaxKind.ObjectInitializerExpression,
                                SeparatedList<ExpressionSyntax>(
                                    new[]
                                    {
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("Source"),
                                            InvocationExpression(IdentifierName("LoadTexture"))
                                                .AddArgumentListArguments(
                                                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression,
                                                        Literal(node.Source)))
                                                )
                                        ),
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("X"),
                                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(node.X))
                                        ),
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("Y"),
                                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(node.Y))
                                        )
                                    }
                                )
                            )
                        )
                )
            )
        );
    }
}