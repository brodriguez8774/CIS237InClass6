using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS237InClass6.Models;

namespace CIS237InClass6.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private CarsEntities db = new CarsEntities();

        // GET: /Cars/
        public ActionResult Index()
        {
            // Set up variable to hold the Cars data set.
            DbSet<Car> CarsToSearch = db.Cars;

            string filterMake = "";
            string filterMin = "";
            string filterMax = "";

            int minCyl = 0;
            int maxCyl = 16;

            // Cast session to variables, if there is anything in them.
            if (Session["make"] != null && ((string)Session["make"]).Trim() != "")
            {
                filterMake = (string)Session["make"];
            }

            if (Session["min"] != null && ((string)Session["min"]).Trim() != "")
            {
                try
                {
                    string s = (string)Session["min"];
                    minCyl = Convert.ToInt32(s);
                    filterMin = s;
                }
                catch
                {

                }
            }

            if (Session["max"] != null && ((string)Session["max"]).Trim() != "")
            {
                try
                {
                    string s = (string)Session["max"];
                    maxCyl = Convert.ToInt32(s);
                    filterMax = s;
                }
                catch
                {

                }
            }

            // Do the filter on the CarsToSearch Dataset.
            IEnumerable<Car> filtered = CarsToSearch.Where(car => car.cylinders >= minCyl &&
                                                                    car.cylinders <= maxCyl &&
                                                                    car.make.Contains(filterMake));

            // Convert filtered DataSet to list.
            IEnumerable<Car> finalFiltered = filtered.ToList();

            // Add Session info to viewbag to send to view.
            ViewBag.filterMake = filterMake;
            ViewBag.filterMin = filterMin;
            ViewBag.filterMax = filterMax;

            // Return view with a filtered selection of cars.
            return View(finalFiltered);
        }

        // GET: /Cars/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: /Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,year,make,model,type,horsepower,cylinders")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: /Cars/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: /Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,year,make,model,type,horsepower,cylinders")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: /Cars/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: /Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("Filter")]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            // Get form data.
            string make = Request.Form.Get("make");
            string min = Request.Form.Get("min");
            string max = Request.Form.Get("max");

            // Store in user's session. Default session timeout is 20 min.
            Session["make"] = make;
            Session["min"] = min;
            Session["max"] = max;

            // Redirect to index. We will do the work of actually filtering within index method.
            return RedirectToAction("Index");
        }
    }
}
