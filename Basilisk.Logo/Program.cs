using System.Text;
using System.Xml;

namespace Basilisk.Logo
{
    public static class Program
    {
        public static int Main()
        {
            XmlWriterSettings settings = new() { Indent = true, IndentChars = "  ", Encoding = Encoding.UTF8 };

            using XmlWriter writer = XmlWriter.Create("basilisk.svg", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            writer.WriteAttributeString("width", "256");
            writer.WriteAttributeString("height", "256");

            string d
                = "M80,40L128,0" // head
                + "L224,32L256,128"// neck
                + "L256,256L128,256"// front
                + "L80,240L64,192"// back
                + "L80,144L128,128"// tail
                + "L0,128"// wing
                + "L0,0"
                + "L128,128"
                + "L64,192" // tail
                + "L128,256"// back
                + "L256,128"// front
                + "L128,0"// head
                + "L128,80"
                + "z";

            writer.WriteStartElement("path");
            writer.WriteAttributeString("d", d);
            writer.WriteAttributeString("stroke", "none");
            writer.WriteAttributeString("fill", "black");
            writer.WriteEndElement();

            writer.WriteEndElement();

            return 0;
        }
    }
}
