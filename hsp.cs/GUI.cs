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

            if (!Program.ClassBody[0].Contains("public void Screen"))
            {
                Program.ClassBody[0] += "public void Screen(Form form, int width, int height)\n{\n" +
                                        "form.Width = width + form.PreferredSize.Width;\n" +
                                        "form.Height = height + form.PreferredSize.Height;\n" +
                                        "form.Size = new Size(form.Width, form.Height);\n}\n\n";
            }

            return "hspWindow.Screen(form" + p[0] + ", " + p[1] + ", " + p[2] + ")";
        }

        public static string Title(string strings)
        {
            if (!Program.ClassBody[0].Contains("public void Screen"))
            {
                Program.ClassBody[0] += "public void Title(Form form, string strings)\n{\n" +
                                        "form.Text = strings;\n}\n";

            }
            
            return "hspWindow.Title(CurrentScreenID, " + strings + ")";
        }

        public static string Circle(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 4)
            {
                Program.ClassBody[1] += "g.FillEllipse(brush, " + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");";
            }
            else if (p.Count() == 5)
            {
                if (p[4].Equals("0"))
                {
                    Program.ClassBody[1] += "g.DrawEllipse(pen, " + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");";
                }
                else
                {
                    Program.ClassBody[1] += "g.FillEllipse(brush, " + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");";
                }
            }
            else
            {
                //
            }

            return "//Circle(" + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");";
        }

        public static string Boxf(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 2)
            {
                Program.ClassBody[1] += "g.FillRectangle(brush, " + p[0] + ", " + p[1] + ", Width, Height);\n";
                return "//Boxf(" + p[0] + ", " + p[1] + ", Width, Height);";
            }
            else if (p.Count() == 4)
            {
                if (p[0].Equals(string.Empty))
                {
                    p[0] = "0";
                }
                else if (p[1].Equals(string.Empty))
                {
                    p[1] = "0";
                }
                
                Program.ClassBody[1] += "g.FillRectangle(brush, " + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");\n";
                return "//Boxf(" + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");";
            }
            else
            {
                return "error";
            }
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
