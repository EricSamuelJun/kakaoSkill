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
        public dynamic Post(JObject value) {
            
            Dictionary<string, object> response = new Dictionary<string, object>();
            try {
                Dictionary<string, object> text = new Dictionary<string, object>();
                Dictionary<string, object> simpleText = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();
                text.Add("text", "가즈아1234");
                text.Add("level", "6");
                text.Add("Song", "자 이제 시작이야~");
                simpleText.Add("simpleText", text);
                outputs.Add(simpleText);

                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message + "\n"+e.StackTrace);
            }

            return response;
        }

        public void CheckText(JObject value) {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            try {
                Console.WriteLine(value.ToString());
            }
            catch(Exception ex) {
                Console.WriteLine("Error @ 1");
            }
            try {
                Console.WriteLine(value["userRequest"]["utterance"].ToString());
            }
            catch (Exception ex) {
                Console.WriteLine("Error @ 2");
            }
            try {
                Console.WriteLine(value["userRequest"]["user"]["id"].ToString());
            }
            catch (Exception ex) {
                Console.WriteLine("Error @ 3");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        public string FindWithKeyString(JObject jobj, string key) {
            if (jobj.ContainsKey(key)) {
                return jobj[key].ToString();
            }
            return "";
        }
        public JObject FindWithKeyObject(JObject jobj, string key) {
            if (jobj.ContainsKey(key)) {
            }
            return null;
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
