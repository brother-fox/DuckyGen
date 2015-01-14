using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DuckyGenLib
{
    public class DuckyGenerator
    {
        private StringBuilder SB;
        public uint Delay { get; set; }

        public DuckyGenerator()
        {
            SB = new StringBuilder();
        }

        public void WriteCharacter(char Char, bool AddDelay = false)
        {
            SB.AppendLine("STRING " + Char);

            if (AddDelay)
                this.AddDelay(Delay);
        }

        public void WriteString(string Value, bool AddDelay = false)
        {
            if (String.IsNullOrEmpty(Value))
                throw new ArgumentNullException("Value)");
            SB.AppendLine("STRING " + Value);

            if (AddDelay)
                this.AddDelay(Delay);
        }

        /// <summary>
        /// Write multiple strings
        /// </summary>
        /// <param name="Values">The strings</param>
        /// <param name="AddEnter">Add a enter after each string</param>
        /// <param name="AddDelay">Add a delay after each string</param>
        public void WriteStrings(string[] Values, bool AddEnter, bool AddDelay = false)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                WriteString(Values[i], AddDelay);

                if (AddEnter)
                {
                    PressEnter(false);
                }
            }
        }

        public void PressEnter(bool AddDelay = false)
        {
            SB.AppendLine("ENTER");

            if (AddDelay)
                this.AddDelay(this.Delay);
        }

        public void PressESC(bool AddDelay = false)
        {
            SB.AppendLine("ESC");

            if (AddDelay)
                this.AddDelay(this.Delay);
        }

        public void PressMenu(bool AddDelay = false)
        {
            SB.AppendLine("MENU");

            if (AddDelay)
                this.AddDelay(this.Delay);
        }

        /// <summary>
        /// Press ALT + X at the same time for combined key presses
        /// </summary>
        /// <param name="Key"></param>
        public void PressAlt(string Key, bool AddDelay = false)
        {
            SB.AppendLine("ALT " + Key);

            if (AddDelay)
                this.AddDelay(Delay);
        }

        /// <summary>
        /// Press CONTROL + X at the same time for combined key presses
        /// </summary>
        /// <param name="Key"></param>
        public void PressCtrl(string Key, bool AddDelay = false)
        {
            SB.AppendLine("CONTROL " + Key);

            if (AddDelay)
                this.AddDelay(Delay);
        }

        /// <summary>
        /// Press SHIFT + X at the same time for combined key presses
        /// </summary>
        /// <param name="Key"></param>
        public void PressShift(string Key, bool AddDelay = false)
        {
            SB.AppendLine("SHIFT " + Key);

            if (AddDelay)
                this.AddDelay(Delay);
        }

        /// <summary>
        /// Press WINDOWS_KEY + X at the same time for combined key presses, Also known as GUI
        /// </summary>
        /// <param name="Key"></param>
        public void PressWinKey(char Key, bool AddDelay = false)
        {
            SB.AppendLine("GUI " + Key);

            if (AddDelay)
                this.AddDelay(Delay);
        }

        public void PressDownArrow(bool AddDelay = false)
        {
            SB.AppendLine("DOWNARROW");

            if (AddDelay)
                this.AddDelay(Delay);
        }

        public void PressUpArrow(bool AddDelay = false)
        {
            SB.AppendLine("UPARROW");

            if (AddDelay)
                this.AddDelay(Delay);
        }

        public void PressLeftArrow(bool AddDelay = false)
        {
            SB.AppendLine("LEFTARROW");

            if (AddDelay)
                this.AddDelay(Delay);
        }

        public void PressRightArrow(bool AddDelay = false)
        {
            SB.AppendLine("RIGHTARROW");

            if (AddDelay)
                this.AddDelay(Delay);
        }

        public void AddDelay(uint Delay)
        {
            SB.AppendLine("DELAY " + Delay);
        }

        /// <summary>
        /// Compile the ducky script with the given Encoder
        /// </summary>
        /// <param name="DuckEncoderPath">The path to the encoder</param>
        public void Compile(string DuckEncoderPath, string Output, string KeyboardLayout)
        {
            string Path = Environment.CurrentDirectory + "\\" + DateTime.Now.Ticks + ".txt";
            File.WriteAllText(Path, GetScript());
            FileInfo encoderFI = new FileInfo(DuckEncoderPath);

            using (Process proc = new Process())
            {

                proc.StartInfo = new ProcessStartInfo("java", "-jar " + encoderFI.Name + " -i \"" + Path + "\" -o \"" + Output + "\" -l " + KeyboardLayout);
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = encoderFI.DirectoryName;

                if (!proc.Start())
                {

                }

                proc.WaitForExit();
            }
        }

        public string GetScript()
        {
            return SB.ToString();
        }
    }
}