using Dapper;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Monitor.Data
{
    public class CustomMapsRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;

        public CustomMapsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DateTime GetMapImageTime(string targetMapName)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.QueryFirstOrDefault<DateTime>(@"SELECT TOP 1 UpdateTime FROM CustomMaps WHERE MapName=@mapName",
                    new { mapName = targetMapName });
            }
        }

        public string GetMapImageData(string targetMapName)
        {

            using (var con = new SqlConnection(connectionString))
            {
                return con.QueryFirstOrDefault<string>("SELECT TOP 1 MapImageData FROM CustomMaps WHERE MapName = @mapName",
                    new { mapName = targetMapName });
            }

        }

        public void SetMapImageData(string targetMapName, string mapImageData)
        {

            // save encoded data
            using (var con = new SqlConnection(connectionString))
            {
                con.Execute(@"DELETE FROM CustomMaps WHERE MapName = @mapName;
                                  INSERT INTO CustomMaps VALUES (@updateTime, @mapName, @mapImageData)",
                    new
                    {
                        mapName = targetMapName,
                        mapImageData,
                        updateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            }
        }

        public string ConvertImageToEncodedString(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public Image ConvertEncodedStringToImage(string mapEncodedString)
        {

            byte[] mapDecodedBytes = Convert.FromBase64String(mapEncodedString);
            using (var ms = new MemoryStream(mapDecodedBytes))
            {
                return Image.FromStream(ms);
            }
        }

    }
}
