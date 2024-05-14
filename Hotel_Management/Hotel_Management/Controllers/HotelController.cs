using Hotel_Management.Data;
using Hotel_Management.DTO;
using Hotel_Management.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management.Controllers
{
    public class HotelController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HotelController(Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddHotelDTO model)
        {
            if(ModelState.IsValid)
            {
                if (model.Photo != null)
                {
                    string folder = "hotel/photo/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                    model.PhotoUrl = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await model.Photo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                }

                var hotel = new Hotel
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhotoUrl = !string.IsNullOrEmpty(model.PhotoUrl) ? model.PhotoUrl : ""
                };

                await _context.Hotels.AddAsync(hotel);

                await _context.SaveChangesAsync();

               
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> AllHotel()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return View(hotels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            var newHotel = new EditHotelDTO
            {
                Id = id,
                Name = hotel.Name,
                Address = hotel.Address,
                Email = hotel.Email,

            };
            return View(newHotel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditHotelDTO model)
        {

            if (model.Photo != null)
            {
                string folder = "hotel/photo/";
                folder += Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                model.PhotoUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await model.Photo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            }


            var hotel = await _context.Hotels.FindAsync(model.Id);

            if(hotel != null)
            {
                hotel.Name = model.Name;
                hotel.Address = model.Address;
                hotel.Email = model.Email;
                hotel.PhotoUrl = !string.IsNullOrEmpty(model.PhotoUrl) ? model.PhotoUrl : hotel.PhotoUrl;

                await _context.SaveChangesAsync();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var hotel = await _context.Hotels.FindAsync(id);
                if (hotel != null)
                {
                    _context.Hotels.Remove(hotel);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Hotel not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
