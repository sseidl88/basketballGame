using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = GameLibrary.Message;

namespace ContentExtension
{
    [ContentTypeWriter]
    public class TextWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.text);
        }

        //public override string GetRuntimeType(TargetPlatform targetPlatform)
        //{
        //    return typeof(text).AssemblyQualifiedName;
        //}

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "GameLibrary.MessageReader, TextLibrary";
        }
    }
}
