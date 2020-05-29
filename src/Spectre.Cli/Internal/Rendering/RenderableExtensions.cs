using System.Linq;

namespace Spectre.Cli.Internal.Rendering
{
    internal static class RenderableExtensions
    {
        public static bool StartsWithLineBreak(this IRenderable renderable)
        {
            if (renderable != null)
            {
                if (renderable is LineBreakElement)
                {
                    return true;
                }

                if (renderable is BlockElement block)
                {
                    return block.Elements.FirstOrDefault()?.StartsWithLineBreak() ?? false;
                }
            }

            return false;
        }
    }
}
