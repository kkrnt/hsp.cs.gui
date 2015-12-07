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
    }
}
