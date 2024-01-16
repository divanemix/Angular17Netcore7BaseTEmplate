
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SmartWare
{
    public class SmartWareControllerBase : ControllerBase
    {

        public string GetRootPath()
        {
            return "ClientApp/dist";
        }

    }
}