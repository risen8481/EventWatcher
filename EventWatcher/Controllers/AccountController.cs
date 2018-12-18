using EventWatcher.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EventWatcher.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public async Task<ActionResult> CheckUser(UserModel user)
        {

            bool isAccess = await SendAuthorizationRequestAsync(user);

            ListProperties();


            if (isAccess)
            {
                return Json(new { result = "true" });
            }
            return Json(new { result = "false" });
        }


        private async Task<bool> SendAuthorizationRequestAsync(UserModel user)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //specify to use TLS 1.2 as default connection
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            string sha256Password = Sha256(user.password);

            string authMsg = JsonConvert.SerializeObject(new UserModel() { username = user.username, password = sha256Password });

            var content2 = new StringContent(authMsg, Encoding.UTF8, "application/json");

            try
            {
                var request = httpClient.PostAsync("http://127.0.0.1:8000/tickets/admin_authorization", content2);
                var res = await request.Result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<AuthResult>(res);

                if(response.Result == true && response.Status == 200)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;

            }
          
        }

        private string Sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        private void ListProperties()
        {
            List<object> eventsTypesObjectList = new List<object>();

          
            eventsTypesObjectList.Add(new ConcertDetails());
            eventsTypesObjectList.Add(new OperaDetails());

            foreach (var item in eventsTypesObjectList)
            {
                Type t = item.GetType();
                // Get the public properties.
                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                string name = propInfos[0].DeclaringType.Name;
            }

            
            
        }
    }
}