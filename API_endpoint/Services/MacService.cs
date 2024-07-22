using System.Net.NetworkInformation;

namespace API_endpoint.Services
{
    public class MacService
    {
        public string GetMacAddress()
        {
            /*
             * Para este módulo se debe de instalar por medio de la terminal dentro del proyecto: dotnet add package System.Management
             */
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            // Recorro las interfaces (locales de mi PC) y debe encontrar la dirección de la MAC
            foreach (var networkInterface in networkInterfaces)
            {
                // Valido que la tarjeta esté activa para poder tomar los datos de la misma
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    // Obtengo los datos de la tarjeta física y los retorno en una variable
                    var address = networkInterface.GetPhysicalAddress();
                    if (address != null && address.ToString() != "")
                    {
                        return address.ToString();
                    }
                }
            }
            return "MAC Address not found";
        }
    }
}
