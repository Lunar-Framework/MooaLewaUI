using JetBrains.Annotations;
using Lunar.Framework.MooaLewaUI.SourceGenerator;
using Microsoft.CodeAnalysis;
using Xunit.Abstractions;

namespace Lunar.Framework.MooaLewaUI.Test;

[TestSubject(typeof(MlXamlCodeGenerator))]
public class MlXamlCodeGeneratorTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Generate_WithTextBlockAndSprite_ProducesExpectedCode()
    {
        // Arrange: Construct AST
        var astNodes = new List<IMlXamlNode>
        {
            new TextBlockNode { Text = "Hello", Font = "Arial" },
            new SpriteNode { Source = "player.png", X = 100, Y = 200 }
        };

        // Act: Generate code
        var tree = MlXamlCodeGenerator.Generate(astNodes, "GeneratedUI", "");
        var code = tree.GetRoot().NormalizeWhitespace().ToFullString();

        // Assert: Verify that the generated result contains key fragments
        testOutputHelper.WriteLine(code);

        Assert.Contains("public partial class", code);
        Assert.Contains("public void InitializeUI()", code);

        Assert.Contains("new TextBlock", code);
        Assert.Contains("new Sprite", code);
        Assert.Contains("X = 100", code);
        Assert.Contains("Y = 200", code);
    }
}