using JetBrains.Annotations;
using Lunar.Framework.MooaLewaUI.MlXaml.Compiler;
using Microsoft.CodeAnalysis;
using Xunit.Abstractions;

namespace Lunar.Framework.MooaLewaUI.Test.MlXaml.Compiler;

[TestSubject(typeof(MlXamlParser))]
[TestSubject(typeof(MlXamlCodeGenerator))]
public class EndToEndMlXamlTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ParseAndGenerate_CombinedTest_ProducesExpectedCode()
    {
        // 1. Arrange: Define the input XML string
        var xml = @"
            <Root>
                <TextBlock Text='Hello' Font='Arial'/>
                <Sprite Source='player.png' X='100' Y='200'/>
            </Root>";

        // 2. Act: Parse the XML string into AST nodes
        var astNodes = MlXamlParser.Parse(xml);

        // 3. Act: Generate code from the AST nodes
        var tree = MlXamlCodeGenerator.Generate(astNodes);
        var code = tree.GetRoot().NormalizeWhitespace().ToFullString();

        // 4. Assert: Validate the generated code
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