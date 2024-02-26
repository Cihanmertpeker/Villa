using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Villa.Business.Abstract;
using Villa.Dto.Dtos.BannerDtos;
using Villa.Entity.Entities;

namespace Villa.WebUI.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerservice;
        private readonly IMapper _mapper;

        public BannerController(IBannerService bannerservice, IMapper mapper)
        {
            _bannerservice = bannerservice;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _bannerservice.TGetListAsync();
            var bannerList = _mapper.Map<List<ResultBannerDto>>(values);
            return View(bannerList);
        }
        public async Task<IActionResult> DeleteBanner(ObjectId id)
        {
            await _bannerservice.TDeleteAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult CreateBanner()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            var newBanner = _mapper.Map<Banner>(createBannerDto);
            await _bannerservice.TCreateAsync(newBanner);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateBanner(ObjectId id)
        {
            var value =  await _bannerservice.TGetByIdAsync(id);
            var updateBanner = _mapper.Map<UpdateBannerDto>(value);

            return View(updateBanner);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto updateBannerDto)
        {
            var banner = _mapper.Map<Banner>(updateBannerDto);
            await _bannerservice.TUpdateAsync(banner);

            return RedirectToAction("Index");
        }
    }
}
