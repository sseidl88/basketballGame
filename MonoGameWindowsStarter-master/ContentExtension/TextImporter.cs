using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using TInput = System.String;

namespace ContentExtension
{
   
        [ContentImporter(".txt", DisplayName = "TextImporter", DefaultProcessor = "TextProcessor")]
        public class TextImporter : ContentImporter<TInput>
        {
            public override TInput Import(string filename, ContentImporterContext context)
            {
                return System.IO.File.ReadAllText(filename);

            }

        }
    
}
