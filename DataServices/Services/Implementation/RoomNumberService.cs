using AutoMapper;
using DataServices.Common.DTO;
using DataServices.Common.DTO.RoomNumber;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Services.Implementation
{
   
    public class RoomNumberService : IRoomNumberService
    {

        private readonly IWorker _IWorker;
        private readonly APIResponse _APIResponse;
        private readonly IMapper _IMapper;

        public RoomNumberService(IWorker IWorker, IMapper mapper)
        {
            _IWorker = IWorker;
            _IMapper  = mapper;
            _APIResponse = new();
        }

        public async Task<APIResponse> IsUniqueRoomNumber(int RoomNo)
        {

            try
            {
                var objRoom = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == RoomNo);

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
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }

        public async Task<APIResponse> GetAsync(int RoomNo)
        {    

            try
            {
                var objRoomNo = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == RoomNo,IncludeProperties:"Room");

                if (objRoomNo == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                RoomNumberDTO model = _IMapper.Map<RoomNumberDTO>(objRoomNo);

                _APIResponse.IsSuccess = true;
                _APIResponse.Message = SD.CrudTransactionsMessage.RecordFound;
                _APIResponse.StatusCode = HttpStatusCode.OK;
                _APIResponse.Result = model;
                return _APIResponse;
            

            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }

        public async Task<APIResponse> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {          

            try
            {
                var objRoomNos = await _IWorker.tbl_RoomNumber.GetAllAsync(null, IncludeProperties:"Room", isTracking, pageSize, pageNumber);

                if(objRoomNos == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;

                }

                IEnumerable<RoomNumberDTO> model = _IMapper.Map<List<RoomNumberDTO>>(objRoomNos);

                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
                _APIResponse.Message = SD.CrudTransactionsMessage.RecordFound;
                _APIResponse.Result = model;
                return _APIResponse;
              
            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }      

        }

        [HttpPost]
        public async Task<APIResponse> CreateAsync(RoomNumberCreateDTO objRoomNumber)
        {

            try
            {

                var response = await IsUniqueRoomNumber(objRoomNumber.RoomNo);

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                RoomNumber model = _IMapper.Map<RoomNumber>(objRoomNumber);

                await _IWorker.tbl_RoomNumber.CreateAync(model);
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
        public async Task<APIResponse> UpdateAsync(RoomNumberUpdateDTO objRoomNumber)
        {
            try
            {
                var objData = await _IWorker.tbl_RoomNumber.GetAsync(fw => fw.RoomNo == objRoomNumber.RoomNo);

                if (objData == null || objRoomNumber == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                RoomNumber model = _IMapper.Map<RoomNumber>(objRoomNumber);

                await _IWorker.tbl_RoomNumber.UpdateAsync(model);
                await _IWorker.tbl_RoomNumber.SaveAsync();

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
        public async Task<APIResponse> RemoveAsync(int RoomNo)
        {
            Room objRoom = new();

            try
            {
                objRoom = await _IWorker.tbl_Rooms.GetAsync(fw => fw.RoomId == RoomNo);

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
                _APIResponse.IsSuccess = false;
                _APIResponse.Message = ex.Message + " " + SD.SystemMessage.ContactAdmin;
                return _APIResponse;
            }
        }



    }
    
}
