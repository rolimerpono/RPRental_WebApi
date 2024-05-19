using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Services.Implementation
{
    public class AmenityService : IAmenityService
    {
        private readonly IWorker _IWorker;
        private readonly APIResponse _APIResponse;
        public AmenityService(IWorker iWorker)
        {
            _IWorker = iWorker;
            _APIResponse = new();
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
        public async Task<Amenity> GetAsync(int Id)
        {
            Amenity objAmenity = new();

            try
            {

                objAmenity = await _IWorker.tbl_Amenity.GetAsync(fw => fw.AmenityId == Id);

                if (objAmenity != null)
                {
                    return objAmenity;
                }

                return null!;

            }
            catch (Exception ex)
            {
                return null!;
            }
        }



        [HttpGet]
        public async Task<IEnumerable<Amenity>> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1)
        {
            IEnumerable<Amenity> objAmenities = new List<Amenity>();

            try
            {
                objAmenities = await _IWorker.tbl_Amenity.GetAllAsync(null, null, isTracking, pageSize, pageNumber);

                if (objAmenities != null)
                {
                    return objAmenities;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
            return null!;
        }


        [HttpPost]
        public async Task<APIResponse> CreateAsync(Amenity objAmenity)
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

                await _IWorker.tbl_Amenity.CreateAync(objAmenity);
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
        public async Task<APIResponse> UpdateAsync(Amenity objAmenity)
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

                await _IWorker.tbl_Amenity.UpdateAsync(objAmenity);
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
