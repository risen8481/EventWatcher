using EventWatcher.Data;
using EventWatcher.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventWatcher.Controllers
{
    public class HomeController : Controller
    {

        private event_watcher_dbEntities dbContext = new event_watcher_dbEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            SetCompetitionSelectListAsync();
            return View();
        }

        [HttpPost]
        public ActionResult GetBoxesList()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            PropertyInfo propertyInfo = typeof(EventModel).GetProperty(sortColumn);
            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;



            // Getting all Customer data    
            List<EventModel> eventData = dbContext.events.Select(x => new EventModel()
            {
                EventId = x.id,
                EventType = x.events_types.event_type,
                Location = x.locations.location,
                EventDate = x.locations.date,
                Descryption = x.descryptions.descryprion,
                ShowingDay = x.locations.date.Value.ToString(),
                ShowingPrice = x.price.Value.ToString()

            }).ToList();



            //Sorting    
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                if (sortColumnDir == "asc")
                {
                    eventData = eventData.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    eventData = eventData.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    eventData = eventData.Where(m => m.Location.Contains(searchValue)).ToList();
                }
                catch { }
            }

            //total number of rows count     
            recordsTotal = eventData.Count();
            //Paging     
            var data = eventData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

        }

        [HttpPost]
        public ActionResult DisplayDetailsModal(long eventID)
        {
            EventsObjects eventsObjects = new EventsObjects();

            //eventsObjects.EventType = eventTypeID;
            //eventsObjects.EventObjectList = new List<object>() { EventTypesRepository(eventTypeID) };

            string desc = dbContext.events.Where(x => x.id == eventID).Select(x => x.descryptions.descryprion).FirstOrDefault();

            string[] processedDesc = desc.Split(',');

            List<string> detailsToListConversion = processedDesc.ToList();

            for (int i = 0; i < detailsToListConversion.Count; i++)
            {
                detailsToListConversion[i] = detailsToListConversion[i].Replace("\"", "");
            }

            return PartialView("_EventDetailsPartial", detailsToListConversion);
        }

        public ActionResult DisplayAddModal(long eventTypeID)
        {
            EventsObjects eventsObjects = new EventsObjects();

            eventsObjects.EventType = eventTypeID;
            eventsObjects.EventObjectList = new List<object>() { EventTypesRepository(eventTypeID) };

            return PartialView("_AddEventPartial", eventsObjects);
        }

        [HttpPost]
        public ActionResult AddEvent(string token)
        {
            token = token.Replace("{", "").Replace("}", "");


            string[] reload = token.Split(',');

          

            string[] parts0 = reload[0].Split(':');
            string[] parts1 = reload[1].Split(':');
            string[] parts2 = reload[2].Split(':');
            string[] parts3 = reload[3].Split(':');
            string[] parts4 = reload[4].Split(':');

            

           
            List<DateTime?> dateTime = new List<DateTime?>() ;
            dateTime.Add(new DateTime());
            try
            {
                string[] parseData = parts4[1].Split('T');

                string rawDate =parseData[0].Replace("\"", "")+" "+parseData[1].Replace("\"", "") + ":" + parts4[2].Replace("\"", "");

                List<string> dateTimeStringSplit = rawDate.Split(' ').ToList();
                List<int> date = dateTimeStringSplit[0].Split('-').Select(int.Parse).ToList();
                List<int> time = dateTimeStringSplit[1].Split(':').Select(int.Parse).ToList();
                time.Add(0);
                dateTime[0] = ( new DateTime(date[0], date[1], date[2], time[0], time[1], time[2]));
            }
            catch (Exception ex )
            {

                
            }
            
            
            locations locationEntity = dbContext.locations.Add(
                        new locations()
                        {
                            location = parts1[1].Replace("\"", ""),
                            date = dateTime[0]
                        });
            dbContext.SaveChanges();

            reload = reload.Skip(5).ToArray();

            string desc = string.Join(",", reload);

            descryptions descryptionsEntity = dbContext.descryptions.Add(
                new descryptions()
                {
                    descryprion= desc
                });
            dbContext.SaveChanges();
            long id = Convert.ToInt64(parts0[1].Replace("\"", ""));
            decimal? priceValue = Convert.ToDecimal(parts2[1].Replace("\"", ""));
            events eventsEntity = dbContext.events.Add(
                new events()
                {
                    event_type_id = id,
                    location_id = locationEntity.id,
                    details_id = descryptionsEntity.id,
                    price = priceValue
                });

            var metris = EventTypesRepository(id);

            int seatsCount = Convert.ToInt32(parts3[1].Replace("\"", ""));
            SeatsStatus seatsStatusEntity = dbContext.SeatsStatus.Add(
                new SeatsStatus()
                {
                    event_id = eventsEntity.id,
                    seats_count = seatsCount
                });

            dbContext.SaveChanges();

            return Json(new { result = "true"});
        }

        [HttpPost]
        public ActionResult DeleteEvent(long eventIdValue)
        {

            dbContext.events.Remove(dbContext.events.Where(x => x.id == eventIdValue).FirstOrDefault());
            int result = dbContext.SaveChanges();

            if(result > 0)
            {
                return Json(new { msg = "true" });
            }
            else
            {
                return Json(new { msg = "false" });
            }
        }


        private object EventTypesRepository(long eventTypeID)
        {
            events_types eventType = dbContext.events_types.Where(x => x.id == eventTypeID).FirstOrDefault();



            List<object> eventsTypesObjectList = new List<object>();
            eventsTypesObjectList.Add(new ConcertDetails());
            eventsTypesObjectList.Add(new OperaDetails());

            foreach (var item in eventsTypesObjectList)
            {
                Type t = item.GetType();

                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                string name = propInfos[0].DeclaringType.Name;

                string requestEventName = name.ToLower().Replace("details", "");
                string eventName = eventType.event_type.ToLower();

                if (requestEventName == eventName)
                {

                    return item;
                }


            }

            return null;
        }


        private void SetCompetitionSelectListAsync()
        {
            List<events_types> eventsList = dbContext.events_types.ToList();
            ViewBag.EventsTypeList = new SelectList(eventsList, "id", "event_type");
        }
    }
}
