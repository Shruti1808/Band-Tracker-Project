using Nancy;
using System;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;


namespace BandTracker
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["index.cshtml"];
            };

            Get["/venues"] = _ => {
                List<Venue> allVenues = Venue.GetAll();
                return View["venues.cshtml", allVenues];
            };

            Get["/venues/new"] = _ => {
                return View["venues_form.cshtml"];
            };

            Post["/venues/new"] = _ => {
                Venue newVenue = new Venue(Request.Form["venue-name"]);
                newVenue.Save();
                return View["success.cshtml"];
            };

            Get["venue/edit/{id}"] = parameters => {
                Venue SelectedVenue = Venue.Find(parameters.id);
                return View["venue_edit.cshtml", SelectedVenue];
            };

            Patch["venue/edit/{id}"] = parameters => {
                Venue SelectedVenue = Venue.Find(parameters.id);
                SelectedVenue.Update(Request.Form["venue-name"]);
                return View["success.cshtml"];
            };
        }
    }
}
