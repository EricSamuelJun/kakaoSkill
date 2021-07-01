using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kakaoSkill.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class sayHello : ControllerBase {
        // GET: api/<sayHello>
        [HttpGet]
        public IEnumerable<string> Get() {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine("\nGetMethod Call");
            Console.WriteLine("======================================");
            Console.WriteLine("");
            return new string[] { "value1000", "value2" };
        }

        // GET api/<sayHello>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine("\nGetMethod Call");
            Console.WriteLine("======================================");
            Console.WriteLine("");
            return "value";
        }

        // POST api/<sayHello>
        [HttpPost]
        public dynamic Post([FromBody]JObject vals) {
            Dialog d = new Dialog(vals);
            Dictionary<string, object> response = new Dictionary<string, object>();

            try {
                Dictionary<string, object> text = new Dictionary<string, object>();
                Dictionary<string, object> simpleText = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();

                text.Add("text", d.Utterance + "1234");
                simpleText.Add("simpleText", text);
                outputs.Add(simpleText);

                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message+"\n"+e.StackTrace); 
            }

            return response;

        }

        // PUT api/<sayHello>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine("\nPut Mehtod Call with id: {0}, and value: {1}",id,value);
            Console.WriteLine("======================================");
            Console.WriteLine("");
        }

        // DELETE api/<sayHello>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
