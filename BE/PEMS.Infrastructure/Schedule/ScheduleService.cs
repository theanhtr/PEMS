﻿using PEMS.Application;
using PEMS.Domain;

namespace PEMS.Infrastructure
{
    public class ScheduleService : IScheduleService
    {
        /// <summary>
        /// Xóa những file đã xử lý bên trong folder
        /// </summary>
        /// <param name="folderPath">đường dẫn đến folder muốn xóa</param>
        /// CreatedBy: TTANH (22/07/2024)
        public void ClearFiles(string folderPath)
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);

            if (d.Exists)
            {
                FileInfo[] Files = d.GetFiles();

                foreach (FileInfo file in Files)
                {
                    var fileExpiredTime = file.CreationTime.AddMinutes(AppConst.ExpiredTime);

                    // -1: now < fileExpiredTime, 0 bằng nhau, 1 now > fileExpiredTime
                    var compareNow = DateTime.Compare(DateTime.Now, fileExpiredTime);

                    if (compareNow > 0)
                    {
                        file.Delete();
                    }
                }
            }
        }
    }
}
