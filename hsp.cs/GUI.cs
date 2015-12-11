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

        public static string Print(string strings)
        {
            strings = Analyzer.StringUnEscape(strings);
            Program.ClassBody[1] += "g.DrawString(" + strings + ", font, brush, CurrentPositionX, CurrentPositionY);\n" +
                                    "CurrentPositionY += FontSize;\n";
            return "//Print(" + strings + ");";
        }

        public static string Mes(string strings)
        {
            strings = Analyzer.StringUnEscape(strings);
            Program.ClassBody[1] += "g.DrawString(" + strings + ", font, brush, CurrentPositionX, CurrentPositionY);\n" +
                                    "CurrentPositionY += FontSize;\n";
            return "//Mes(" + strings + ");";
        }

        public static string Pos(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (strings.Equals(string.Empty))
            {
                Program.ClassBody[1] += "CurrentPositionX = CurrentPositionX;\n" +
                                        "CurrentPositionY = CurrentPositionY;\n";
                return "//SetCurrentPosition(CurrentPositionX, CurrentPositionY);";
            }
            else if (p.Count() == 2)
            {
                Program.ClassBody[1] += "CurrentPositionX = " + p[0] + ";\n" +
                                        "CurrentPositionY = " + p[1] + ";\n";
                return "//SetCurrentPosition(" + p[0] + ", " + p[1] + ");";
            }
            else
            {
                return "error";
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
            if (!Program.ClassBody[0].Contains("public void Title"))
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

            if (int.Parse(p[0]) > int.Parse(p[2]))
            {
                var temp = p[0];
                p[0] = p[2];
                p[2] = temp;
            }
            if (int.Parse(p[1]) > int.Parse(p[3]))
            {
                var temp = p[1];
                p[1] = p[3];
                p[3] = temp;
            }

            if (p.Count() == 4)
            {
                Program.ClassBody[1] += 
                    "g.FillEllipse(brush, " + p[0] + ", " + p[1] + ", " + p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
            }
            else if (p.Count() == 5)
            {
                if (p[4].Equals("0"))
                {
                    Program.ClassBody[1] +=
                        "g.DrawEllipse(pen, " + p[0] + ", " + p[1] + ", " + p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
                }
                else
                {
                    Program.ClassBody[1] +=
                        "g.FillEllipse(brush, " + p[0] + ", " + p[1] + ", " + p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");";
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

            if (int.Parse(p[0]) > int.Parse(p[2]))
            {
                var temp = p[0];
                p[0] = p[2];
                p[2] = temp;
            }
            if (int.Parse(p[1]) > int.Parse(p[3]))
            {
                var temp = p[1];
                p[1] = p[3];
                p[3] = temp;
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
                if (p[1].Equals(string.Empty))
                {
                    p[1] = "0";
                }
                if (p[2].Equals(string.Empty))
                {
                    p[2] = "Width";
                }
                if (p[3].Equals(string.Empty))
                {
                    p[3] = "Height";
                }

                Program.ClassBody[1] += 
                    "g.FillRectangle(brush, " + p[0] + ", " + p[1] + ", " + p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
                return "//Boxf(" + p[0] + ", " + p[1] + ", " + p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static string Line(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 2)
            {
                Program.ClassBody[1] += "g.DrawLine(pen, CurrentPositionX, CurrentPositionY, " + p[0] + ", " + p[1] + ");\n" +
                                        "CurrentPositionX = " + p[0] + ";\nCurrentPositionY = " + p[1] + ";\n";
                return "//Line(CurrentPositionX, CurrentPositionY, " + p[0] + ", " + p[1] + ");";
            }
            else if (p.Count() == 4)
            {
                Program.ClassBody[1] += "g.DrawLine(pen, " + p[2] + ", " + p[3] + ", " + p[0] + ", " + p[1] + ");\n" +
                                        "CurrentPositionX = " + p[0] + ";\nCurrentPositionY = " + p[1] + ";\n";
                return "//Line(" + p[2] + ", " + p[3] + ", " + p[0] + ", " + p[1] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static string Color(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 3)
            {
                if (p[0].Equals(string.Empty))
                {
                    p[0] = "0";
                }
                if (p[1].Equals(string.Empty))
                {
                    p[1] = "0";
                }
                if (p[2].Equals(string.Empty))
                {
                    p[2] = "0";
                }

                Program.ClassBody[1] += "brush = new SolidBrush(Color.FromArgb(" + p[0] + ", " + p[1] + ", " + p[2] + "));\n" +
                                        "pen = new Pen(Color.FromArgb(" + p[0] + ", " + p[1] + ", " + p[2] + "));\n";
                return "//Color(" + p[0] + ", " + p[1] + ", " + p[2] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static void Ginfo_sizeX(List<string> sentence, int i)
        {
            sentence[i] = "Program.CurrentScreenID.Width.ToString()";
        }

        public static void Ginfo_sizeY(List<string> sentence, int i)
        {
            sentence[i] = "Program.CurrentScreenID.Height.ToString()";
        }
    }
}
