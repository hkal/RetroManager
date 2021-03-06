﻿﻿﻿using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RetroManager
{
    public static class RedudantHelper
    {
        public static string DefaultDirectory;

		public static bool DirectoryCheck(string text)
        {

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(@"Directory field can not be empty");
                return false;
            }

            if (!Directory.Exists(text))
            {
                MessageBox.Show(@"Please enter a valid directory.");
                return false;
            } 

            SetDefaultDirectory(text);

            return true;
        }

        public static void SetDefaultDirectory(string text)
		{
            DefaultDirectory = text;
		}

		public static string GetDefaultDirectory()
		{
			return DefaultDirectory;
		}

        public static string Browse(string path)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                var result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                    return folderDialog.SelectedPath;
                
                return path;
            }
        }

        public static void FileMaker(ArrayList array, string filename)
        {
            if (array.Count <= 0) return;

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var duplicatefile = Directory.GetFiles(path, filename + "*.txt");

            if (duplicatefile.Length != 0)
            {
                duplicatefile = duplicatefile.OrderByDescending(c => c).ToArray();
                var num = new string(duplicatefile[0].Where(char.IsDigit).ToArray());

                filename += string.IsNullOrWhiteSpace(num) ? "1" : (Convert.ToInt16(num) + 1) + "";
            }

            filename += ".txt";
            var filepath = path + filename;
            using (var sw = new StreamWriter(filepath))
            {
                foreach (string w in array)
                    sw.WriteLine(w);
            }
            Process.Start(filepath);
        }

		public static bool IsUnixBased
		{
			get
			{
				var p = (int)Environment.OSVersion.Platform;
				return (p == 4) || (p == 6) || (p == 128);
			}
		}
    }
}
