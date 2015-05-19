using System;
using System.IO;
using System.Web;

namespace SprwMusic.Utils
{
    public class FileUtil
    {
        public static bool CreateFile(string filePath, HttpPostedFileBase file, int fileNum)
        {
            bool success = false;
            try
            {
                var BufferSize = 65536;
                using (
                    FileStream fs =
                        System.IO.File.Create(HttpContext.Current.Server.MapPath(filePath + fileNum + ".mp3")))
                {
                    using (Stream reader = file.InputStream)
                    {
                        byte[] buffer = new byte[BufferSize];
                        int read = -1, pos = 0;
                        do
                        {
                            int len = (file.ContentLength < pos + BufferSize ?
                                file.ContentLength - pos :
                                BufferSize);
                            read = reader.Read(buffer, 0, len);
                            fs.Write(buffer, 0, len);
                            pos += read;
                        } while (read > 0);
                    }
                }

                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public static bool CreateDirectory(string dirPath)
        {
            bool success = false;
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dirPath));
                    success = true;
                }

            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}