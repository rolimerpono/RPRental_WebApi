using AutoMapper;
using Azure;
using DataServices.Common.DTO;
using DataServices.Common.DTO.Amenity;
using DataServices.Common.DTO.RoomAmenity;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Services.Implementation
{
    public class RoomAmenityService : IRoomAmenityService
    {
        private readonly IWorker _IWorker;
        private readonly APIResponse _APIResponse;
        private readonly IMapper _IMapper;
        public RoomAmenityService(IWorker iWorker , IMapper mapper)
        {

            _IWorker = iWorker;
            _IMapper = mapper;
            _APIResponse = new();

        }

        [HttpGet]
        public async Task<APIResponse> GetAsync(int RoomId)
        {
            try
            {

                var objRoomAmenity = await _IWorker.tbl_RoomAmenity.GetAllAsync(fw => fw.RoomId == RoomId, IncludeProperties: "Room,Amenity");               
              
               

                if (objRoomAmenity == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;

                }

                List<RoomAmenityDTO> model = _IMapper.Map<List<RoomAmenityDTO>>(objRoomAmenity);
                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.RecordFound;
                _APIResponse.Result = model;
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
        public async Task<APIResponse> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                var objRoomAmenities = await _IWorker.tbl_RoomAmenity.GetAllAsync(null, IncludeProperties: "Room,Amenity", isTracking, pageSize, pageNumber);

                if (objRoomAmenities == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                IEnumerable<RoomAmenityDTO> model = _IMapper.Map<List<RoomAmenityDTO>>(objRoomAmenities);

                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.RecordFound;
                _APIResponse.Result = model;
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
        public async Task<APIResponse> CreateAsync(RoomAmenityCreateDTO objRoomAmenity)
        {

            RoomAmenity objRoomAmenityData = new();

            try
            {

                var objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                if(objRoom == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }


                foreach(var Amenity in objRoomAmenity.AmenityId)
                {

                    var objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == Amenity);


                    if (objAmenity == null)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.NotFound;
                        _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                        return _APIResponse;
                    }

                }

                var objData = await _IWorker.tbl_RoomAmenity.GetAllAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                foreach (var data in objData)
                {
                    await _IWorker.tbl_RoomAmenity.RemoveAsync(data);
                    await _IWorker.tbl_RoomAmenity.SaveAsync();
                }


                RoomAmenity model = new();

                foreach (var Amenity in objRoomAmenity.AmenityId)
                {

                    objRoomAmenityData = new();                 

                    objRoomAmenityData.RoomId = objRoomAmenity.RoomId;
                    objRoomAmenityData.AmenityId = Amenity;


                    model = _IMapper.Map<RoomAmenity>(objRoomAmenityData);

                    await _IWorker.tbl_RoomAmenity.CreateAync(model);
                    await _IWorker.tbl_RoomAmenity.SaveAsync();

                }

                var result = await _IWorker.tbl_RoomAmenity.GetAllAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                _APIResponse.IsSuccess = true;
                _APIResponse.Result = result;
                _APIResponse.Message = SD.CrudTransactionsMessage.Save;
                _APIResponse.StatusCode = HttpStatusCode.OK;

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
        public async Task<APIResponse> UpdateAsync(RoomAmenityUpdateDTO objRoomAmenity)
        {
            try
            {
                var objData = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == objRoomAmenity.RoomId);

                if (objData == null || objRoomAmenity == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                RoomAmenity model = _IMapper.Map<RoomAmenity>(objRoomAmenity);

                await _IWorker.tbl_RoomAmenity.UpdateAsync(model);
                await _IWorker.tbl_RoomAmenity.SaveAsync();

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
            RoomAmenity objRoomAmenity = new();

            try
            {
                objRoomAmenity = await _IWorker.tbl_RoomAmenity.GetAsync(fw => fw.RoomId == RoomId);

                if (objRoomAmenity != null)
                {
                    await _IWorker.tbl_RoomAmenity.RemoveAsync(objRoomAmenity);
                    await _IWorker.tbl_RoomAmenity.SaveAsync();

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
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }

    }
}
