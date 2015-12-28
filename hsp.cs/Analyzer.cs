﻿/*===============================
             hsp.cs
  Created by @kkrnt && @ygcuber
===============================*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace hsp.cs
{
    class Analyzer
    {
        /// <summary>
        /// コード中の""で括られた文字列をエスケープ
        /// </summary>
        /// <param name="hspArrayString"></param>
        /// <returns></returns>
        public static string StringEscape(string hspArrayString)
        {
            var hspStringData = hspArrayString;
            while (true)
            {
                var preIndex = hspArrayString.IndexOf("\"", StringComparison.OrdinalIgnoreCase);
                if (preIndex == -1 || hspArrayString[preIndex - 1] == '\\') break;
                var x = hspArrayString.Substring(preIndex + 1);
                var postIndex = x.IndexOf("\"", StringComparison.OrdinalIgnoreCase);
                if (postIndex == -1 || hspArrayString[preIndex + postIndex] == '\\') break;
                var midString = hspArrayString.Substring(preIndex, postIndex + 2);
                Program.StringList.Add(midString);
                hspArrayString = hspArrayString.Replace(midString, "");
                hspStringData = hspStringData.Replace(midString, "＠＋＠" + (Program.StringList.Count - 1) + "＠ー＠");
            }
            return hspStringData;
        }

        /// <summary>
        /// エスケープした文字列を元に戻す
        /// </summary>
        /// <param name="hspArrayString"></param>
        /// <returns></returns>
        public static string StringUnEscape(string hspArrayString)
        {
            var hspStringData = hspArrayString;
            while (true)
            {
                var preStringIndex = hspArrayString.IndexOf("＠＋＠", StringComparison.OrdinalIgnoreCase);
                if (preStringIndex != -1)
                {
                    var postStringIndex = hspArrayString.IndexOf("＠ー＠", StringComparison.OrdinalIgnoreCase);
                    if (postStringIndex != -1)
                    {
                        var o = hspArrayString.Substring(preStringIndex, postStringIndex - preStringIndex + 3);
                        var index = int.Parse(o.Replace("＠＋＠", "").Replace("＠ー＠", ""));
                        hspArrayString = hspArrayString.Replace(o, Program.StringList[index]);
                        hspStringData = hspArrayString;
                    }
                }
                else
                {
                    break;
                }
            }
            return hspStringData;
        } 

        /// <summary>
        /// 関数呼び出し
        /// </summary>
        /// <param name="hspArrayString"></param>
        /// <returns></returns>
        public static string Function(string hspArrayString)
        {
            //要素単位で分解するために半角スペースでスプリット
            var sentence = hspArrayString.Replace("  ", " ").Split(' ').ToList();
            for (var j = 0; j < sentence.Count; j++)
            {
                //余計なものは省く
                //関数は必ず関数名の後に"("が来るはず
                sentence[j] = sentence[j].Trim();
                if (sentence[j] == null ||
                    sentence[j].Equals("\n") ||
                    sentence[j].Equals("") ||
                    !Program.FunctionList.Contains(sentence[j]) ||
                    sentence[j + 1][0] != '(')
                    continue;

                //初めに")"が来る行と, それまでに"("が幾つ出てくるか数える
                var bracketStartCount = 0;
                int k;
                for (k = j + 1; k < sentence.Count; k++)
                {
                    if (sentence[k].Equals("("))
                    {
                        bracketStartCount++;
                    }
                    if (sentence[k].Equals(")"))
                    {
                        break;
                    }
                }

                //"("の数だけ該当する")"をズラす
                for (var l = 0; l < bracketStartCount - 1; l++)
                {
                    var flag = false;
                    for (var m = k + 1; m < sentence.Count; m++)
                    {
                        if (sentence[m].Equals(")"))
                        {
                            k = m;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        /*============================
                        //カッコの数がオカシイのでエラー
                        =============================*/
                        Console.WriteLine("Error");
                    }
                }

                //sentence[j]が関数名
                //sentence[k]が関数の")"
                //sentence[j + 1]～sentence[k]で"("～")"まで
                switch (sentence[j])
                {
                    case "int":
                        HSP.Int(sentence, j);
                        break;
                    case "double":
                        HSP.Double(sentence, j);
                        break;
                    case "str":
                        HSP.Str(sentence, j, k);
                        break;
                    case "abs":
                        HSP.Abs(sentence, j, k);
                        break;
                    case "absf":
                        HSP.Absf(sentence, j, k);
                        break;
                    case "sin":
                        HSP.Sin(sentence, j);
                        break;
                    case "cos":
                        HSP.Cos(sentence, j);
                        break;
                    case "tan":
                        HSP.Tan(sentence, j);
                        break;
                    case "atan":
                        HSP.Atan(sentence, j);
                        break;
                    case "deg2rad":
                        HSP.Deg2rad(sentence, j);
                        break;
                    case "rad2deg":
                        HSP.Rad2deg(sentence, j);
                        break;
                    case "expf":
                        HSP.Expf(sentence, j);
                        break;
                    case "logf":
                        HSP.Logf(sentence, j);
                        break;
                    case "powf":
                        HSP.Powf(sentence, j);
                        break;
                    case "sqrt":
                        HSP.Sqrt(sentence, j);
                        break;
                    case "instr":
                        HSP.Instr(sentence, j, k);
                        break;
                    case "strlen":
                        HSP.Strlen(sentence, j, k);
                        break;
                    case "strmid":
                        HSP.Strmid(sentence, j, k);
                        break;
                    case "strtrim":
                        HSP.Strtrim(sentence, j, k);
                        break;
                    case "limit":
                        HSP.Limit(sentence, j, k);
                        break;
                    case "limitf":
                        HSP.Limitf(sentence, j, k);
                        break;
                    case "length":
                        HSP.Length(sentence, j, k, 1);
                        break;
                    case "length2":
                        HSP.Length(sentence, j, k, 2);
                        break;
                    case "length3":
                        HSP.Length(sentence, j, k, 3);
                        break;
                    case "length4":
                        HSP.Length(sentence, j, k, 4);
                        break;
                    case "gettime":
                        HSP.Gettime(sentence, j);
                        break;
                    case "rnd":
                        HSP.Rnd(sentence, j, k);
                        break;
                }
            }
            //結果を反映
            return string.Join(" ", sentence);
        }

        public static string Macro(string hspArrayString)
        {
            //要素単位で分解するために半角スペースでスプリット
            var sentence = hspArrayString.Replace("  ", " ").Split(' ').ToList();
            for (var i = 0; i < sentence.Count; i++)
            {
                //余計なものは省く
                sentence[i] = sentence[i].Trim();
                if (sentence[i] == null ||
                    sentence[i].Equals("\n") ||
                    sentence[i].Equals(""))
                    continue;
                if (Program.MacroList.Contains(sentence[i]))
                {
                    switch (sentence[i])
                    {
                        case "M_PI":
                            HSP.M_pi(sentence, i);
                            break;
                        case "and":
                            HSP.And(sentence, i);
                            break;
                        case "not":
                            HSP.Not(sentence, i);
                            break;
                        case "or":
                            HSP.Or(sentence, i);
                            break;
                        case "xor":
                            HSP.Xor(sentence, i);
                            break;
                        case "mousex":
                            HSPGUI.Mousex(sentence, i);
                            break;
                        case "mousey":
                            HSPGUI.Mousey(sentence, i);
                            break;
                        case "dir_cur":
                            HSP.Dir_cur(sentence, i);
                            break;
                        case "dir_desktop":
                            HSP.Dir_desktop(sentence, i);
                            break;
                        case "dir_exe":
                            HSP.Dir_exe(sentence, i);
                            break;
                        case "dir_mydoc":
                            HSP.Dir_mydoc(sentence, i);
                            break;
                        case "dir_sys":
                            HSP.Dir_sys(sentence, i);
                            break;
                        case "dir_win":
                            HSP.Dir_win(sentence, i);
                            break;
                        case "ginfo_mx":
                            HSP.Ginfo_mx(sentence, i);
                            break;
                        case "ginfo_my":
                            HSP.Ginfo_my(sentence, i);
                            break;
                        case "ginfo_sizex":
                            HSPGUI.Ginfo_sizeX(sentence, i);
                            break;
                        case "ginfo_sizey":
                            HSPGUI.Ginfo_sizeY(sentence, i);
                            break;
                        case "ginfo_r":
                            HSPGUI.Ginfo_r(sentence, i);
                            break;
                        case "ginfo_g":
                            HSPGUI.Ginfo_g(sentence, i);
                            break;
                        case "ginfo_b":
                            HSPGUI.Ginfo_b(sentence, i);
                            break;
                        case "ginfo_cx":
                            HSPGUI.Ginfo_cx(sentence, i);
                            break;
                        case "ginfo_cy":
                            HSPGUI.Ginfo_cy(sentence, i);
                            break;
                        case "ginfo_dispx":
                            HSPGUI.Ginfo_dispx(sentence, i);
                            break;
                        case "ginfo_dispy":
                            HSPGUI.Ginfo_dispy(sentence, i);
                            break;
                        case "ginfo_wx1":
                            HSPGUI.Ginfo_wx1(sentence, i);
                            break;
                        case "ginfo_wx2":
                            HSPGUI.Ginfo_wx2(sentence, i);
                            break;
                        case "ginfo_wy1":
                            HSPGUI.Ginfo_wy1(sentence, i);
                            break;
                        case "ginfo_wy2":
                            HSPGUI.Ginfo_wy2(sentence, i);
                            break;
                        case "ginfo_sel":
                            HSPGUI.Ginfo_sel(sentence, i);
                            break;
                        case "hwnd":
                            HSPGUI.Hwnd(sentence, i);
                            break;
                        case "__date__":
                            HSPGUI.__date__(sentence, i);
                            break;
                        case "__time__":
                            HSPGUI.__time__(sentence, i);
                            break;
                    }
                }
            }

            //結果を反映
            return string.Join(" ", sentence);
        }
    }
}
