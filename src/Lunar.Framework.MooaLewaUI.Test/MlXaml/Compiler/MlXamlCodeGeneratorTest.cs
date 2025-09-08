using JetBrains.Annotations;
using Lunar.Framework.MooaLewaUI.MlXaml.Compiler;
using Microsoft.CodeAnalysis;
using Xunit.Abstractions;

namespace Lunar.Framework.MooaLewaUI.Test.MlXaml.Compiler;

[TestSubject(typeof(MlXamlCodeGenerator))]
public class MlXamlCodeGeneratorTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Generate_WithTextBlockAndSprite_ProducesExpectedCode()
    {
        // Arrange: 构造 AST
        var astNodes = new List<IMlXamlNode>
        {
            new TextBlockNode { Text = "Hello", Font = "Arial" },
            new SpriteNode { Source = "player.png", X = 100, Y = 200 }
        };

        // Act: 生成代码
        var tree = MlXamlCodeGenerator.Generate(astNodes);
        var code = tree.GetRoot().NormalizeWhitespace().ToFullString();

        // Assert: 验证生成结果包含关键片段
        testOutputHelper.WriteLine(code);
        
        Assert.Contains("public static void InitializeUI()", code);
        Assert.Contains("new TextBlock", code);
        Assert.Contains("Text = \"Hello\"", code);
        Assert.Contains("Font = LoadFont(\"Arial\")", code);

        Assert.Contains("new Sprite", code);
        Assert.Contains("Source = LoadTexture(\"player.png\")", code);
        Assert.Contains("X = 100", code);
        Assert.Contains("Y = 200", code);
    }
}