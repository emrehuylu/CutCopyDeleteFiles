using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CutCopy
{
	public static class WriteLogs
	{
		public static bool WriteLogsMethod(string strFileName, string strMessage)
		{
			try
			{
				string LogPath_value = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Carre");

				using (FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", Path.GetFullPath(LogPath_value), strFileName), FileMode.Append, FileAccess.Write))
				{
					using (StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream))
					{
						objStreamWriter.WriteLine(strMessage);
						objStreamWriter.Close();
						objFilestream.Close();
					}
				}
				return true;
			}

			catch (Exception)
			{
				return false;
			}
		}
	}
}
