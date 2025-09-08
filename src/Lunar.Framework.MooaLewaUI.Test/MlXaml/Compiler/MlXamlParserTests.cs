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
                <TextBlock>hello</TextBlock>
                <Sprite Source='player.png' X='100' Y='200'/>
            </Root>";

        var nodes = MlXamlParser.Parse(xml);

        Assert.Equal(2, nodes.Count);
        Assert.IsType<TextBlockNode>(nodes[0]);
        Assert.IsType<SpriteNode>(nodes[1]);
    }
}