using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kakaoSkill.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class sayHello : ControllerBase {
        // GET: api/<sayHello>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1000", "value2" };
        }

        // GET api/<sayHello>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<sayHello>
        [HttpPost]
        public dynamic Post(dynamic value) {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try {
                Dictionary<string, object> text = new Dictionary<string, object>();
                Dictionary<string, object> simpleText = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();
                text.Add("text", CreateText(value));
                //text.Add("포켓몬", "자 이제 시작이야 내꿈을~");
                simpleText.Add("simpleText", text);
                outputs.Add(simpleText);

                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
            }
            catch (Exception e) {

            }

            return response;
        }

        public string CreateText(dynamic val) {
            string text = "Default";
            try {
                text = val;
            }
            catch {

            }
            return text;
        }

        // PUT api/<sayHello>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<sayHello>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
