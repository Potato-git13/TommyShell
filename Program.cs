/***************************/
// COPYRIGHT (c) TOMMY1262 //
/***************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace TommyShell
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            
            Console.WindowWidth = 100;
            Console.WindowHeight = 30;
            Console.WriteLine("WINDOW WIDTH: " + Console.WindowWidth + " WINDOW HEIGHT: " + Console.WindowHeight);
            // TITLE
            Console.Title = "TommyShell";
            // BANNER
            String line;
            StreamReader sr = new StreamReader(".ts_startup");
            line = sr.ReadLine();
            while (line != null)
            {
                Command(line);
                line = sr.ReadLine();
            }
            sr.Close();
            breakln(1);

            // CLI
            var startPath = Directory.GetCurrentDirectory();
            for (; ; )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Environment.UserName +"@" + Environment.MachineName);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(":");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(Directory.GetCurrentDirectory());
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("# ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                var Line = "[" + DateTime.Now + "] " + input;
                File.AppendAllText(startPath + "/log/history.log", Line + Environment.NewLine);
                TextWriter tw = new StreamWriter(startPath + "/log/last_c.log");
                tw.WriteLine("[" + DateTime.Now + "] " + input);
                tw.Close();
                Command(input);
            }
        }
        static void Command(string input)
        {
            if (input == "help")
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("help             Shows list of commands.");
                Console.WriteLine("clear            Clears screen");
                Console.WriteLine("echo             Prints input on screen");
                Console.WriteLine("ls               Lists directory");
                Console.WriteLine("credits          Prints you credits");
                Console.WriteLine("noice            Prints you noice 15x times");   // Noice!
                Console.WriteLine("exit             Kills shell");
                Console.WriteLine("annoyng          Plays annoyng sound for 10 seconds");
                Console.WriteLine("rnd              Types random number from 0 to input");
                Console.WriteLine("mkfile           Makes empty file(type mkfile and your file name)");
                Console.WriteLine("mkdir            Makes empty directory (type mkdir and your folder name)");
                Console.WriteLine("bg blue          Changes bgcolor to blue");
                Console.WriteLine("bg white         Changes bgcolor to white");
                Console.WriteLine("bg red           Changes bgcolor to red");
                Console.WriteLine("bg reset         Resets bgcolor");
                Console.WriteLine("floop            Forever loop");
                Console.WriteLine("md5c             Crypts md5 hash");
                Console.WriteLine("type             Displays you what types in file");
                Console.WriteLine("rem              Removes file");
                Console.WriteLine("remd             Removes directory");
                Console.WriteLine("freq             Plays your beep frequency");
                Console.WriteLine("csong            Plays you christmas song");
                Console.WriteLine("sleep            Sleeps for seconds");
                Console.WriteLine("pause            Pauses until you press key");
                Console.WriteLine("cmd              Launchs cmd");
                Console.WriteLine("datetime         Time and date");
                Console.WriteLine("ping             Pings");
                Console.WriteLine("crash            Converts string into int");
                Console.WriteLine("exec             Executes tommy executable files");
                Console.WriteLine("var2base64       Converts string to base64");
                Console.WriteLine("base642var       Converts base64 to string");
                Console.WriteLine("sort             Sorts numbers");
                Console.WriteLine("cd               Changes directory");
                Console.WriteLine("#                Comment");
            }
            else if (input == "clear")
            {
                Console.Clear();
            }
            else if (input.StartsWith("echo"))
            {
                if (input.Remove(0, 4) == "")
                {
                    Console.WriteLine("ERROR : No input string");
                }
                else if (input.Remove(0, 5) == "")
                {
                    Console.WriteLine("ERROR : No input string");
                }
                else if (input.Remove(0, 5) == "-hw")
                {
                    printf("Hello World!\n");
                }
                else
                {
                    try
                    {
                        Console.WriteLine(input.Remove(0, 5));
                    }
                    catch (Exception ex)
                    {
                        DoNothingString(ex.Message);
                    }
                }
            }
            else if (input == "")
            {

            }
            else if (input == "credits")
            {
                Console.WriteLine("============================");
                Console.WriteLine("Copyright (c) Tommy1262 2018");
                Console.WriteLine("============================");
            }
            else if (input == "noice")
            {
                Console.WriteLine("noice");
                Console.WriteLine("noicenoice");
                Console.WriteLine("noicenoicenoice");
                Console.WriteLine("noicenoicenoicenoice");
                Console.WriteLine("noicenoicenoicenoicenoice");
            }
            else if (input == "exit")
            {
                Console.Beep(4000, 300);
                Console.Beep(3000, 300);
                Console.Beep(2000, 300);
                Console.Beep(1000, 300);
                Environment.Exit(0x9999042);
            }
            else if (input == "annoyng")
            {
                Console.Beep(10000, 9000);
            }
            else if (input.StartsWith("rnd "))
            {
                Random rnd = new Random();
                Console.WriteLine(rnd.Next(0, int.Parse(input.Remove(0, 4))));
            }
            else if (input.StartsWith("mkfile "))
            {
                try
                {
                    var dir = "";
                    var filename = input.Remove(0, 7);
                    string pathString = System.IO.Path.Combine(dir);
                    pathString = System.IO.Path.Combine(pathString, filename);
                    if (!System.IO.File.Exists(pathString))
                    {
                        using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                        {
                            for (byte i = 0; i < 100; i++)
                            {
                                fs.WriteByte(i);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("File \"{0}\" already exists.", filename);
                        return;
                    }
                    try
                    {
                        byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
                        foreach (byte b in readBuffer)

                        {
                            Console.Write(b + " ");
                        }
                        Console.WriteLine();
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.Write("Lines in file: ");
                    var lns = Console.ReadLine();
                    TextWriter tw = new StreamWriter(filename);
                    tw.Write("");
                    tw.Close();
                    for (int i = 0; i < int.Parse(lns); i++)
                    {
                        Console.Write(i + 1 + " ");
                        var ln = Console.ReadLine();
                        File.AppendAllText(filename, ln + Environment.NewLine);
                    }
                }
                catch (Exception ss)
                {
                    DoNothingString(ss.Message);
                }

            }
            else if (input.StartsWith("mkdir "))
            {
                try
                {
                    if (!Directory.Exists(input.Remove(0, 6)))
                    {
                        Directory.CreateDirectory(input.Remove(0, 6));
                    }
                }
                catch (Exception ex)
                {
                    DoNothingString(ex.Message);
                }
            }
            else if (input.StartsWith("md5c ")) // https://www.md5hashgenerator.com/
            {
                try
                {
                    string hash = input.Remove(0, 5).ToUpper();
                    string Pass = "";
                    int Count = 0;
                    bool closeLoop = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    StreamReader file = new StreamReader(@"wordlist.txt");
                    while (closeLoop == true && (Pass = file.ReadLine()) != null)
                    {
                        if (MD5Hash(Pass) == hash)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(Pass);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Cracked hash = " + Pass + "\n\r" + MD5Hash(Pass));
                            Console.ResetColor();
                            closeLoop = false;
                            file.Close();
                        }
                        else
                        {
                            Console.WriteLine(Pass);
                        }
                        Count++;
                        Console.Title = "Current password count: " + Count.ToString();
                        Sleep(10);
                    }
                    file.Close();
                    Console.Title = "TommyShell";
                }
                catch (Exception hhh)
                {
                    DoNothingString(hhh.Message);
                }
            }
            else if (input.StartsWith("type "))
            {
                try
                {
                    String line;
                    StreamReader sr1 = new StreamReader(input.Remove(0, 5));
                    line = sr1.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine(line);
                        line = sr1.ReadLine();
                    }
                    sr1.Close();
                }
                catch (Exception blll)
                {
                    DoNothingString(blll.Message);
                }
            }
            else if (input.StartsWith("rem "))
            {
                try
                {
                    File.Delete(input.Remove(0, 4));
                }
                catch (Exception bau)
                {
                    DoNothingString(bau.Message);
                }
            }
            else if (input.StartsWith("remd "))
            {
                try
                {
                    Directory.Delete(input.Remove(0, 5));
                }
                catch (Exception uau)
                {
                    DoNothingString(uau.Message);
                }
            }
            else if (input.StartsWith("freq "))
            {
                try
                {
                    Console.Beep(int.Parse(input.Remove(0, 5)), 2000);
                }
                catch (Exception blaa)
                {
                    DoNothingString(blaa.Message);
                }
            }
            else if (input == "csong")
            {
                // Note frequencies (from http://pages.mtu.edu/~suits/notefreqs.html)

                int g4 = 392;
                int a4 = 440;
                int h4 = 494;
                int c5 = 523;
                int d5 = 587;
                int e5 = 659;
                int f5 = 698;

                // Basic length
                int ms = 1000;

                Beep2(g4, ms, "We");
                Beep2(c5, ms, "wish");
                Beep2(c5, ms / 2, "you");
                Beep2(d5, ms / 2, "a");
                Beep2(c5, ms / 2, "mer-");
                Beep2(h4, ms / 2, "ry");
                Beep2(a4, ms, "Christ-");
                Beep2(a4, ms, "mas,");
                Beep2(a4, ms, "we");
                Beep2(d5, ms, "wish");
                Beep2(d5, ms / 2, "you");
                Beep2(e5, ms / 2, "a");
                Beep2(d5, ms / 2, "mer-");
                Beep2(c5, ms / 2, "ry");
                Beep2(h4, ms, "Christ-");
                Beep2(g4, ms, "mas,");
                Beep2(g4, ms, "we");
                Beep2(e5, ms, "wish");
                Beep2(e5, ms / 2, "you");
                Beep2(f5, ms / 2, "a");
                Beep2(e5, ms / 2, "mer-");
                Beep2(d5, ms / 2, "ry");
                Beep2(c5, ms, "Christ-");
                Beep2(a4, ms, "mas");
                Beep2(g4, ms / 2, "and");
                Beep2(g4, ms / 2, "a");
                Beep2(a4, ms, "hap-");
                Beep2(d5, ms, "py");
                Beep2(h4, ms, "New");
                Beep2(c5, 2 * ms, "Year!");
            }
            else if (input.StartsWith("sleep "))
            {
                try
                {
                    Sleep(int.Parse(input.Remove(0, 6)));
                }
                catch (Exception abc)
                {
                    DoNothingString(abc.Message);
                }
            }
            else if (input.StartsWith("pause"))
            {
                pause(input.Remove(0, 6));
            }
            else if (input.StartsWith("#"))
            {
                
            }
            else if (input.StartsWith("datetime "))
            {
                Console.WriteLine("[" + DateTime.Now.ToString(input.Remove(0, 9)) + "]");
            }
            else if (input.StartsWith("bg "))
            {
                if (input.Remove(0, 3) == "blue")
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    
                }
                else if (input.Remove(0, 3) == "white")
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    
                }
                else if (input.Remove(0, 3) == "red")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    
                }
                else if (input.Remove(0, 3) == "reset")
                {
                    Console.ResetColor();

                }
                Console.WindowHeight = 30;
                Console.WindowWidth = Console.WindowHeight;
                System.Threading.Thread.Sleep(100);
                Console.WindowWidth = 100;
                Console.WindowHeight = 30;
            }
            else if (input == "cmd")
            {
                System.Diagnostics.Process.Start("C:/Windows/System32/cmd.exe");
            }
            else if (input.StartsWith("ping "))
            {
                Ping p = new Ping();
                PingReply r;
                string s;
                s = input.Remove(0, 5);
                r = p.Send(s);
                for (int i = 0; i < 42; i++)
                {
                    if (r.Status == IPStatus.Success)
                    {
                        Console.WriteLine("Ping to " + s.ToString() + "[" + r.Address.ToString() + "]" + " Successful"
                           + " Response delay = " + r.RoundtripTime.ToString() + " ms" + " | i = " + i + 1);
                    }
                    Thread.Sleep(1000);
                }
            }
            else if (input == "floop")
            {
                for (; ; );
            }

            else if (input.StartsWith("sort "))
            {
                int[] x = new int[] { int.Parse(input.Remove(0, 5)) };
                int temp = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    for (int j = i + 1; j < x.Length; j++)
                    {
                        if (x[i] > x[j])
                        {
                            temp = x[j];
                            x[j] = x[i];
                            x[i] = temp;
                        }
                    }
                    Console.WriteLine(x[i]);
                }
            }
            else if (input == "crash")
            {
                Console.WriteLine(int.Parse("crash"));
            }
            else if (input.StartsWith("exec "))
            {
                String line;
                StreamReader sr2 = new StreamReader(input.Remove(0, 5));
                line = sr2.ReadLine();
                while (line != null)
                {
                    var lineCount = File.ReadAllLines(input.Remove(0, 5)).Length;
                    for (int i = 0; i < lineCount; i++)
                    {
                        byte[] encodedDataAsBytes = System.Convert.FromBase64String(line.Remove(i, 0));
                        string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
                        Command(returnValue);
                        line = sr2.ReadLine();
                    }
                }
                sr2.Close();

            }
            else if (input.StartsWith("var2base64 "))
            {
                string plainText = input.Remove(0, 11);

                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                string encodedText = Convert.ToBase64String(plainTextBytes);
                Console.WriteLine(encodedText);
            }
            else if (input.StartsWith("base642var "))
            {
                string encodedText = input.Remove(0, 11);

                var encodedTextBytes = Convert.FromBase64String(encodedText);

                string plainText = Encoding.UTF8.GetString(encodedTextBytes);
                Console.WriteLine(plainText);
            }
            else if (input.StartsWith("ls"))
            {

                if (input == "ls")
                {
                    string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                    foreach (string dir in dirs)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(dir.Replace(Path.GetDirectoryName(dir) + Path.DirectorySeparatorChar, ""));
                    }
                    foreach (string file in files)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
                else if (input == "ls ")
                {
                    string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                    foreach (string dir in dirs)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(dir.Replace(Path.GetDirectoryName(dir) + Path.DirectorySeparatorChar, ""));
                    }
                    foreach (string file in files)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
                else
                {
                    string[] dirs = Directory.GetDirectories(input.Remove(0, 3));
                    string[] files = Directory.GetFiles(input.Remove(0, 3));
                    foreach (string dir in dirs)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(dir.Replace(Path.GetDirectoryName(dir) + Path.DirectorySeparatorChar, ""));
                    }
                    foreach (string file in files)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
                Console.ResetColor();
            }
            else if (input.StartsWith("cd "))
            {
                Environment.CurrentDirectory = input.Remove(0, 3);
            }
            else if (input.StartsWith("msg -e "))
            {
                MessageBox.Show(input.Remove(0, 7),
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            else if (input.StartsWith("msg -w "))
            {
                MessageBox.Show(input.Remove(0, 7),
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);
            }
            else if (input.StartsWith("msg -i "))
            {
                MessageBox.Show(input.Remove(0, 7),
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1);
            }
            else if (input.StartsWith("msg -q "))
            {
                MessageBox.Show(input.Remove(0, 7),
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            }
            else if (input.StartsWith("msg -n"))
            {
                MessageBox.Show(input.Remove(0, 4));
            }
            else if (input == "msg -h")
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("-e       Error");
                Console.WriteLine("-w       Warning");
                Console.WriteLine("-i       Info");
                Console.WriteLine("-q       Question");
                Console.WriteLine("-n       None");
            }
            



            else
            {
                if (File.Exists(input))
                {
                    Process.Start(input);
                }
                else
                {
                    Console.WriteLine("Command or file '" + input + "' does not exist!");
                    Console.WriteLine("Type help for list of commands.");
                }
            }
        }
        public static string MD5Hash(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider MD5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = MD5Provider.ComputeHash(new UTF8Encoding().GetBytes(inputString));
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        static void Beep2(int fr, int ln, string txt)
        {
            printf("Console.Beep(" + fr + ", " + ln + ");  // " + txt + "\n");
            Console.Beep(fr, ln);
        }

        static void Sleep(int num)
        {
            Thread.Sleep(num);
        }
        static void DoNothingString(string stink)
        {
            int count = 1;
            foreach (char item in stink)
            {
                count++;
                for (int i = 0; i < count; i++) ;
            }
        }
        static void DoNothingInt(int ink)
        {
            for (int i = 0; i < ink; i++) ;
        }
        static void printf(string text)
        {
            Console.Write(text);
        }
        static void pause(string text)
        {
            printf(text);
            Console.ReadKey();
            printf("\n");
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        static void breakln(int aaa)
        {
            for (int i = 0; i < aaa; i++)
            {
                Console.WriteLine(" ");
            }
        }
    }
}
