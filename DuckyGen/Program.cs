using DuckyGenLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace DuckyGen
{
    class Program
    {
        static void Main(string[] args)
        {
            DuckyGenerator gen = new DuckyGenerator();
            gen.Delay = 750;
            gen.AddDelay(1000); //execution delay

            
            //Your Script(s) Here
            ChangeWallpaper("http://memekat.com/media/created/xr5kdq.jpg", gen);
            HideDesktopIcons(gen);

            
            Console.WriteLine("SCRIPT: \r\n\r\n" + gen.GetScript());

            //for me L:\ is directly writing to the DUCKY SdCard
            Process.Start("L:\\");
            Thread.Sleep(1000);
            gen.Compile(@"C:\Users\Anguis\Desktop\DuckyEncoder\encoder.jar", @"L:\\inject.bin", "resources/us.properties");

            Console.WriteLine("Inject.bin is compiled, have fun");
            Console.ReadLine();
        }

        private static void DrawIlluminati(DuckyGenerator gen)
        {
            gen.PressWinKey('r', true);
            gen.WriteString("notepad.exe");
            gen.PressMenu(true);
            gen.WriteStrings(File.ReadAllText(@"D:\Desktop\Files\illuminati.txt").Split(new string[] { "\r\n" }, StringSplitOptions.None), true); //draw illuminati lol
        }

        private static void HideDesktopIcons(DuckyGenerator gen)
        {
            uint oldDelay = gen.Delay;
            gen.Delay = 100; //fast execution
            gen.PressWinKey('d', true);
            gen.PressMenu(true);
            gen.WriteCharacter('v');
            gen.WriteCharacter('d');
            gen.PressWinKey('d', true); //jump back to how it was, sneaky change yay
            gen.Delay = oldDelay;
        }

        private static void ChangeWallpaper(string Path, DuckyGenerator gen)
        {
            //download image
            gen.PressWinKey('r', true);
            gen.WriteString(Path);
            gen.PressEnter(true);
            gen.PressCtrl("s");
            gen.AddDelay(250);
            gen.WriteString(@"%userprofile%\Pictures\uwotmate.jpg", true);
            gen.PressEnter();
            gen.AddDelay(250);

            //if file exists, just overwrite
            gen.PressLeftArrow();
            gen.PressEnter();

            //set wallpaper
            gen.PressWinKey('r', true);
            gen.WriteString(@"%userprofile%\Pictures\uwotmate.jpg", true);
            gen.PressEnter(true);
            gen.PressMenu();
            gen.PressDownArrow();
            gen.PressDownArrow();
            gen.PressEnter();
            gen.PressAlt("F4");
            
        }
    }
}
