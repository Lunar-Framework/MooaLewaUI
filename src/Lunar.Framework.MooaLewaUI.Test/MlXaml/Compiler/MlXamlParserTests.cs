using System.Diagnostics;
using JetBrains.Annotations;
using Lunar.Framework.MooaLewaUI.MlXaml.Compiler;

namespace Lunar.Framework.MooaLewaUI.Test.MlXaml.Compiler;

[TestSubject(typeof(MlXamlParser))]
public class MlXamlParserTests
{
    [Fact]
    public void Parse_TextBlockAndSprite_ReturnsNodes()
    {
        var xml = @"
            <Root>
                <TextBlock Text='Hello' Font='Arial'/>
                <Sprite Source='player.png' X='100' Y='200'/>
            </Root>";

        var nodes = MlXamlParser.Parse(xml);

        Assert.Equal(2, nodes.Count);
        Assert.IsType<TextBlockNode>(nodes[0]);
        Assert.IsType<SpriteNode>(nodes[1]);
        
        var textBlockNode = nodes[0] as TextBlockNode;
        var spriteNode = nodes[1] as SpriteNode;
        Debug.Assert(textBlockNode != null, nameof(textBlockNode) + " != null");
        Debug.Assert(spriteNode != null, nameof(spriteNode) + " != null");

        Assert.Equal("Hello", textBlockNode.Text);
        Assert.Equal("Arial", textBlockNode.Font);

        Assert.Equal("player.png", spriteNode.Source);
        Assert.Equal(100, spriteNode.X);
        Assert.Equal(200, spriteNode.Y);
    }
}