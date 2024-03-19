using Microsoft.AspNetCore.Mvc;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services.Interface;

namespace UserDataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private IUserDataClientServices _userDataClientServices;
        private IDataInsertServices<UserData.UserDataResponse, UserDataInsertDTO, UserData.Address, AddressDataInsertDTO> _dataInsertServices;
        private IReadDataServices<UserData.UserDataResponse> _readDataServices;
        private IDataUpdateServices<UserData.UserDataResponse, UserDataDTO> _dataUpdateServices;
        private IDataDeleteServices _dataDeleteServices;

        public UserDataController( IUserDataClientServices userDataClientServices, IDataInsertServices<UserData.UserDataResponse, UserDataInsertDTO, UserData.Address, AddressDataInsertDTO> dataInsertServices,
            IReadDataServices<UserData.UserDataResponse> readDataServices,
            IDataUpdateServices<UserData.UserDataResponse, UserDataDTO> dataUpdateServices,
            IDataDeleteServices dataDeleteServices
            )
        {
            _userDataClientServices = userDataClientServices;
            _dataInsertServices = dataInsertServices;
            _readDataServices = readDataServices;
            _dataUpdateServices = dataUpdateServices;
            _dataDeleteServices = dataDeleteServices;
        }

        #region Create
        [HttpPost("DownloadUserDataClient")]
        public async Task<ActionResult<UserData.UserDataResponse>> SetUserDataClient()
        {
            var userDataResponse = await _userDataClientServices.SetUserDataClient();
            var userDataResult = await _dataInsertServices.AsignDataClient(userDataResponse);

            if (userDataResult == null)
            {
                return NotFound();
            }

            return StatusCode(201, userDataResult);
        }
        [HttpPost("InsertUserData")]
        public async Task<ActionResult> InsertUserData(UserData.UserDataResponse userData)
        {
            var userDataResponse = await _dataInsertServices.AddUserData(userData);

            if (userDataResponse == null)
            {
                return NotFound();
            }

            return StatusCode(201, userDataResponse);
        }
        [HttpPost("InsertAdressData")]
        public async Task<ActionResult> InsertAdressData(UserData.Address adressDataInsertDTO)
        {
            var adressDataResponse = await _dataInsertServices.SetAdressData(adressDataInsertDTO);

            if (adressDataResponse == null)
            {
                return NotFound();
            }

            return StatusCode(201, adressDataResponse);
        }
        #endregion

        #region Read
        [HttpGet("ReadAllUserDataList")]
        public async Task<ActionResult<IEnumerable<UserDataDTO>>> ReadAllUserDataList()
        {
            var userDataReadDTO = await _readDataServices.ReadAllUserDataList();

            if (userDataReadDTO == null)
            {
                return NotFound();
            }

            return Ok(userDataReadDTO);
        }
        [HttpGet("ReadUserData/{id}")]
        public async Task<ActionResult<UserDataDTO>> ReadUserDataList(int id)
        {
            var userData = await _readDataServices.ReadUserData(id);
            if (userData == null)
            {
                NotFound();
            }

            return Ok(userData);
        }
        #endregion

        #region Update
        [HttpPut("UpdateUserData")]
        public async Task<ActionResult<UserDataDTO>> UpdateUserData(UserData.UserDataResponse userData)
        {
            var userDataResult = await _dataUpdateServices.UpdateUserData(userData);

            if (userDataResult == null)
            {
                NotFound();
            }

            return Ok(userDataResult);
        }
        #endregion

        #region Delete
        [HttpDelete("RemoveUserData/{id}")]
        public async Task<ActionResult> RemoveUserData(int id)
        {
            var userData = _dataDeleteServices.RemoveData(id);
            if (userData == null)
            {
                NotFound();
            }

            return NoContent();
        }
        [HttpDelete("RemoveAdressData/{id}")]
        public async Task<ActionResult> RemoveAdressData(int id)
        {
            var adressData = _dataDeleteServices.RemoveOtherData(id);
            if (adressData == null)
            {
                NotFound();
            }

            return Ok();
        }
        [HttpDelete("ClearAllUserData")]
        public async Task<ActionResult> ClearAllUserDataClient()
        {
            bool isClearData = await _dataDeleteServices.ClearAllUserDataClient();

            if (isClearData)
            {
                return Ok();
            }

            return NoContent();
        }
        #endregion

    }
}
