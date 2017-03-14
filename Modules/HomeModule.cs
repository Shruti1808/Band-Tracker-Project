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
                List<Band> VenueBands = newVenue.GetBands();
                List<Band> allBands = Band.GetAll();
                Dictionary<string, object> model = new Dictionary<string, object>();
                model.Add("venue", newVenue);
                model.Add("venueBands", VenueBands);
                model.Add("allBands", allBands);
                return View["venue_detail.cshtml", model];
            };

            Get["venues/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue SelectedVenue = Venue.Find(parameters.id);
                List<Band> VenueBands = SelectedVenue.GetBands();
                List<Band> allBands = Band.GetAll();
                model.Add("venue", SelectedVenue);
                model.Add("venueBands", VenueBands);
                model.Add("allBands", allBands);
                return View["venue_detail.cshtml", model];
            };

            Post["venue/add_band"] = _ => {
              Dictionary<string, object> model = new Dictionary<string, object>();
                Venue venue = Venue.Find(Request.Form["venue-id"]);
                Band band = Band.Find(Request.Form["band-id"]);
                venue.AddBand(band);
                List<Band> VenueBands = venue.GetBands();
                List<Band> allBands = Band.GetAll();
                model.Add("venue", venue);
                model.Add("venueBands", VenueBands);
                model.Add("allBands", allBands);
                return View["venue_detail.cshtml", model];
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

            Get["venue/delete/{id}"] = parameters => {
                Venue SelectedVenue =Venue.Find(parameters.id);
                return View["venue_delete.cshtml", SelectedVenue];
            };

            Delete["venue/delete/{id}"] = parameters => {
                Venue SelectedVenue =Venue.Find(parameters.id);
                SelectedVenue.Delete();
                return View["success.cshtml"];
            };

            //For bands

            Get["/bands"] = _ => {
                List<Band> allBands = Band.GetAll();
                return View["bands.cshtml", allBands];
            };

            Get["/bands/new"] = _ => {
                return View["bands_form.cshtml"];
            };

            Post["/bands/new"] = _ => {
                Band newBand = new Band(Request.Form["band-name"]);
                newBand.Save();
                List<Venue> BandVenues = newBand.GetVenues();
                List<Venue> allVenues = Venue.GetAll();
                Dictionary<string, object> model = new Dictionary<string, object>();
                model.Add("band", newBand);
                model.Add("bandVenues", BandVenues);
                model.Add("allVenues", allVenues);
                return View["band_details.cshtml", model];
            };

            Get["bands/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Band SelectedBand = Band.Find(parameters.id);
                List<Venue> BandVenues = SelectedBand.GetVenues();
                List<Venue> allVenues = Venue.GetAll();
                model.Add("band", SelectedBand);
                model.Add("bandVenues", BandVenues);
                model.Add("allVenues", allVenues);
                return View["band_details.cshtml", model];
            };

            Post["band/add_venue"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue venue = Venue.Find(Request.Form["venue-id"]);
                Band band = Band.Find(Request.Form["band-id"]);
                band.AddVenue(venue);
                List<Venue> BandVenues = band.GetVenues();
                List<Venue> allVenues = Venue.GetAll();
                model.Add("band", band);
                model.Add("bandVenues", BandVenues);
                model.Add("allVenues", allVenues);
                return View["band_details.cshtml", model];
              };
        }
    }
}
