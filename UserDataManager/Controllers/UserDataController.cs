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
        private IDataInsertServices<UserDataDTO, UserDataInsertDTO, AddressDTO, AddressDataInsertDTO> _dataInsertServices;
        private IReadDataServices<UserDataDTO> _readDataServices;
        private IDataUpdateServices<UserDataUpdateDTO, UserDataDTO> _dataUpdateServices;
        private IDataDeleteServices _dataDeleteServices;

        public UserDataController( IUserDataClientServices userDataClientServices, IDataInsertServices<UserDataDTO, UserDataInsertDTO, AddressDTO, AddressDataInsertDTO> dataInsertServices,
            IReadDataServices<UserDataDTO> readDataServices,
            IDataUpdateServices<UserDataUpdateDTO, UserDataDTO> dataUpdateServices,
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
        public async Task<ActionResult<UserData.UserDResp>> SetUserDataClient()
        {
            var userDataResponse = await _userDataClientServices.DownloadUserData();
            var userDataResult = await _dataInsertServices.AsignDataClient(userDataResponse);

            if (userDataResult == null)
            {
                return NotFound();
            }

            return StatusCode(201, userDataResult);
        }
        [HttpPost("InsertUserData")]
        public async Task<ActionResult> InsertUserData(UserDataInsertDTO userData)
        {
            var userDataResponse = await _dataInsertServices.AddUserData(userData);

            if (userDataResponse == null)
            {
                return NotFound();
            }

            return StatusCode(201, userDataResponse);
        }
        [HttpPost("InsertAdressData")]
        public async Task<ActionResult> InsertAdressData(AddressDataInsertDTO adressDataInsertDTO)
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
            var userDataReadDTO = await _readDataServices.ReadAllData();

            if (userDataReadDTO == null)
            {
                return NotFound();
            }

            return Ok(userDataReadDTO);
        }
        [HttpGet("ReadUserData/{id}")]
        public async Task<ActionResult<UserDataDTO>> ReadUserDataList(int id)
        {
            var userData = await _readDataServices.ReadData(id);
            if (userData == null)
            {
                NotFound();
            }

            return Ok(userData);
        }
        //Read Adress
        #endregion

        #region Update
        [HttpPut("UpdateUserData")]
        public async Task<ActionResult<UserDataDTO>> UpdateUserData(UserDataUpdateDTO userData)
        {
            var userDataResult = await _dataUpdateServices.UpdateData(userData);

            if (userDataResult == null)
            {
                return NotFound();
            }

            return Ok(userDataResult);
        }
        #endregion

        #region Delete
        [HttpDelete("RemoveUserData/{id}")]
        public async Task<ActionResult> RemoveUserData(int id)
        {
            var userData = await _dataDeleteServices.RemoveData(id);
            if (!userData)
            {
                NotFound();
            }

            return NoContent();
        }
        [HttpDelete("RemoveAdressData/{id}")]
        public async Task<ActionResult> RemoveAdressData(int id)
        {
            var adressData = await _dataDeleteServices.RemoveOtherData(id);
            if (!adressData)
            {
                NotFound();
            }

            return NoContent();
        }
        [HttpDelete("ClearAllUserData")]
        public async Task<ActionResult> ClearAllUserData()
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
