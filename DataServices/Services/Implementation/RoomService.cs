using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Utility;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace DataServices.Services.Implementation
{
    public class RoomService : IRoomService
    {

        private readonly IWorker _IWorker;
        private readonly IWebHostEnvironment _Webhost;
        private readonly IHttpContextAccessor _HttpContext;
        private readonly APIResponse _APIResponse;

        public RoomService(IWorker IWorker, IWebHostEnvironment webhost, IHttpContextAccessor httpContext)
        {
            _IWorker = IWorker;
            _Webhost = webhost;        
            _APIResponse  = new();
            _HttpContext = httpContext;
        }

        public async Task<APIResponse> IsUniqueRoom(string RoomName)
        {

            try
            {
                var objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomName.ToLower() == RoomName.ToLower());

                if (objRoom != null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                _APIResponse.IsSuccess = true;
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }

        [HttpGet]
        public async Task<Room> GetAsync(int RoomId)
        {
            Room objRoom = new();

            try
            {

                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == RoomId, null);

                if (objRoom != null)
                {
                    return objRoom;
                }

                return null!;

            }
            catch (Exception ex)
            {
                return null!;
            }

        }


        [HttpGet]
        public async Task<IEnumerable<Room>> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<Room> objRooms = new List<Room>();

            try
            {
                objRooms = await _IWorker.tbl_Rooms.GetAllAsync(null,null, isTracking, pageSize, pageNumber);             
                

                if (objRooms != null)
                {
                    return objRooms;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
        }


        [HttpPost]
        public async Task<APIResponse> CreateAsync(Room objRoom)
        {
           
            try
            {           

                var response = await IsUniqueRoom(objRoom.RoomName);

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                if (objRoom.Image != null)
                {
                    string strFilename = Guid.NewGuid().ToString() + ".jpg";
                    string strImagePath = Path.Combine(_Webhost.WebRootPath, @"img\Room Images");


                    var filestream = new FileStream(Path.Combine(strImagePath, strFilename), FileMode.Create);

                    objRoom.Image.CopyTo(filestream);                  

                    var baseUrl = $"{_HttpContext.HttpContext!.Request.Scheme}://{_HttpContext.HttpContext.Request.Host.Value}{_HttpContext.HttpContext.Request.PathBase.Value}";                   
                    objRoom.ImageUrl = baseUrl + @"\img\Room Images\" + strFilename;
                    objRoom.ImageUrlLocalPath = strImagePath + strFilename;

                    filestream.Close();
                    filestream.Dispose();
                }

                await _IWorker.tbl_Rooms.CreateAync(objRoom);
                await _IWorker.tbl_Rooms.SaveAsync();
                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.Save;

                return _APIResponse;
            }
            catch (Exception ex) {

                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }

        }


        [HttpPost]
        public async Task<APIResponse> UpdateAsync(Room objRoom)
        {
            try
            {
                var objData = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == objRoom.RoomId);

                if (objData == null || objRoom == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                if (objRoom.Image != null)
                {
                    string strFilename = Guid.NewGuid().ToString() + ".jpg";
                    string strImagePath = Path.Combine(_Webhost.WebRootPath, @"img\Room Images");


                    if (!String.IsNullOrEmpty(objRoom.ImageUrl))
                    {
                        string previous_image = strImagePath + "\\" + Path.GetFileName(objRoom.ImageUrl);

                        if (System.IO.File.Exists(previous_image))
                        {
                            System.IO.File.Delete(previous_image);
                        }

                    }

                    var filestream = new FileStream(Path.Combine(strImagePath, strFilename), FileMode.Create);
                    objRoom.Image.CopyTo(filestream);
                  

                    var baseUrl = $"{_HttpContext.HttpContext!.Request.Scheme}://{_HttpContext.HttpContext.Request.Host.Value}{_HttpContext.HttpContext.Request.PathBase.Value}";
                    objRoom.ImageUrl = baseUrl + @"\img\Room Images\" + strFilename;
                    objRoom.ImageUrlLocalPath = strImagePath + strFilename;



                    filestream.Close();
                    filestream.Dispose();

                }


                await _IWorker.tbl_Rooms.UpdateAsync(objRoom);
                await _IWorker.tbl_Rooms.SaveAsync();

                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.Save;
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }

        }



        [HttpPost]
        public async Task<APIResponse> RemoveAsync(int RoomId)
        {
            Room objRoom = new();

            try
            {
                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == RoomId);

                if (objRoom != null)
                {
                    await _IWorker.tbl_Rooms.RemoveAsync(objRoom);
                    await _IWorker.tbl_Rooms.SaveAsync();

                    _APIResponse.IsSuccess = true;
                    _APIResponse.Message = SD.CrudTransactionsMessage.Delete;
                    return _APIResponse;
                }

                _APIResponse.IsSuccess = false;
                _APIResponse.Message = SD.CrudTransactionsMessage.RecordFound;
                return _APIResponse;
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess= false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin; 
                return _APIResponse;
            }
        }

    }
}
