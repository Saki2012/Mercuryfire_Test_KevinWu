using Mercuryfire_Test_KevinWu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Mercuryfire_Test_KevinWu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcpdController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AcpdController(IConfiguration configuration) => _configuration = configuration;
        private string? ConnStr => _configuration.GetConnectionString("DefaultConnection");

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AcpdModel model)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(ConnStr);
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("usp_Insert_ACPD", conn))
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(new[] { model }); // 或用 JsonConvert.SerializeObject
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@json", json);
                    await cmd.ExecuteNonQueryAsync();
                }
                return Ok(new { Message = "新增成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
            var result = new List<AcpdModel>();

            using var conn = new SqlConnection(ConnStr);
            using var cmd = new SqlCommand("usp_Get_ACPD", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new AcpdModel
                {
                    ACPD_SID = reader["ACPD_SID"]?.ToString(),
                    ACPD_Cname = reader["ACPD_Cname"]?.ToString(),
                    ACPD_Ename = reader["ACPD_Ename"]?.ToString(),
                    ACPD_Sname = reader["ACPD_Sname"]?.ToString(),
                    ACPD_Email = reader["ACPD_Email"]?.ToString(),
                    ACPD_Status = reader["ACPD_Status"] as byte? ?? 0,
                    ACPD_Stop = reader["ACPD_Stop"] as bool? ?? false,
                    ACPD_StopMemo = reader["ACPD_StopMemo"]?.ToString(),
                    ACPD_LoginID = reader["ACPD_LoginID"]?.ToString(),
                    ACPD_LoginPWD = reader["ACPD_LoginPWD"]?.ToString(),
                    ACPD_Memo = reader["ACPD_Memo"]?.ToString(),
                    ACPD_NowDateTime = reader["ACPD_NowDateTime"] as DateTime?,
                    ACPD_NowID = reader["ACPD_NowID"]?.ToString(),
                    ACPD_UPDDateTime = reader["ACPD_UPDDateTime"] as DateTime?,
                    ACPD_UPDID = reader["ACPD_UPDID"]?.ToString()
                });
            }

            return Ok(result);
        }
		
		[HttpGet("{sid}")]
		public async Task<IActionResult> GetById(string sid)
		{
			var all = await GetAll() as OkObjectResult;
			var list = all?.Value as List<AcpdModel>;
			var target = list?.FirstOrDefault(x => x.ACPD_SID == sid);
			if (target == null) return NotFound();
			return Ok(target);
		}
		
		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] AcpdModel model)
		{
            var json = JsonConvert.SerializeObject(new[] { model });

            using var conn = new SqlConnection(ConnStr);
            using var cmd = new SqlCommand("usp_Update_ACPD", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@json", json);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return Ok(new { status = "updated" });
        }
		
		[HttpDelete("delete/{sid}")]
		public async Task<IActionResult> Delete(string sid)
		{
            var json = JsonConvert.SerializeObject(new[] { new { ACPD_SID = sid } });

            using var conn = new SqlConnection(ConnStr);
            using var cmd = new SqlCommand("usp_Delete_ACPD", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@json", json);

            await conn.OpenAsync();
            var rowsAffected = await cmd.ExecuteNonQueryAsync();

            return Ok(new { deleted = rowsAffected > 0 });
        }

        private async void ExecSQL(string json)
        {
            using var conn = new SqlConnection(ConnStr);
            using var cmd = new SqlCommand("usp_Delete_ACPD", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@json", json);

            await conn.OpenAsync();
        }
    }
}
