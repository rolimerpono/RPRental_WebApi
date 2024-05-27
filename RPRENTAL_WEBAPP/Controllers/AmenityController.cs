using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Amenity;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class AmenityController : Controller
    {

        private readonly IAmenityService _IAmenityService;
        private readonly IMapper _IMapper;
        private readonly IHelperService _helper;
        private readonly ICompositeViewEngine _viewEngine;

       
        public AmenityController(IAmenityService AmenityService, IMapper Mapper, IHelperService helper, ICompositeViewEngine viewEngine)
        {
            _IAmenityService = AmenityService;
            _IMapper = Mapper;
            _helper = helper;
            _viewEngine = viewEngine;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AmenityDTO> objAmenities = new List<AmenityDTO>();

            try
            {

                var response = await _IAmenityService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession)!);

                if (response != null && response.IsSuccess)
                {
                    objAmenities = JsonConvert.DeserializeObject<List<AmenityDTO>>(Convert.ToString(response.Result)!)!;

                }

                return Json(new { success = true, message = "", data = objAmenities });
            }
            catch(Exception ex) {
                return Json(new { success = false, message = ex.Message, data = objAmenities });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AmenityCreateDTO objAmenity = new();
            try
            {                

                PartialViewResult pvr = PartialView("Create", objAmenity);
                string html_string = _helper.ViewToString(this.ControllerContext, pvr, _viewEngine);

                return Json(new { success = true, htmlContent = html_string });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }

        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AmenityCreateDTO objAmenity)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.InvalidInput });
                }

                var response = await _IAmenityService.CreateAsync<APIResponse>(objAmenity, HttpContext.Session.GetString(SD.TokenSession)!);

                if (response != null && response.IsSuccess)
                {
                    return Json(new { success = true, message = response.Message });
                }
                else
                {
                    return Json(new { success = false, message = response.Message });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }


        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(int AmenityId)
        {
            try
            {
                AmenityUpdateDTO objAmenity = new();

                var response = await _IAmenityService.GetAsync<APIResponse>(AmenityId, HttpContext.Session.GetString(SD.TokenSession)!);

                objAmenity = JsonConvert.DeserializeObject<AmenityUpdateDTO>(Convert.ToString(response.Result)!)!;


                PartialViewResult pvr = PartialView("Update", objAmenity);
                string html_string = _helper.ViewToString(this.ControllerContext, pvr, _viewEngine);

                return Json(new { success = true, message = "", htmlContent = html_string });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }


        }


        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AmenityUpdateDTO objAmenity)
        {
            try
            {
             

                var response = await _IAmenityService.UpdateAsync<APIResponse>(objAmenity, HttpContext.Session.GetString(SD.TokenSession)!);

                if (response != null && response.IsSuccess)
                {
                    return Json(new { success = true, message = response.Message });
                }
                else
                {
                    return Json(new { success = false, message = response.Message });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }

        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int AmenityId)
        {
            try
            {

                if (AmenityId == 0)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.RecordNotFound });
                }

                var response = await _IAmenityService.DeleteAsync<APIResponse>(AmenityId, HttpContext.Session.GetString(SD.TokenSession)!);

                if (response != null && response.IsSuccess)
                {
                    return Json(new { success = true, message = response.Message });
                }
                else
                {
                    return Json(new { success = false, message = response.Message });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }

        }


    }
}
