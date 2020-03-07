﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_Project.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace API_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDbContext _context;

        private readonly string apiKey;
        public HomeController(IConfiguration configuration, MovieDbContext context)
        {
            apiKey = configuration.GetSection("APIKeys")["APIMovieKey"];
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        
        public async Task<IActionResult> MovieSearch(string title, string year)
        {
            if(year == null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://www.omdbapi.com");

                var response = await client.GetAsync($"?apikey={apiKey}&s={title}");

                var results = await response.Content.ReadAsAsync<MovieSearch>();

                return View(results);
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://www.omdbapi.com");

                var response = await client.GetAsync($"?apikey={apiKey}&s={title}&y={year}");

                var results = await response.Content.ReadAsAsync<MovieSearch>();

                return View(results);
            }
        }

        public async Task<IActionResult> MovieDetails(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com");

            var response = await client.GetAsync($"?apikey={apiKey}&i={id}");
            var results = await response.Content.ReadAsAsync<MovieDetails>();

            return View(results);

        }

        public async Task<IActionResult> AddToFavorites(string Id)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.omdbapi.com");
            var response = await client.GetAsync($"?apikey={apiKey}&i={Id}");



            var movie = await response.Content.ReadAsAsync<MovieDetails>();



            int movieYear = int.Parse(movie.Year);

            FavoriteMovies finalMovie = new FavoriteMovies(movie.Title, movieYear, movie.imdbID, 50);

            finalMovie.UserId = id;

            _context.FavoriteMovies.Add(finalMovie);
            _context.SaveChanges();


            return RedirectToAction("ListFavorites");

        }

        public IActionResult ListFavorites()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<FavoriteMovies> movies = _context.FavoriteMovies.Where(x => x.UserId == id).ToList();
            return View(movies);
        }



        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}







