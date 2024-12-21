using System;
using System.IO;

namespace CutCopy
{
    //!!! Dont Forget;
    //We can also use "Cut Method" from Directory and File Class, if you work same disk drive
    //You can cut files just in same Disk Drive, that's why we use this way.
    //Firstly we copy to files, later we delete "resources files"


    internal class Program
	{
		static void Main(string[] args)
		{
			CopyDirectory();  //copy to files recursively
            DeleteDirectory(); //delete to files recursively
            Environment.Exit(0);
		}

		/// <summary>
		/// Source Path(Whatever nested files that you have)
		/// </summary>
		public static readonly string APPLICATION_DATA = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EmreFolder", "Images");

        /// <summary>
        /// Destination Path((Whatever nested files that you wanted to copy))
        /// </summary>
        public static readonly string DestinationPath = "C:\\Users\\emrehuylu\\Desktop\\EmreFolder\\Images";

		/// <summary>
		/// Copy All Files as recursive method.
		/// </summary>
		/// <param name="srcDir"></param>
		/// <param name="destDir"></param>
		/// <param name="recursive"></param>
		/// <exception cref="DirectoryNotFoundException"></exception>
		static void CopyDirectory(string srcDir, string destDir, bool recursive)
		{
			// Get information about the source directory
			var dir = new DirectoryInfo(srcDir);

			// Check if the source directory exists
			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
			}

			// Cache directories before we start copying
			DirectoryInfo[] dirs = dir.GetDirectories();

			// Create the destination directory
			Directory.CreateDirectory(destDir);

			string[] sourceFolderPaths = Directory.GetDirectories(srcDir);
			string destinationFolderPaths = destDir;

			// Get the files in the source directory and copy to the destination directory
			try
			{
				foreach (FileInfo file in dir.GetFiles())
				{
					string targetFilePath = Path.Combine(destDir, file.Name);
					file.CopyTo(targetFilePath);
				}
				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder moved succesfully -->", Consts.IslemBasarılı.ToString()));
			}
			catch (Exception ex)
			{
				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder does not moved. Error -->", ex.Message));
			}

			try
			{
				// If recursive and copying subdirectories, recursively call this method
				if (recursive)
				{
					foreach (DirectoryInfo subDir in dirs)
					{
						string newDestinationDir = Path.Combine(destDir, subDir.Name);
						CopyDirectory(subDir.FullName, newDestinationDir + "_" + Guid.NewGuid().ToString(), true);
					}
				}

				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder moved succesfully -->", Consts.RecursiveBasarili.ToString()));
			}
			catch (Exception ex)
			{
				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder does not moved. Error -->", ex.Message));
			}
		}

        /// <summary>
        /// Copy all nested files recursively
        /// </summary>
        public static void CopyDirectory()
		{
			try
			{
				string srcDir = APPLICATION_DATA;
				string destDir = DestinationPath;

				CopyDirectory(srcDir, destDir, true);

				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder moved succesfully -->", Consts.KopyalamaBasarili.ToString()));
			}
			catch (Exception ex)
			{
				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder does not moved. Error -->", ex.Message));
			}
		}


		/// <summary>
		/// Delete all nested files recursively
		/// </summary>
		public static void DeleteDirectory()
		{
			try
			{
				System.IO.DirectoryInfo di = new DirectoryInfo(APPLICATION_DATA);

				foreach (DirectoryInfo dir in di.GetDirectories())
				{
					dir.Delete(true);
				}

				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder moved succesfully -->", Consts.SilmeBasarili.ToString()));
				return;
			}
			catch (Exception ex)
			{
				WriteLogs.WriteLogsMethod("CutPaste.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", " Folder does not moved. Error -->", ex.Message));
			}
		}

	}

}
