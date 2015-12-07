# hsp.cs
HSPのコードをC#に変換し, 実行します  

## LICENSE
[The MIT License](https://github.com/kkrnt/hsp.cs/blob/master/LICENSE)

## Download
[v0.1.4](https://github.com/kkrnt/hsp.cs/releases/tag/v0.1.4)  
[v0.1.3](https://github.com/kkrnt/hsp.cs/releases/tag/v0.1.3)  
[v0.1.2](https://github.com/kkrnt/hsp.cs/releases/tag/v0.1.2)  
[v0.1.1](https://github.com/kkrnt/hsp.cs/releases/tag/v0.1.1)  
[v0.1.0](https://github.com/kkrnt/hsp.cs/releases/tag/v0.1.0)  

## Usage
```
hsp.cs [.hsp] [option]
Option:
     -o  --output <file>       output executive file(.exe)
     -c  --cs     <file>       output csharp source file(.cs)
Example:
     hsp.cs.exe sample.hsp
     hsp.cs.exe sample.hsp -o sample.exe -c sample.cs
```

## Basic Grammar
- if
- else
- for
- next
- while
- wend
- repeat
- loop
- switch
- swend
- swbreak
- case
- default
- _break
- _continue
- goto
- gosub

## Function
- int
- double
- str
- abs
- absf
- sin
- cos
- tan
- atan
- expf
- logf
- powf
- sqrt
- instr
- strlen
- limit
- limitf
- length
- length2
- length3
- length4
- gettime
- deg2rad
- rad2deg
- strmid
- strtrim
- rnd

## Command
- print
- mes
- exist
- delete
- mkdir
- split
- bcopy
- strrep
- dim
- ddim
- chdir
- ginfo_mx
- ginfo_my
- end
- stop

## Macro
- M_PI
- and
- not
- or
- xor
- dir_cur

## Example
<img src="http://o8o.jp/hsp.cs.png" width="40%">  

```
hsp.cs.exe sample.hsp

<HSP Code>
a="123"
b="456"
hoge=str (int (a+double (b)))
print hoge
c = 0
if c==0 : print "c=0" : print "ok"
d = 1
if (d = 0) && (c!=0) && (c=1) {
        print "no"
}else{
        print "yes"
}
for i,0,3,1
        print i
next
e = 3
while e>0
        print e
        e--
wend
repeat 5
        print cnt
loop
f = 1
switch f
case 0
        print "f=0"
        swbreak
case 1
        print "f=1"
case 2
        print "f=2"
default
        print "f!=0"
        swbreak
swend

print "start"
goto *one

*two
        print "two"
        goto *three

*one
        print "one"
        goto *two

*endl
        print "end"
        return

*three
        print "three"
        gosub *endl
        print "gosub ok"
        stop

========================

<C# Code>
using System;
using System.Collections.Generic;
public class Program
{
public static void Main()
{
int cnt = 0;
var label_bf4ae00a07014dd491434044a809f636 = new List<string>();
dynamic a = "123";
dynamic b = "456";
dynamic hoge =  ( int.Parse ( a + double.Parse ( b ) ) ).ToString();
Console.WriteLine(hoge);
dynamic c = 0;
if(c == 0)
{
Console.WriteLine("c=0");
Console.WriteLine("ok");
};
dynamic d = 1;
if((d==0)&&(c != 0)&&(c==1))
{
Console.WriteLine("no");
}else{
Console.WriteLine("yes");
}
for (var i = 0; i != 3; i += 1 )
{
Console.WriteLine(i);
}
dynamic e = 3;
while (e>0)
{
Console.WriteLine(e);
e --;
}
for (cnt=0; cnt<5; cnt++)
{
Console.WriteLine(cnt);
}
dynamic f = 1;
string switchTmpString_e3403cfa78c14044a52e1ed1f84e1aa2 = f.ToString();
switch (switchTmpString_e3403cfa78c14044a52e1ed1f84e1aa2)
{
case "0":
Console.WriteLine("f=0");
break;
case "1":
Console.WriteLine("f=1");
goto case "2";
case "2":
Console.WriteLine("f=2");Console.WriteLine("f!=0");
break;
default:
Console.WriteLine("f!=0");
break;
}

Console.WriteLine("start");
goto  one;

two:
Console.WriteLine("two");
goto  three;

one:
Console.WriteLine("one");
goto  two;

endl:
Console.WriteLine("end");
goto label_bf4ae00a07014dd491434044a809f636;

three:
Console.WriteLine("three");
label_bf4ae00a07014dd491434044a809f636.Clear();
label_bf4ae00a07014dd491434044a809f636.Add("returnLabel_ef8db3b5707d4e23ae0ea1f630751a95");
goto endl;
returnLabel_ef8db3b5707d4e23ae0ea1f630751a95:
Console.WriteLine("gosub ok");
return;
label_bf4ae00a07014dd491434044a809f636:
switch(label_bf4ae00a07014dd491434044a809f636[0])
{
case "two":
goto two;
case "one":
goto one;
case "endl":
goto endl;
case "three":
goto three;
case "returnLabel_ef8db3b5707d4e23ae0ea1f630751a95":
goto returnLabel_ef8db3b5707d4e23ae0ea1f630751a95;
}
}
}
========================

構文エラーなし

========================

意味エラーなし

========================

<実行結果>
123456
c=0
ok
yes
0
1
2
3
2
1
0
1
2
3
4
f=1
f=2
f!=0
start
one
two
three
end
gosub ok

========================
```