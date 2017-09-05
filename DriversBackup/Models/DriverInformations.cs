using System.Linq;
using System.Threading.Tasks;
using InfHelper;
using InfHelper.Models;

namespace DriversBackup.Models
{
    public class DriverInformations
    {
        public DriverInformation FromInfFile(string path)
        {
            var helper = new InfUtil();
            var result = helper.SerializeFileInto<DriverInformation>(path, out InfData data);

            string description = "...";
            var descritpionKey = data["Strings"].Keys.FirstOrDefault(x => x.Id.ToLower().Contains("desc"));
            if (descritpionKey != null)
            {
                description = descritpionKey.KeyValues.First().Value;
            }
            
            result.InfPath = path;
            result.DriverDescription = description;
            result.DriverId = "empty driver id";
            return result;
        }

        public async Task<DriverInformation> FromInfFileAsync(string path)
        {
            return await Task.Run(() => FromInfFile(path));
        }        
    }
}