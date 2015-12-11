/*===============================
             hsp.cs
  Created by @kkrnt && @ygcuber
===============================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace hsp.cs
{
    public partial class Program
    {
        //基本文法
        public static List<string> BasicList = new List<string>()
        {
            "if",
            "else",
            "while",
            "wend",
            "for",
            "next",
            "_break",
            "_continue",
            "repeat",
            "loop",
            "switch",
            "swend",
            "swbreak",
            "case",
            "default",
            "goto",
            "gosub",
            "return"
        };

        //文字列を格納するリスト
        public static List<string> StringList = new List<string>();

        //関数リスト
        public static readonly List<string> FunctionList = new List<string>()
        {
            "int",
            "double",
            "str",
            "abs",
            "absf",
            "sin",
            "cos",
            "tan",
            "atan",
            "deg2rad",
            "rad2deg",
            "expf",
            "logf",
            "powf",
            "sqrt",
            "instr",
            "strlen",
            "strmid",
            "strtrim",
            "limit",
            "limitf",
            "length",
            "length2",
            "length3",
            "length4",
            "gettime",
            "rnd"
        };

        //コマンドリスト
        public static readonly List<string> CommandList = new List<string>()
        {
            "print",
            "mes",
            "exist",
            "delete",
            "bcopy",
            "mkdir",
            "chdir",
            "split",
            "strrep",
            "dim",
            "ddim",
            "end",
            "stop",
            "pos",
            "screen",
            "title",
            "circle",
            "boxf",
            "line",
            "color"
        };

        //変数リスト
        public static List<string> VariableList = new List<string>()
        {
            "strsize",
            "stat",
            "cnt"
        };

        //配列変数リスト
        public static List<string> ArrayVariableList = new List<string>();

        //マクロリスト
        public static List<string> MacroList = new List<string>()
        {
            "M_PI",
            "and",
            "not",
            "or",
            "xor",
            "dir_cur",
            "ginfo_mx",
            "ginfo_my",
            "ginfo_sizeX",
            "ginfo_sizeY"
        };

        //using
        public static string Using = "using System;\nusing System.Drawing;\nusing System.Windows.Forms;\n";
        //class
        private const string Header = "public class Program\n{\n";
        //field
        private static string ProgramField = "public static Form form0 = new Form();\n" +
                                        "public static Form CurrentScreenID = form0;\n";
        //Main関数以外の関数の定義
        public static string SubFunction = "";
        //Main関数の定義
        private const string MainFunction = "public static void Main()\n{\n" +
            "HSPWindow hspWindow = new HSPWindow();\n" +
            "HSPPaintEvent hspPaintEvent = new HSPPaintEvent();\n" +
            "HSPKeyDownEvent hspKeyDownEvent = new HSPKeyDownEvent();\n" +
            "HSPTickEvent hspTickEvent = new HSPTickEvent();\n" +
            "hspWindow.InitScreen(form0, hspPaintEvent, hspKeyDownEvent, hspTickEvent);\n";
        //ウィンドウを動かすためのコードの追加
        private const string AddMainFunction = "Application.Run(form0);\n";
        //システム変数宣言
        public static string VariableDefinition = "";
        //footer
        public static string Footer = "}\n}\n";
        //自作クラスの定義
        public static List<string> ClassHeader = new List<string>()
        {
            "class HSPWindow\n{\n",

            "class HSPPaintEvent\n{\n",

            "class HSPKeyDownEvent\n{\n",

            "class HSPTickEvent\n{\n"
        };

        public static List<string> ClassBody = new List<string>()
        {
            "public void InitScreen(Form form, HSPPaintEvent hspPaintEvent, " +
            "HSPKeyDownEvent hspKeyDownEvent, HSPTickEvent hspTickEvent)\n{\n" +
            "form.Width = 640 + form.PreferredSize.Width;\n" +
            "form.Height = 480 + form.PreferredSize.Height;\n" +
            "form.Size = new Size(form.Width, form.Height);\n" +
            "form.Text = \"hsp.cs\";\n" +
            "form.BackColor = Color.FromArgb(255, 255, 255);\n" +
            "form.MaximizeBox = false;\n" +
            "form.FormBorderStyle = FormBorderStyle.FixedSingle;\n" +
            "form.Paint += hspPaintEvent.paint;\n" +
            "form.KeyDown += hspKeyDownEvent.keyDown;\n" +
            "Timer timer = new Timer();\n"+
            "timer.Interval = 15;\n" +
            "timer.Tick += hspTickEvent.tick;\n" +
            "timer.Start();\n}\n\n",

            "public void paint(object sender, PaintEventArgs e)\n{\n" +
            "var FontSize = 14;\n"+
            "var CurrentPositionX = 0;\n" +
            "var CurrentPositionY = 0;\n" +
            "Graphics g = e.Graphics;\n" +
            "Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0));\n" +
            "Pen pen = new Pen(Color.FromArgb(0, 0, 0));\n" +
            "Font font = new Font(\"MS Pゴシック\", FontSize);\n",

            "public void keyDown(object sender, KeyEventArgs e)\n{\n",

            "public void tick(object sender, EventArgs e)\n{\n"
        };
        
        public static List<string> ClassFooter = new List<string>()
        {
            "\n}\n\n", "}\n}\n\n", "}\n}\n\n", "Program.CurrentScreenID.Invalidate();\n}\n}\n\n"
        };

        //if文の末尾に"}"を付けるためのフラグ
        private static List<int> ifFlag = new List<int>();

        //コメントをエスケープするためのフラグ
        public static bool commentFlag = false;

        //switch文の中にいるかどうか
        private static bool switchFlag = false;
        //switch文の行数を入れるためのリスト
        private static List<int> switchList = new List<int>();
        //1つ目のcase文
        private static bool firstCase = true;

        //変数名の先頭として存在してはいけない文字
        private static List<char> VariableNameRule =
            "0123456789!\"#$%&'()-^\\=~|@[`{;:]+*},./<>?".ToCharArray().ToList();

        private static List<string> ReturnLabelList = new List<string>(); 

        public static List<Form> Window = new List<Form>(); 

        /// <summary>
        /// ローカル変数名を作成する関数
        /// GUIDを生成し, 変数名の末尾に追加する
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static string __LocalName(string variableName)
        {
            return variableName + "_" + Guid.NewGuid().ToString("N");
        }
    }
}
