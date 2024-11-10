using Microsoft.AspNetCore.Mvc;

namespace Report.API
{
    /// <summary>
    /// Controller của phòng ban
    /// </summary>
    /// CreatedBy: TTANH (12/07/2024)
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet]
        public string Test()
        {
            var result = $"Service {AppDomain.CurrentDomain.FriendlyName} is running on {Environment.MachineName}.";
            try
            {
                var location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.dll");
                var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(location);
                var fi = new System.IO.FileInfo(location);
                var builtDate = (new DateTime[] { fi.CreationTime, fi.LastWriteTime }).Max();
                result += $"{Environment.NewLine}Version: {fvi.FileVersion}, built in: {builtDate.ToString("dd/MM/yyyy HH:mm:ss")}.";
            }
            catch
            {
            }
            return result;
        }
    }
}
