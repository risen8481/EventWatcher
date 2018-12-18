using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EventWatcher.Data;
using Newtonsoft.Json;

namespace EventWatcher.Controllers
{
    public class eventsController : ApiController
    {
        private event_watcher_dbEntities db = new event_watcher_dbEntities();

        public const int SALT_SIZE = 24; // size in bytes
        public const int HASH_SIZE = 24; // size in bytes
        public const int ITERATIONS = 100000; // number of pbkdf2 iterations

        // GET: api/events
        public async Task<HttpResponseMessage> Getevents()
        {

            var eventsList = await db.events.ToListAsync();

            List<string> eventsJSONParts = new List<string>();

            foreach (var eventObject in eventsList)
            {

                string details = eventObject.descryptions.descryprion;
                string location = eventObject.locations.location;
                string price = Convert.ToString(eventObject.price);
                string event_type = eventObject.events_types.event_type;
                string date = eventObject.locations.date.Value.ToString("yyyy-MM-dd HH:MM:ss");
                string seats = Convert.ToString((db.SeatsStatus.Where(x => x.event_id == eventObject.id).FirstOrDefault()).seats_count);

                eventsJSONParts.Add(GenerateJSONPart(details, location, price, event_type, date, seats));

            }

            string message = GenerateJSONMessage(eventsJSONParts);


            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {

                Content = new StringContent(message)
            };
        }

        private string GenerateJSONMessage(List<string> eventsJSONParts)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("{\"events\":[");

            string eventsList = string.Join(",", eventsJSONParts);

            stringBuilder.Append(eventsList);

            stringBuilder.Append("]}");

            return stringBuilder.ToString();

        }

        private string GenerateJSONPart(string details, string location, string price, string event_type, string date, string seats)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("{")
              .Append("\"Location\":")
              .Append("\"" + location + "\"")
              .Append(",")
              .Append("\"event_type\":")
              .Append("\"" + event_type + "\"")
              .Append(",")
              .Append("\"seats_left\":")
              .Append("\"" + seats + "\"")
              .Append(",")
              .Append("\"price\":")
              .Append("\"" + price + "\"")
              .Append(",")
              .Append("\"date\":")
              .Append("\"" + date + "\"")
              .Append(",")
              .Append("\"details\":")
              .Append("[")
              .Append(GenerateDetails(details))
              .Append("]}");

            

            return sb.ToString();
        }

        private string GenerateDetails(string details)
        {
            string procedDetails = details.Replace(",", "},{");
            StringBuilder sb = new StringBuilder();
            sb.Append("{")
              .Append(procedDetails)
              .Append("}");

            return sb.ToString();
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool eventsExists(long id)
        {
            return db.events.Count(e => e.id == id) > 0;
        }
    }
}