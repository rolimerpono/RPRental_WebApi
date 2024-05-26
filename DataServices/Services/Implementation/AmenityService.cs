using AutoMapper;
using DataServices.Common.DTO;
using DataServices.Common.DTO.Amenity;
using DataServices.Services.Interface;
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
    public class AmenityService : IAmenityService
    {
        private readonly IWorker _IWorker;
        private readonly APIResponse _APIResponse;
        private readonly IMapper _IMapper;
        public AmenityService(IWorker iWorker , IMapper mapper)
        {
            _IWorker = iWorker;
            _APIResponse = new();
            _IMapper = mapper;
        }

        public async Task<APIResponse> IsUniqueAmenity(string AmenityName)
        {

            try
            {
                var objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityName.ToLower() == AmenityName.ToLower());

                if (objAmenity != null)
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
        public async Task<APIResponse> GetAsync(int AmenityId)
        {
           

            try
            {

                var objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == AmenityId);

                if (objAmenity == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;                
                }

                AmenityDTO model = _IMapper.Map<AmenityDTO>(objAmenity);    

                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
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
                var objAmenities = await _IWorker.tbl_Amenity.GetAllAsync(null, null, isTracking, pageSize, pageNumber);

                if (objAmenities == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.NotFound;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                IEnumerable<AmenityDTO> model = _IMapper.Map<List<AmenityDTO>>(objAmenities);

                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode  = System.Net.HttpStatusCode.OK;
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
        public async Task<APIResponse> CreateAsync(AmenityCreateDTO objAmenity)
        {

            try
            {

                var response = await IsUniqueAmenity(objAmenity.AmenityName);

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordExists;
                    return _APIResponse;
                }

                Amenity model = _IMapper.Map<Amenity>(objAmenity);

                model.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

                await _IWorker.tbl_Amenity.CreateAync(model);
                await _IWorker.tbl_Amenity.SaveAsync();
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
        public async Task<APIResponse> UpdateAsync(AmenityUpdateDTO objAmenity)
        {
            try
            {
                var objData = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == objAmenity.AmenityId);

                if (objData == null || objAmenity == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.Message = SD.CrudTransactionsMessage.RecordNotFound;
                    return _APIResponse;
                }

                Amenity model = _IMapper.Map<Amenity>(objAmenity);

                model.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);

                await _IWorker.tbl_Amenity.UpdateAsync(model);
                await _IWorker.tbl_Amenity.SaveAsync();

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
        public async Task<APIResponse> RemoveAsync(int AmenityId)
        {
            Amenity objAmenity = new();

            try
            {
                objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == AmenityId);

                if (objAmenity != null)
                {
                    await _IWorker.tbl_Amenity.RemoveAsync(objAmenity);
                    await _IWorker.tbl_Amenity.SaveAsync();

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
