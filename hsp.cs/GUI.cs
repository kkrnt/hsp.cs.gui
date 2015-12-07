using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hsp.cs
{
    public class HSPGUI : Form
    {
        public HSPGUI()
        {
            //
        }

        static void Screen(int id)
        {
            if (Program.Window.Count > id)
            {
                //Activate
            }
            else
            {
                Program.Window.Add(new Form());
            }
        }

        public static string Screen(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            //Program.Window.Add();

            Program.Footer += "class Window" + p[0] + "\n{\n" +
                "public static void screen" + p[0] + "() {\n" +
                "Form form" + p[0] + " = new Form();\n" +
                "form" + p[0] + ".Size = new Size(form" + p[0] + ".Width + form" + p[0] + ".PreferredSize.Width, " +
                "form" + p[0] + ".Height + form" + p[0] + ".PreferredSize.Height);\n" +
                "form" + p[0] + ".Text = \"hsp.cs\";\n" +
                "form" + p[0] + ".BackColor = Color.FromArgb(255, 255, 255);\n" +
                "form" + p[0] + ".MaximizeBox = false;\n" +
                "Application.Run(form" + p[0] + ");\n}\n}";

            return "Window" + p[0] +".screen" + p[0] + "();";
        }

        public static void Ginfo_sizeX(List<string> sentence, int i)
        {
            sentence[i] = "Width";
        }

        public static void Ginfo_sizeY(List<string> sentence, int i)
        {
            sentence[i] = "Height";
        }
    }
}
