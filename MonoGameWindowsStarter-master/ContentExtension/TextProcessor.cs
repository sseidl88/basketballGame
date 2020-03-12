
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using TInput = System.String;

namespace ContentExtension
{
    [ContentProcessor(DisplayName = "TextProcessor")]
    public class TextProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            try
            {
                string[] parts = input.Split(':');
                string message = parts[1];
                var output = new GameLibrary.Message(message);
                return output;
            }
            catch (Exception ex)
            {
                throw;
            }
           

        }
    }
}
